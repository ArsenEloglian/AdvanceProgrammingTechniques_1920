using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace gra
{
    public partial class sterowniki : Form
    {
        void InitializeComponentHere()
        {
            Icon = Program.żabaIcon;
        }
        public sterowniki()
        {
            InitializeComponent();
            InitializeComponentHere();
        }
        [DllImport("psapi")]
        private static extern bool EnumDeviceDrivers(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] UInt32[] ddAddresses,
            UInt32 arraySizeBytes,
            [MarshalAs(UnmanagedType.U4)] out UInt32 bytesNeeded
        );

        [DllImport("psapi")]
        private static extern int GetDeviceDriverBaseName(
            UInt32 ddAddress,
            StringBuilder ddBaseName,
            int baseNameStringSizeChars
        );
        [DllImport("psapi")]
        private static extern int GetDeviceDriverFileName(
        UInt32 ddAddress,
        StringBuilder ddBaseName,
        int baseNameStringSizeChars
        );
        class cLoadAddress
        {
            public uint LoadAddress;
            public override string ToString()
            {
                return LoadAddress.ToString("X8");
            }
        }
        private void getRunningDrivers()
        {
            UInt32 arraySize;
            UInt32 arraySizeBytes;
            UInt32[] ddAddresses;
            UInt32 bytesNeeded;
            bool success;
            success = EnumDeviceDrivers(null, 0, out bytesNeeded);
            arraySize = bytesNeeded / 4;
            arraySizeBytes = bytesNeeded;
            ddAddresses = new UInt32[arraySize];
            success = EnumDeviceDrivers(ddAddresses, arraySizeBytes, out bytesNeeded);
            DataTable table = new DataTable();
            table.Columns.Add("BaseName", typeof(string));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("LoadAddress", typeof(cLoadAddress));
            for (int i = 0; i < arraySize; i++)
            {
                StringBuilder sbN = new StringBuilder(1000);
                int result = GetDeviceDriverBaseName(ddAddresses[i], sbN, sbN.Capacity);
                StringBuilder sbF = new StringBuilder(1000);
                result = GetDeviceDriverFileName(ddAddresses[i], sbF, sbF.Capacity);
                table.Rows.Add(sbN.ToString(), sbF.ToString(), new cLoadAddress { LoadAddress = ddAddresses[i] });
            }
            table.DefaultView.Sort = "BaseName";
            disregardSelectionChanged = true;
            dataGridViewSterownikiWpamięci.DataSource = table;
            disregardSelectionChanged = false;
        }
        private void getRegistryDrivers()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BaseName", typeof(string));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Start", typeof(string));
            RegistryKey rKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\");
            foreach (string subKeyName in rKey.GetSubKeyNames())
            {   
                RegistryKey subKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\"+subKeyName);
                string fileName="";
                string type = "";
                string start = "";
                foreach (string subKeyValueName in subKey.GetValueNames())
                {   //ErrorControl dword musi być
                    if (subKeyValueName== "ImagePath")
                    {
                        fileName=(string)subKey.GetValue(subKeyValueName);
                    }
                    if (subKeyValueName == "Type")
                    {
                        switch ((int)subKey.GetValue(subKeyValueName))
                        {
                            case 1:
                            case 2:
                                type = "ring0";
                                break;
                            case 16:
                            case 32:
                                type = "ring1";
                                break;
                            case 4:
                            default:
                                type = "none";
                                break;
                        }
                    }
                    if (subKeyValueName == "Start")
                    {
                        switch ((int)subKey.GetValue(subKeyValueName))
                        {
                            case 0:
                                start = "boot";
                                break;
                            case 1:
                                start = "kernel";
                                break;
                            case 2:
                                start = "system";
                                break;
                            case 3:
                                start = "manual";
                                break;
                            case 4:
                            default:
                                start = "other";
                                break;
                        }
                    }
                }
                subKey.Close();
                table.Rows.Add(subKeyName,fileName,type,start);
            }
            rKey.Close();
            table.DefaultView.Sort = "BaseName";
            disregardSelectionChanged = true;
            dataGridViewSpisSterowników.DataSource = table;
            disregardSelectionChanged = false;
        }
        void registerDriver(string driverName)
        {
            if (!File.Exists("C:\\Windows\\Sysnative\\drivers\\" + driverName + ".sys")) File.Copy(driverName + ".sys", "C:\\Windows\\Sysnative\\drivers\\" + driverName + ".sys");
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\"+driverName);
            if (driverKey == null) {
                driverKey=Registry.LocalMachine.CreateSubKey("System\\CurrentControlSet\\Services\\" + driverName);
                driverKey.SetValue("DisplayName", driverName, RegistryValueKind.String);
                driverKey.SetValue("ErrorControl", 1, RegistryValueKind.DWord);
                driverKey.SetValue("ImagePath", "system32\\drivers\\"+driverName+".sys", RegistryValueKind.ExpandString);
                driverKey.SetValue("Start", 1, RegistryValueKind.DWord);
                driverKey.SetValue("Type", 1, RegistryValueKind.DWord);
            }
        }
        privilege driverPrivilege,debugPrivilege = null;
        private void sterowniki_Load(object sender, EventArgs e)
        {
            driverPrivilege = new privilege("SeLoadDriverPrivilege");
            debugPrivilege = new privilege("SeDebugPrivilege");
            registerDriver("npcap");
            getRunningDrivers();
            getRegistryDrivers();
            wgrajSpisZnanychSterowników();
            txtZeSpisuSterowników_TextChanged(null,null);
        }
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;
        }
        [DllImport("NtDll.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void RtlInitUnicodeString(ref UNICODE_STRING DestinationString, [MarshalAs(UnmanagedType.LPWStr)] string SourceString);
        [DllImport("ntdll.dll")]
        public static extern uint ZwLoadDriver(ref UNICODE_STRING DestinationString);
        [DllImport("ntdll.dll")]
        public static extern uint ZwUnloadDriver(ref UNICODE_STRING DestinationString);
        [DllImport("NtDll.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int RtlNtStatusToDosError(int Status);
        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            string fileName = dataGridViewSpisSterowników.SelectedRows[0].Cells["FileName"].Value.ToString();
            UNICODE_STRING unicodeString = new UNICODE_STRING();
            RtlInitUnicodeString(ref unicodeString, "\\Registry\\Machine\\System\\CurrentControlSet\\Services\\" + (string)dataGridViewSpisSterowników.SelectedRows[0].Cells["BaseName"].Value);
            uint wynik=ZwLoadDriver(ref unicodeString);
            if (wynik == 0)
            {
                getRunningDrivers();
                findDriverInDataGridViewFirstCell(dataGridViewSterownikiWpamięci, nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.SelectedRows[0].Cells[1].Value.ToString()));
                return;
            } 
            wynik = ZwUnloadDriver(ref unicodeString);
            if (wynik == 0)
            {
                getRunningDrivers();
                return;
            } else if (wynik==0xC0000034) {
                MessageBox.Show(nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.SelectedRows[0].Cells[1].Value.ToString())+" nie w system32\\drivers\\");
            } else {
                MessageBox.Show(wynik.ToString("X8"));
            }
        }
        private void txtZeSpisuSterowników_TextChanged(object sender, EventArgs e)
        {
            findDriverInDataGridViewFirstCell(dataGridViewSpisSterowników, txtZeSpisuSterowników.Text);
        }
        void findDriverInDataGridViewFirstCell(DataGridView dataGridViewGiven, string driverName) {
            int bestRow = 0;
            int maxMatch = -1;
            for (int i = 0; i < dataGridViewGiven.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewGiven.Rows[i].Cells[0].Value.ToString().Length && j < driverName.Length && char.ToUpper(dataGridViewGiven.Rows[i].Cells[0].Value.ToString()[j]) == char.ToUpper(driverName[j]); j++)
                {
                    if (j > maxMatch)
                    {
                        maxMatch = j;
                        bestRow = i;
                    }
                }
            }
            if (dataGridViewGiven.Rows.Count != 0) {
                dataGridViewGiven.FirstDisplayedScrollingRowIndex = bestRow;
                disregardSelectionChanged = true;
                dataGridViewGiven.ClearSelection();
                dataGridViewGiven.Rows[bestRow].Selected = true;
                disregardSelectionChanged = false;
            }
        }
        bool disregardSelectionChanged = false;
        private void dataGridViewZeSpisuSterowników_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSterownikiWpamięci.SelectedRows.Count > 0&&!disregardSelectionChanged) findDriverInDataGridViewFirstCell(dataGridViewSterownikiWpamięci, nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.SelectedRows[0].Cells[1].Value.ToString()));
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSterownikiWpamięci.SelectedRows.Count>0&&!disregardSelectionChanged) findDriverInDataGridViewFirstCell(dataGridViewSpisSterowników, dataGridViewSterownikiWpamięci.SelectedRows[0].Cells[0].Value.ToString().Split('.')[0]);
        }

        private void txtSpisSterowników_Click(object sender, EventArgs e)
        {
            findDriverInDataGridViewFirstCell(dataGridViewSpisSterowników, txtZeSpisuSterowników.Text);
        }

        private void txtSterownikiWpamięci_Click(object sender, EventArgs e)
        {
            findDriverInDataGridViewFirstCell(dataGridViewSterownikiWpamięci, txtSterownikiWpamięci.Text);
        }
        string nazwaSterownikaZeScieżki(string ścieżkaPliku) {
            if (ścieżkaPliku == "") return "";
            string[] częsciŚcieżki = ścieżkaPliku.Split(' ')[0].Split('\\');
            return częsciŚcieżki[częsciŚcieżki.Length - 1];
        }
        private void twóżSpisSterowników_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = new StreamWriter("sterowniki.znn")) {
                for (int i = 0; i < dataGridViewSpisSterowników.RowCount; i++)
                {
                    string nazwaSterownika = nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.Rows[i].Cells[1].Value.ToString());
                    if (nazwaSterownika != "")
                    {
                        file.WriteLine(nazwaSterownika);
                    }
                }
            }
        }
        private void txtSterownikiWpamięci_TextChanged(object sender, EventArgs e)
        {
            findDriverInDataGridViewFirstCell(dataGridViewSterownikiWpamięci, txtSterownikiWpamięci.Text);
        }

        List<string> znaneSterowniki = null;

        private void znajdźNieznany_Click(object sender, EventArgs e)
        {
            for (int i = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex + 1; i < dataGridViewSpisSterowników.RowCount; i++) if (dataGridViewSpisSterowników.Rows[i].Cells[1].Style.ForeColor == Color.Red) {
                    dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
        }

        private void btnOdPoczątku_Click(object sender, EventArgs e)
        {
            dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex = 0;
        }

        void wgrajSpisZnanychSterowników()
        {
            znaneSterowniki = new List<string>();
            if (!File.Exists("sterowniki.znn")) return;
            using (StreamReader file = new StreamReader("sterowniki.znn"))
            {
                string nazwaSterownika;
                while ((nazwaSterownika = file.ReadLine()) != null) znaneSterowniki.Add(nazwaSterownika);
            }
            for (int i = 0; i < dataGridViewSpisSterowników.RowCount; i++) if(dataGridViewSpisSterowników.Rows[i].Cells[1].Value.ToString()!="" && !znaneSterowniki.Contains(nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.Rows[i].Cells[1].Value.ToString()))) dataGridViewSpisSterowników.Rows[i].Cells[1].Style.ForeColor = Color.Red;
        }
    }
}

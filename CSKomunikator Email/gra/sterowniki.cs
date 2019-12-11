using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.ServiceProcess;
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
        public void Stream_WriteInt(Stream ioStream, uint tenInt)
        {
            ioStream.WriteByte((byte)(tenInt / (256 * 256 * 256)));
            ioStream.WriteByte((byte)(tenInt / (256 * 256)));
            ioStream.WriteByte((byte)(tenInt / 256));
            ioStream.WriteByte((byte)(tenInt & 255));
            ioStream.Flush();
        }
        public uint Stream_ReadInt(Stream ioStream)
        {
            uint tenInt = (uint)(ioStream.ReadByte() * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 + ioStream.ReadByte() * 256 + ioStream.ReadByte());
            return tenInt;
        }
        public void Stream_WriteString(Stream ioStream, string outString)
        {
            byte[] outBuffer = new UnicodeEncoding().GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue) len = (int)UInt16.MaxValue;
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();
        }
        public string Stream_ReadString(Stream ioStream)
        {
            int len;
            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            var inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);
            return new UnicodeEncoding().GetString(inBuffer);
        }

        private void getRegistryDrivers()
        {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installDriverService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(130);
            pipeServer.WaitForConnection();
            DataTable table = new DataTable();
            table.Columns.Add("BaseName", typeof(string));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Start", typeof(string));
            RegistryKey rKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\");
            foreach (string subKeyName in rKey.GetSubKeyNames())
            {
                pipeServer.WriteByte(1);
                Stream_WriteString(pipeServer, subKeyName);
                uint valueType= Stream_ReadInt(pipeServer);
                uint valueStart= Stream_ReadInt(pipeServer);
                string valueImagePath= Stream_ReadString(pipeServer);
                string type = "";
                string start = "";
                switch (valueType)
                {
                    case 1:
                    case 2:
                        type = "ring0";
                        break;
                    case 16:
                    case 32:
                        type = "ring1";
                        break;
                    case UInt32.MaxValue:
                        type = "";
                        break;
                    default:
                        type = "none";
                        break;
                }
                switch (valueStart)
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
                    case UInt32.MaxValue:
                        type = "";
                        break;
                    default:
                        start = "other";
                        break;
                }
                table.Rows.Add(subKeyName, valueImagePath, type,start);
            }
            rKey.Close();
            table.DefaultView.Sort = "BaseName";
            disregardSelectionChanged = true;
            dataGridViewSpisSterowników.DataSource = table;
            disregardSelectionChanged = false;
            pipeServer.WriteByte(0);
            pipeServer.Dispose();
        }
        void registerDriver(string driverPath, string driverName)
        {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installDriverService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(128);
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, driverPath);
            Stream_WriteString(pipeServer, driverName);
            pipeServer.Dispose();
            return;
        }
        privilege driverPrivilege = new privilege("SeLoadDriverPrivilege"), debugPrivilege = new privilege("SeDebugPrivilege"), globalPrivilege = new privilege("SeCreateGlobalPrivilege");
        private void sterowniki_Load(object sender, EventArgs e)
        {
            registerDriver(AppDomain.CurrentDomain.BaseDirectory, "npcap");
            getRunningDrivers();
            getRegistryDrivers();
            wgrajSpisZnanychSterowników();
        }
        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installDriverService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(129);
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, (string)dataGridViewSpisSterowników.SelectedRows[0].Cells["BaseName"].Value);
            uint wynik= Stream_ReadInt(pipeServer);
            if (wynik == 0)
            {
                getRunningDrivers();
                findDriverInDataGridViewFirstCell(dataGridViewSterownikiWpamięci, nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.SelectedRows[0].Cells[1].Value.ToString()));
            }
            else {
                MessageBox.Show(wynik.ToString("X8"));
            }
            pipeServer.Dispose();
            return;
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
            txtZeSpisuSterowników_TextChanged(null, null);
        }
    }
}

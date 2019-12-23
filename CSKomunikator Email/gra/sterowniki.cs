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
using System.Text.RegularExpressions;
using gra.Properties;
using System.Diagnostics;

namespace gra
{
    public partial class sterowniki : Form
    {
        void getFilesFromWeb(string webPageAddress) {
            string webPage = Program.getWebPage(webPageAddress);
            Regex pattern = new Regex(Resources.link2String);
            Match match = pattern.Match(webPage);
            while (match.Success)
            {
                try
                {
                string link = match.Groups["link"].Value;
                string subWebPage = Program.getWebPage("https://www.sysfiledown.com" + link);
                Regex subPattern = new Regex(Resources.link3String);
                Match subMatch = subPattern.Match(subWebPage);
                string subLink = subMatch.Groups["link"].Value;
                string subSubWebPage = Program.getWebPage("https://www.sysfiledown.com" + subLink);
                Regex subSubPattern = new Regex(Resources.link4String);
                Match subSubMatch = subSubPattern.Match(subSubWebPage);
                string subSubLink = subSubMatch.Groups["link"].Value;
                string finalWebPage = Program.getWebPage("https://www.sysfiledown.com" + subSubLink);
                Process.Start("https://www.sysfiledown.com" + subSubLink);
                }
            catch (Exception ex)
            {
                
            }
            match = match.NextMatch();
            }
        }
        void installAllSysFromTeczkaRing0() {
            try {
                foreach (FileInfo file in new DirectoryInfo(Program.gamePath + "ring0\\").GetFiles("*.sys"))
                {
                    registerDriver(Program.gamePath + "ring0\\", file.Name.Substring(0, file.Name.Length - 4), 1, 1);
                    uint wynik = loadDriver(file.Name.Substring(0, file.Name.Length - 4));
                }
            }
            catch (Exception ex) { 
            }
        }
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
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installAdminService();
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
            wgrajSpisZnanychSterowników();
        }
        void registerDriver(string driverPath, string driverName, uint driverType, uint driverStart)
        {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installAdminService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(128);
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, driverPath);
            Stream_WriteString(pipeServer, driverName);
            Stream_WriteInt(pipeServer, driverType);
            Stream_WriteInt(pipeServer, driverStart);
            pipeServer.Dispose();
            return;
        }

        privilege driverPrivilege = new privilege("SeLoadDriverPrivilege"), debugPrivilege = new privilege("SeDebugPrivilege"), globalPrivilege = new privilege("SeCreateGlobalPrivilege");
        private void sterowniki_Load(object sender, EventArgs e)
        {
            installAllSysFromTeczkaRing0();
            getRunningDrivers();
            getRegistryDrivers();
            pointRegistryDriver("ring0");
        }
        void pointRegistryDriver(string registryDriverName) {
            txtZeSpisuSterowników.Text = registryDriverName;
            txtZeSpisuSterowników_TextChanged(null, null);
        }
        uint loadDriver(string driverBaseName) {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installAdminService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(129);
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, driverBaseName);
            uint wynik = Stream_ReadInt(pipeServer);
            pipeServer.Disconnect();
            pipeServer.Dispose();
            return wynik;
        }
        uint unloadDriver(string driverBaseName) {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installAdminService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(132);
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, driverBaseName);
            uint wynik = Stream_ReadInt(pipeServer);
            pipeServer.Disconnect();
            pipeServer.Dispose();
            return wynik;
        }
        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            uint wynik = loadDriver((string)dataGridViewSpisSterowników.SelectedRows[0].Cells["BaseName"].Value);
            if (wynik == 0xC000010E) {
                wynik = unloadDriver((string)dataGridViewSpisSterowników.SelectedRows[0].Cells["BaseName"].Value);    
            }
            if(wynik!=0) MessageBox.Show(wynik.ToString("X8"));
            getRunningDrivers();
            findDriverInDataGridViewFirstCell(dataGridViewSterownikiWpamięci, nazwaSterownikaZeScieżki(dataGridViewSpisSterowników.SelectedRows[0].Cells[1].Value.ToString()));
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
        private void twóżSpisSterowników()
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
        void removeDriverFromRegistry(string driverBaseName)
        {
            ServiceController sc = Program.GetServiceInstalled(Program.ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) Program.installAdminService();
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Global\\graŻabkaPipe", PipeDirection.InOut, 1);
            sc.ExecuteCommand(132);//unload
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, driverBaseName);
            uint wynik = Stream_ReadInt(pipeServer);
            pipeServer.Disconnect();
            getRunningDrivers();
            sc.ExecuteCommand(133);//unregister
            pipeServer.WaitForConnection();
            Stream_WriteString(pipeServer, driverBaseName);
            pipeServer.Disconnect();
            pipeServer.Dispose();
        }
        void removeSelectedDriverFromRegistry() {
            removeDriverFromRegistry((string)dataGridViewSpisSterowników.SelectedRows[0].Cells["BaseName"].Value);
            dataGridViewSpisSterowników.Rows.Remove(dataGridViewSpisSterowników.SelectedRows[0]);
        }
        void znajdźNieznanyWDół() {
            int początekTrovarzenia = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex;
            for (int i = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex + 1; i != początekTrovarzenia; i++) {
                if (i == dataGridViewSpisSterowników.RowCount) i = 0;
                if (dataGridViewSpisSterowników.Rows[i].Cells[1].Style.ForeColor == Color.Red)
                {
                    dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }
        void znajdźNieznanyWGórę()
        {
            int początekTrovarzenia = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex;
            for (int i = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex - 1; i != początekTrovarzenia; i--)
            {
                if (i <0 ) i = dataGridViewSpisSterowników.RowCount-1;
                if (dataGridViewSpisSterowników.Rows[i].Cells[1].Style.ForeColor == Color.Red)
                {
                    dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }
        void usuńWszystkieWtórneSterowniki() {
            List<DataGridViewRow> wszystkieWtórneSterowniki = new List<DataGridViewRow>();
            int początekTrovarzenia = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex;
            for (int i = dataGridViewSpisSterowników.FirstDisplayedScrollingRowIndex + 1; i != początekTrovarzenia; i++)
            {
                if (i == dataGridViewSpisSterowników.RowCount) i = 0;
                if (dataGridViewSpisSterowników.Rows[i].Cells[1].Style.ForeColor == Color.Red) wszystkieWtórneSterowniki.Add(dataGridViewSpisSterowników.Rows[i]);
            }
            foreach (DataGridViewRow wtórnySterownik in wszystkieWtórneSterowniki) removeDriverFromRegistry((string)wtórnySterownik.Cells["BaseName"].Value);
            foreach (DataGridViewRow wtórnySterownik in wszystkieWtórneSterowniki) dataGridViewSpisSterowników.Rows.Remove(wtórnySterownik);
        }
        [DllImport("USER32.dll")]
        static extern short GetKeyState(int nVirtKey);
        private void dataGridViewSpisSterowników_KeyDown(object sender, KeyEventArgs e)
        {
            bool ctrl = Convert.ToBoolean(GetKeyState(0x11) & 0x8000);
            if (!ctrl&&e.KeyCode == Keys.Delete && dataGridViewSpisSterowników.SelectedRows.Count != 0) removeSelectedDriverFromRegistry();
            if (ctrl && e.KeyCode == Keys.Delete) usuńWszystkieWtórneSterowniki();
            if (e.KeyCode==Keys.Insert) twóżSpisSterowników();
            if (e.KeyCode == Keys.A) znajdźNieznanyWGórę();
            if (e.KeyCode == Keys.Z) znajdźNieznanyWDół();
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

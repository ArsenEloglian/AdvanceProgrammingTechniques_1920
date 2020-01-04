using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using wykazPlików.Properties;

namespace wykazPlików
{
    public partial class wykazPlików : Form
    {
        public void Stream_WriteUInt(Stream ioStream, uint tenInt)
        {
            ioStream.WriteByte((byte)(tenInt / (256 * 256 * 256)));
            ioStream.WriteByte((byte)(tenInt / (256 * 256)));
            ioStream.WriteByte((byte)(tenInt / 256));
            ioStream.WriteByte((byte)(tenInt & 255));
            ioStream.Flush();
        }
        public void Stream_WriteULng(Stream ioStream, ulong tenLng)
        {
            ulong halfLng = 256 * 256 * 256;
            halfLng *= 256;
            Stream_WriteUInt(ioStream, (uint)(tenLng / halfLng));
            Stream_WriteUInt(ioStream, (uint)(tenLng & (halfLng - 1)));
            ioStream.Flush();
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
        public uint Stream_ReadUInt(Stream ioStream)
        {
            return (uint)(ioStream.ReadByte() * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 + ioStream.ReadByte() * 256 + ioStream.ReadByte());
        }
        public ulong Stream_ReadULng(Stream ioStream)
        {
            return (ulong)(ioStream.ReadByte() * 256 * 256 * 256 * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 + ioStream.ReadByte() * 256 + ioStream.ReadByte());
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
        public wykazPlików()
        {
            InitializeComponent();
        }

        private void wykazPlików_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Resources.zgłoś;
            pictureBox2.BackgroundImage = Resources.wyjaśnij;
            pictureBox3.BackgroundImage = Resources.usuwaj;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            driveListBox1.Enabled = false;
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox3.Enabled = false;
            twórzWykazPlików();
        }

        private void twórzWykazPlików()
        {
            FileStream fileStream = File.Create("wykazPlików.wkz");
            zrzućTeczkę(fileStream, driveListBox1.Drive+"\\");
            textBox1.Text = "ZROBIONE";
            textBox1.Refresh();
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA
        {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
        public enum FINDEX_INFO_LEVELS
        {
            FindExInfoStandard = 0,
            FindExInfoBasic = 1
        }
        public enum FINDEX_SEARCH_OPS
        {
            FindExSearchNameMatch = 0,
            FindExSearchLimitToDirectories = 1,
            FindExSearchLimitToDevices = 2
        }
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindFirstFileEx(string lpFileName, FINDEX_INFO_LEVELS fInfoLevelId, out WIN32_FIND_DATA lpFindFileData, FINDEX_SEARCH_OPS fSearchOp, IntPtr lpSearchFilter, int dwAdditionalFlags);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);
        [DllImport("kernel32.dll")]
        static extern bool FindClose(IntPtr hFindFile);
        private void zrzućTeczkę(FileStream fileStream, string filePath)
        {
            List<string> plikiNames = new List<string>();
            List<string> teczkiNames = new List<string>();
            WIN32_FIND_DATA findData;
            IntPtr hFile = FindFirstFileEx( filePath+"*", FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, 0);
            if (hFile.ToInt32() != -1)
            {
                do {
                    if ((findData.dwFileAttributes & 0x10) != 0x10) plikiNames.Add(findData.cFileName);
                    else if(findData.cFileName!="."&& findData.cFileName != "..") teczkiNames.Add(findData.cFileName);
                }
                while (FindNextFile(hFile, out findData));
                FindClose(hFile);
            }
            Stream_WriteUInt(fileStream, (uint)plikiNames.Count);
            foreach (string plikName in plikiNames)
            {
                Stream_WriteString(fileStream, plikName);
                Crc32 crc32 = new Crc32();
                try {
                    byte[] allFileBytes = File.ReadAllBytes(filePath + plikName);
                    Stream_WriteUInt(fileStream, crc32.Append(0, allFileBytes, 0, allFileBytes.Length));
                    Stream_WriteUInt(fileStream, Program.liczItag(allFileBytes));
                }
                catch (Exception ex) {
                    Stream_WriteUInt(fileStream, 0);
                    Stream_WriteUInt(fileStream, 0);
                }
            }
            Stream_WriteUInt(fileStream, (uint)teczkiNames.Count);
            foreach (string teczkiName in teczkiNames) {
                Stream_WriteString(fileStream, teczkiName);
                textBox1.Text = filePath + teczkiName;
                textBox1.Refresh();
                Application.DoEvents();
                zrzućTeczkę(fileStream, filePath + teczkiName+"\\");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            driveListBox1.Enabled = false;
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox3.Enabled = false;
            twórzWykazWyjaśńeń();
            textBox1.Text = "ZROBIONE";
            textBox1.Refresh();
        }

        private void twórzWykazWyjaśńeń()
        {
            FileStream fileStream = File.OpenRead("wykazPlików.wkz");
            usuńPlikiWyjaśnień();
            wyjaśnijTeczkę(fileStream, driveListBox1.Drive + "\\", 0);
            naprawPlikiWyjaśnień();
        }

        private void naprawPlikiWyjaśnień()
        {
            naprawPlikWyjaśnień(ścieżkaPlikuZawierającegoNazwyZmienionychPlików);
            naprawPlikWyjaśnień(ścieżkaPlikuZawierającegoNazwyWtórnychPlików);
            naprawPlikWyjaśnień(ścieżkaPlikuZawierającegoNazwyUsuniętychPlików);
            naprawPlikWyjaśnień(ścieżkaPlikuZawierającegoNazwyUsuniętychTeczek);
            naprawPlikWyjaśnień(ścieżkaPlikuZawierającegoNazwyWtórnychTeczek);
        }
        class Wyjaśnienie {
            public string nazwa;
            public List<string> wejścia = new List<string>();
            public List<Wyjaśnienie> podWejścia = new List<Wyjaśnienie>();
        }
        private void naprawPlikWyjaśnień(string ścieżkaPlikuWyjaśnień)
        {
            StreamReader streamReader = File.OpenText(ścieżkaPlikuWyjaśnień);
            Wyjaśnienie wyjaśnienie = new Wyjaśnienie();
            róbPoziomPlikuWyjaśnień(0, streamReader, wyjaśnienie);
            streamReader.Close();
            StreamWriter streamWriter = File.CreateText(ścieżkaPlikuWyjaśnień);
            zapiszPoziomyPlikuWyjaśnień(0, streamWriter, wyjaśnienie);
            streamWriter.Close();
        }
        private void zapiszPoziomyPlikuWyjaśnień(int tenPoziom, StreamWriter streamWriter, Wyjaśnienie wyjaśnienie)
        {
            foreach (string wyjaśnienie1 in wyjaśnienie.wejścia) streamWriter.WriteLine(napisWypoziomowany((uint)tenPoziom, wyjaśnienie1));
            foreach (Wyjaśnienie wyjaśnienie1 in wyjaśnienie.podWejścia)
            {
                streamWriter.WriteLine(napisWypoziomowany((uint)tenPoziom, "<"+wyjaśnienie1.nazwa+">"));
                zapiszPoziomyPlikuWyjaśnień(tenPoziom+1, streamWriter, wyjaśnienie1);
                streamWriter.WriteLine(napisWypoziomowany((uint)tenPoziom, "</>"));
            }
        }
        private void róbPoziomPlikuWyjaśnień(int tenPoziom, StreamReader streamReader, Wyjaśnienie wyjaśnienie)
        {
            string wiersz;
            while ((wiersz = streamReader.ReadLine()) != null)
            {
                int poziom = pobierzPoziom(wiersz);
                if (tenPoziom != poziom) break;
                else if (tenPoziom == poziom && wiersz[poziom] == '<')
                {
                    Wyjaśnienie podWyjaśnienie = new Wyjaśnienie();
                    podWyjaśnienie.nazwa = wiersz.Trim(new char[] { '<', '>', ' ' });
                    róbPoziomPlikuWyjaśnień(tenPoziom + 1, streamReader, podWyjaśnienie);
                    if(podWyjaśnienie.wejścia.Count!=0||podWyjaśnienie.podWejścia.Count!=0) wyjaśnienie.podWejścia.Add(podWyjaśnienie);
                }
                else if (tenPoziom == poziom && wiersz[poziom] != '<') wyjaśnienie.wejścia.Add(wiersz.Trim());
            }
        }
        private int pobierzPoziom(string wiersz)
        {
            int poziom = 0;
            for (int i = 0; i < wiersz.Length; i++) if (wiersz[i] == ' ') poziom++; else break;
            return poziom;
        }
        private void usuńPlikiWyjaśnień()
        {
            if (File.Exists(ścieżkaPlikuZawierającegoNazwyZmienionychPlików)) File.Delete(ścieżkaPlikuZawierającegoNazwyZmienionychPlików);
            if (File.Exists(ścieżkaPlikuZawierającegoNazwyWtórnychPlików)) File.Delete(ścieżkaPlikuZawierającegoNazwyWtórnychPlików);
            if (File.Exists(ścieżkaPlikuZawierającegoNazwyUsuniętychPlików)) File.Delete(ścieżkaPlikuZawierającegoNazwyUsuniętychPlików);
            if (File.Exists(ścieżkaPlikuZawierającegoNazwyUsuniętychTeczek)) File.Delete(ścieżkaPlikuZawierającegoNazwyUsuniętychTeczek);
            if (File.Exists(ścieżkaPlikuZawierającegoNazwyWtórnychTeczek)) File.Delete(ścieżkaPlikuZawierającegoNazwyWtórnychTeczek);
        }

        string napisWypoziomowany(uint poziom, string jakiNapis) {
            string poziomowanie = "";
            for (uint i = 0; i < poziom; i++) poziomowanie += " ";
            return poziomowanie + jakiNapis;
        }
        string ścieżkaPlikuZawierającegoNazwyZmienionychPlików = "zmienionePliki_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        string ścieżkaPlikuZawierającegoNazwyWtórnychPlików = "wtórnePliki_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        string ścieżkaPlikuZawierającegoNazwyUsuniętychPlików = "usuniętePliki_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        string ścieżkaPlikuZawierającegoNazwyUsuniętychTeczek = "usunięteTeczki_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        string ścieżkaPlikuZawierającegoNazwyWtórnychTeczek = "wtórneTeczki_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        public void piszWybranyPlik(string wybranyPlik, uint poziom, string jakiPlikZmieniony) {
            if (!File.Exists(wybranyPlik))
            {
                using (StreamWriter sw = File.CreateText(wybranyPlik))
                {
                    sw.WriteLine(napisWypoziomowany(poziom, jakiPlikZmieniony));
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(wybranyPlik))
                {
                    sw.WriteLine(napisWypoziomowany(poziom, jakiPlikZmieniony));
                }
            }
        }
        public void piszPlikWtórny(uint poziom, string jakiPlikWtórny)
        {
            piszWybranyPlik(ścieżkaPlikuZawierającegoNazwyWtórnychPlików,poziom,jakiPlikWtórny);
        }
        public void piszPlikZmieniony(uint poziom, string jakiPlikZmieniony)
        {
            piszWybranyPlik(ścieżkaPlikuZawierającegoNazwyZmienionychPlików, poziom, jakiPlikZmieniony);
        }
        public void piszPlikUsunięty(uint poziom, string jakiPlikUsunięty)
        {
            piszWybranyPlik(ścieżkaPlikuZawierającegoNazwyUsuniętychPlików, poziom, jakiPlikUsunięty);
        }
        public void piszTeczkaUsunięta(uint poziom, string jakaTeczkaUsunięta)
        {
            piszWybranyPlik(ścieżkaPlikuZawierającegoNazwyUsuniętychTeczek, poziom, jakaTeczkaUsunięta);
        }
        public void piszTeczkaWtórna(uint poziom, string jakaTeczkaWtórna)
        {
            piszWybranyPlik(ścieżkaPlikuZawierającegoNazwyWtórnychTeczek, poziom, jakaTeczkaWtórna);
        }
        class ZawartośćPliku {
            public string nazwaPliku;
            public uint crc32;
            public uint itag;
        }
        private void wyjaśnijTeczkę(FileStream fileStream, string filePath,uint poziom)
        {
            List<ZawartośćPliku> plikiNames = new List<ZawartośćPliku>();
            List<string> teczkiNames = new List<string>();
            { //wgraj pliki z teczkami ze ścieżki
                WIN32_FIND_DATA findData;
                IntPtr hFile = FindFirstFileEx(filePath + "*", FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, 0);
                if (hFile.ToInt32() != -1)
                {
                    do
                    {
                        if ((findData.dwFileAttributes & 0x10) != 0x10) plikiNames.Add(new ZawartośćPliku() { nazwaPliku = findData.cFileName });
                        else if (findData.cFileName != "." && findData.cFileName != "..") teczkiNames.Add(findData.cFileName);
                    }
                    while (FindNextFile(hFile, out findData));
                    FindClose(hFile);
                }
            }
            { //wgraj crc32 oraz itag ze ścieżki
                for (int i = 0; i < plikiNames.Count; i++)
                {
                    Crc32 crc32 = new Crc32();
                    try
                    {
                        byte[] allFileBytes = File.ReadAllBytes(filePath + plikiNames[i].nazwaPliku);
                        plikiNames[i].crc32 = crc32.Append(0, allFileBytes, 0, allFileBytes.Length);
                        plikiNames[i].itag = Program.liczItag(allFileBytes);
                    }
                    catch (Exception ex)
                    {
                        plikiNames[i].crc32 = 0;
                        plikiNames[i].itag = 0;
                    }
                }
            }
            ZawartośćPliku[] wkzPlikiNames = new ZawartośćPliku[Stream_ReadUInt(fileStream)];
            { //wgraj pliki z wykazu
                for (int i = 0; i < wkzPlikiNames.Length; i++)
                {
                    wkzPlikiNames[i] = new ZawartośćPliku();
                    wkzPlikiNames[i].nazwaPliku = Stream_ReadString(fileStream);
                    wkzPlikiNames[i].crc32 = Stream_ReadUInt(fileStream);
                    wkzPlikiNames[i].itag = Stream_ReadUInt(fileStream);
                }
            }
            { //postanów o zmienionych, usuniętych, wtórnych plikach
                for (int i = 0; i < wkzPlikiNames.Length; i++)
                {
                    bool isWykazInPliki = false;
                    for (int j = 0; j < plikiNames.Count; j++)
                    {
                        if (plikiNames[j].nazwaPliku == wkzPlikiNames[i].nazwaPliku && (plikiNames[j].crc32 != wkzPlikiNames[i].crc32 || plikiNames[j].itag != wkzPlikiNames[i].itag))
                        {
                            piszPlikZmieniony(poziom, wkzPlikiNames[i].nazwaPliku);
                            isWykazInPliki = true;
                            break;
                        }
                        else if (plikiNames[j].nazwaPliku == wkzPlikiNames[i].nazwaPliku)
                        {
                            isWykazInPliki = true;
                            break;
                        }
                    }
                    if (!isWykazInPliki) piszPlikUsunięty(poziom, wkzPlikiNames[i].nazwaPliku);
                }
                foreach (ZawartośćPliku plikName in plikiNames)
                {
                    bool isPlikInWykaz = false;
                    for (int i = 0; i < wkzPlikiNames.Length; i++)
                    {
                        if (plikName.nazwaPliku == wkzPlikiNames[i].nazwaPliku)
                        {
                            isPlikInWykaz = true;
                            break;
                        }
                    }
                    if (!isPlikInWykaz) piszPlikWtórny(poziom, plikName.nazwaPliku);
                }
            }
            string[] wkzTeczkiNames = new string[Stream_ReadUInt(fileStream)];
            { //przetwórz teczki wykazu
                for (int i = 0; i < wkzTeczkiNames.Length; i++)
                {
                    wkzTeczkiNames[i] = Stream_ReadString(fileStream);
                    bool isWykazInTeczkiNames = false;
                    for (int j = 0; j < teczkiNames.Count; j++)
                    {
                        if (teczkiNames[j] == wkzTeczkiNames[i])
                        {
                            isWykazInTeczkiNames = true;
                            break;
                        }
                    }
                    if (!isWykazInTeczkiNames)
                    {
                        piszTeczkaUsunięta(poziom, wkzTeczkiNames[i]);
                        przeskoczWykaz(fileStream);
                    }
                    else
                    {
                        textBox1.Text = filePath + wkzTeczkiNames[i];
                        textBox1.Refresh();
                        Application.DoEvents();
                        {//powiadom xml o zmiannie teczki
                            piszPlikZmieniony(poziom, "<" + wkzTeczkiNames[i] + ">");
                            piszPlikUsunięty(poziom, "<" + wkzTeczkiNames[i] + ">");
                            piszPlikWtórny(poziom, "<" + wkzTeczkiNames[i] + ">");
                            piszTeczkaUsunięta(poziom, "<" + wkzTeczkiNames[i] + ">");
                            piszTeczkaWtórna(poziom, "<" + wkzTeczkiNames[i] + ">");
                        }
                        wyjaśnijTeczkę(fileStream, filePath + wkzTeczkiNames[i] + "\\", poziom + 1);
                        {//powiadom xml o powrocie
                            piszPlikZmieniony(poziom, "</>");
                            piszPlikUsunięty(poziom, "</>");
                            piszPlikWtórny(poziom, "</>");
                            piszTeczkaUsunięta(poziom, "</>");
                            piszTeczkaWtórna(poziom, "</>");
                        }
                    }
                }
            }
            { //wypisz wtórne teczki
                foreach (string teczkiName in teczkiNames)
                {
                    bool isTeczkiNameInWykaz = false;
                    for (int i = 0; i < wkzTeczkiNames.Length; i++)
                    {
                        if (teczkiName == wkzTeczkiNames[i])
                        {
                            isTeczkiNameInWykaz = true;
                            break;
                        }
                    }
                    if (!isTeczkiNameInWykaz) piszTeczkaWtórna(poziom, teczkiName);
                }
            }
        }

        private void przeskoczWykaz(FileStream fileStream)
        {
            ZawartośćPliku[] wkzPlikiNames = new ZawartośćPliku[Stream_ReadUInt(fileStream)];
            for (int i = 0; i < wkzPlikiNames.Length; i++)
            {
                wkzPlikiNames[i] = new ZawartośćPliku();
                wkzPlikiNames[i].nazwaPliku = Stream_ReadString(fileStream);
                wkzPlikiNames[i].crc32 = Stream_ReadUInt(fileStream);
                wkzPlikiNames[i].itag = Stream_ReadUInt(fileStream);
            }
            string[] wkzTeczkiNames = new string[Stream_ReadUInt(fileStream)];
            for (int i = 0; i < wkzTeczkiNames.Length; i++)
            {
                wkzTeczkiNames[i] = Stream_ReadString(fileStream);
                przeskoczWykaz(fileStream);
            }
        }
    }
}

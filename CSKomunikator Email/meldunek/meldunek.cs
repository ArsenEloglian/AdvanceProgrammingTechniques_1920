using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace meldunek
{
    public partial class meldunek : Form
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
        public void Stream_WriteStrings(Stream ioStream, string[] outStrings)
        {
            Stream_WriteUInt(ioStream, (uint)outStrings.Length);
            foreach (string outString in outStrings) Stream_WriteString(ioStream, outString);
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
        public string[] Stream_ReadStrings(Stream ioStream)
        {
            string[] outStrings = new string[Stream_ReadUInt(ioStream)];
            for (int i = 0; i < outStrings.Length; i++) outStrings[i] = Stream_ReadString(ioStream);
            return outStrings;
        }
        void zrzućKlucz(FileStream fileStream, RegistryKey registryKey)
        {
            string[] valueNames = registryKey.GetValueNames();
            Stream_WriteUInt(fileStream, (uint)valueNames.Length);
            foreach (string valueName in valueNames)
            {
                Stream_WriteString(fileStream, valueName);
                RegistryValueKind registryValueKind = registryKey.GetValueKind(valueName);
                Stream_WriteUInt(fileStream, (uint)registryValueKind);
                object object1 = registryKey.GetValue(valueName);
                if (registryValueKind is RegistryValueKind.Binary)
                {
                    Stream_WriteUInt(fileStream, (uint)((byte[])object1).Length);
                    fileStream.Write((byte[])object1, 0, ((byte[])object1).Length);
                }
                else if (registryValueKind is RegistryValueKind.DWord)
                {
                    try
                    {
                        Stream_WriteUInt(fileStream, (uint)((int)object1));
                    }
                    catch (Exception ex)
                    {
                        Stream_WriteUInt(fileStream, 0);
                    }
                }
                else if (registryValueKind is RegistryValueKind.QWord)
                {
                    Stream_WriteULng(fileStream, (ulong)(long)object1);
                }
                else if (registryValueKind is RegistryValueKind.String)
                {
                    Stream_WriteString(fileStream, (string)object1);
                }
                else if (registryValueKind is RegistryValueKind.MultiString)
                {
                    Stream_WriteStrings(fileStream, (string[])object1);
                }
                else if (registryValueKind is RegistryValueKind.ExpandString)
                {
                    Stream_WriteString(fileStream, (string)object1);
                }
            }
            string[] subKeyNames = new string[0];
            try {
                subKeyNames =registryKey.GetSubKeyNames();
            }
            catch (Exception ex) { 
            }
            Stream_WriteUInt(fileStream, (uint)subKeyNames.Length);
            foreach (string subKeyName in subKeyNames)
            {
                Stream_WriteString(fileStream, subKeyName);
                RegistryKey registryKey1 = null;
                try
                {
                    registryKey1 = registryKey.CreateSubKey(subKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (Exception ex)
                {
                    Stream_WriteUInt(fileStream, 0);
                    Stream_WriteUInt(fileStream, 0);
                }
                if (registryKey1 != null)
                {
                    textBox1.Text = registryKey.ToString() + "\\" + subKeyName;
                    textBox1.Refresh();
                    Application.DoEvents();
                    zrzućKlucz(fileStream, registryKey1);
                }
            }
        }
        void przeskoczMeldunek(FileStream fileStream)
        {
            string[] melValueNames = new string[Stream_ReadUInt(fileStream)];
            for (int i = 0; i < melValueNames.Length; i++)
            {
                melValueNames[i] = Stream_ReadString(fileStream);
                RegistryValueKind registryValueKind = (RegistryValueKind)Stream_ReadUInt(fileStream);
                if (registryValueKind is RegistryValueKind.Binary)
                {
                    byte[] binaryObject = new byte[Stream_ReadUInt(fileStream)];
                    fileStream.Read(binaryObject, 0, binaryObject.Length);
                }
                else if (registryValueKind is RegistryValueKind.DWord)
                {
                    object tmpObject = Stream_ReadUInt(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.QWord)
                {
                    object tmpObject = Stream_ReadULng(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.String)
                {
                    object tmpObject = Stream_ReadString(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.MultiString)
                {
                    object tmpObject = Stream_ReadStrings(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.ExpandString)
                {
                    object tmpObject = Stream_ReadString(fileStream);
                }
            }
            string[] melSubKeyNames = new string[Stream_ReadUInt(fileStream)];
            for (int i = 0; i < melSubKeyNames.Length; i++)
            {
                melSubKeyNames[i] = Stream_ReadString(fileStream);
                przeskoczMeldunek(fileStream);
            }
        }
        void wgrajKlucz(FileStream fileStream, RegistryKey registryKey)
        {
            string[] valueNames = registryKey.GetValueNames();
            string[] melValueNames = new string[Stream_ReadUInt(fileStream)];
            object[] melObjects = new object[melValueNames.Length];
            RegistryValueKind[] melRegistryValueKinds = new RegistryValueKind[melValueNames.Length];
            for (int i = 0; i < melValueNames.Length; i++)
            {
                melValueNames[i] = Stream_ReadString(fileStream);
                RegistryValueKind registryValueKind = (RegistryValueKind)Stream_ReadUInt(fileStream);
                melRegistryValueKinds[i] = registryValueKind;
                if (registryValueKind is RegistryValueKind.Binary)
                {
                    byte[] binaryObject = new byte[Stream_ReadUInt(fileStream)];
                    fileStream.Read(binaryObject, 0, binaryObject.Length);
                    melObjects[i] = binaryObject;
                }
                else if (registryValueKind is RegistryValueKind.DWord)
                {
                    melObjects[i] = Stream_ReadUInt(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.QWord)
                {
                    melObjects[i] = Stream_ReadULng(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.String)
                {
                    melObjects[i] = Stream_ReadString(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.MultiString)
                {
                    melObjects[i] = Stream_ReadStrings(fileStream);
                }
                else if (registryValueKind is RegistryValueKind.ExpandString)
                {
                    melObjects[i] = Stream_ReadString(fileStream);
                }
            }
            for (int i = 0; i < melValueNames.Length; i++)
            {
                bool isMeldunekInValues = false;
                for (int j = 0; j < valueNames.Length; j++)
                {
                    RegistryValueKind registryValueKind = registryKey.GetValueKind(valueNames[j]);
                    if (valueNames[j] == melValueNames[i] && registryValueKind == melRegistryValueKinds[i])
                    {
                        isMeldunekInValues = true;
                        break;
                    }
                }
                try
                {
                    if (melRegistryValueKinds[i] != RegistryValueKind.Unknown) registryKey.SetValue(melValueNames[i], melObjects[i], melRegistryValueKinds[i]);
                    if (!isMeldunekInValues && melRegistryValueKinds[i] != RegistryValueKind.Unknown)
                    {
                        WriteZmianyToFile("+" + registryKey.ToString() + "\\" + melRegistryValueKinds[i]);
                    }else if (!isMeldunekInValues && melRegistryValueKinds[i] == RegistryValueKind.Unknown)
                    {
                        WriteZmianyToFile("?" + registryKey.ToString() + "\\" + melRegistryValueKinds[i]);
                    }
                }
                catch (Exception ex)
                {
                    WriteOdmowaToFile("+" + registryKey.ToString() + "\\" + melRegistryValueKinds[i]);
                }
            }
            foreach (string valueName in valueNames)
            {
                bool isValueInMeldunek = false;
                RegistryValueKind registryValueKind = registryKey.GetValueKind(valueName);
                for (int i = 0; i < melValueNames.Length; i++)
                {
                    if (valueName == melValueNames[i] && registryValueKind == melRegistryValueKinds[i])
                    {
                        isValueInMeldunek = true;
                        break;
                    }
                }
                if (!isValueInMeldunek)
                {
                    try
                    {
                        registryKey.DeleteValue(valueName);
                        WriteZmianyToFile("-" + registryKey.ToString() + "\\" + valueName);
                    }
                    catch (Exception ex)
                    {
                        WriteOdmowaToFile("-" + registryKey.ToString() + "\\" + valueName);
                    }
                }
            }
            string[] subKeyNames= new string[0];
            try {
                subKeyNames = registryKey.GetSubKeyNames();
            }
            catch (Exception ex) {
            }
            string[] melSubKeyNames = new string[Stream_ReadUInt(fileStream)];
            for (int i = 0; i < melSubKeyNames.Length; i++)
            {
                melSubKeyNames[i] = Stream_ReadString(fileStream);
                RegistryKey registryKey1 = null;
                bool isMeldunekInSubKeyNames = false;
                for (int j = 0; j < subKeyNames.Length; j++)
                {
                    if (subKeyNames[j] == melSubKeyNames[i])
                    {
                        isMeldunekInSubKeyNames = true;
                        break;
                    }
                }
                try
                {
                    registryKey1 = registryKey.CreateSubKey(melSubKeyNames[i], RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (!isMeldunekInSubKeyNames)
                    {
                        WriteZmianyToFile("[+]" + registryKey.ToString() + "\\" + melSubKeyNames[i]);
                    }
                }
                catch (Exception ex)
                {
                    WriteOdmowaToFile("[+]" + registryKey.ToString() + "\\" + melSubKeyNames[i]);
                    przeskoczMeldunek(fileStream);
                }
                if (registryKey1 != null)
                {
                    textBox1.Text = registryKey.ToString() + "\\" + melSubKeyNames[i];
                    textBox1.Refresh();
                    Application.DoEvents();
                    wgrajKlucz(fileStream, registryKey1);
                }
            }
            foreach (string subKeyName in subKeyNames)
            {
                bool isSubKeyNameInMeldunek = false;
                for (int i = 0; i < melSubKeyNames.Length; i++)
                {
                    if (subKeyName == melSubKeyNames[i])
                    {
                        isSubKeyNameInMeldunek = true;
                        break;
                    }
                }
                if (!isSubKeyNameInMeldunek)
                {
                    try {
                        if (registryKey.SubKeyCount != 0) registryKey.DeleteSubKeyTree(subKeyName);
                        else registryKey.DeleteSubKey(subKeyName);
                        WriteZmianyToFile("[-]" + registryKey.ToString() + "\\" + subKeyName);
                    }
                    catch (Exception ex)
                    {
                        WriteOdmowaToFile("[-]" + registryKey.ToString() + "\\" + subKeyName);
                    }
                }
            }
        }
        string odmowaFilePath = "odmowa_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        string zmianyFilePath = "zmiany_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        public void WriteOdmowaToFile(string Message)
        {
            if (!File.Exists(odmowaFilePath))
            {
                using (StreamWriter sw = File.CreateText(odmowaFilePath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(odmowaFilePath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        public void WriteZmianyToFile(string Message)
        {
            if (!File.Exists(zmianyFilePath))
            {
                using (StreamWriter sw = File.CreateText(zmianyFilePath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(zmianyFilePath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        void zrzućMeldunek()
        {
            if (File.Exists(odmowaFilePath)) File.Delete(odmowaFilePath);
            if (File.Exists(zmianyFilePath)) File.Delete(zmianyFilePath);
            FileStream fileStream = File.Create("meldunek.mel");
            zrzućKlucz(fileStream, Registry.ClassesRoot);
            zrzućKlucz(fileStream, Registry.LocalMachine);
            zrzućKlucz(fileStream, Registry.CurrentUser);
            zrzućKlucz(fileStream, Registry.CurrentConfig);
            zrzućKlucz(fileStream, Registry.Users);
            textBox1.Text = "ZROBIONE";
            textBox1.Refresh();
        }
        void wgrajMeldunek()
        {
            if (File.Exists(odmowaFilePath)) File.Delete(odmowaFilePath);
            if (File.Exists(zmianyFilePath)) File.Delete(zmianyFilePath);
            FileStream fileStream = File.OpenRead("meldunek.mel");
            wgrajKlucz(fileStream, Registry.ClassesRoot);
            wgrajKlucz(fileStream, Registry.LocalMachine);
            wgrajKlucz(fileStream, Registry.CurrentUser);
            wgrajKlucz(fileStream, Registry.CurrentConfig);
            wgrajKlucz(fileStream, Registry.Users);
            textBox1.Text = "ZROBIONE";
            textBox1.Refresh();
        }
        public meldunek()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            zrzućMeldunek();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            wgrajMeldunek();
        }
    }
}

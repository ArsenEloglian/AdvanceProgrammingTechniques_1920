using Microsoft.Win32;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace gra
{
    public partial class usługaŻabki : ServiceBase
    {
        privilege driverPrivilege= new privilege("SeLoadDriverPrivilege"), debugPrivilege = new privilege("SeDebugPrivilege"), globalPrivilege = new privilege("SeCreateGlobalPrivilege");
        void createMemoryMappedFile() {
            MemoryMappedFileSecurity acl = new MemoryMappedFileSecurity();
            acl.AddAccessRule(new AccessRule<MemoryMappedFileRights>(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MemoryMappedFileRights.FullControl, AccessControlType.Allow));
            MemoryMappedFile mmfKernel = MemoryMappedFile.CreateNew("Global\\graŻabkaMMF", 1024, MemoryMappedFileAccess.ReadWrite,0,acl,0);
            MemoryMappedViewStream mmvsKernel = mmfKernel.CreateViewStream(0, 1024);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mmvsKernel, "żabka ze sterownika");
            mmvsKernel.Seek(0, SeekOrigin.Begin);
            Semaphore semaphore = new Semaphore(1, 1, @"Global\graŻabkaSemaphore");
            semaphore.Release();
        }
        public usługaŻabki()
        {
            InitializeComponent();
        }
        protected override void OnCustomCommand(int command)
        {
            switch (command)
            {
                case 128:
                    registerDriver();
                    break;
                case 129:
                    loadUnloadDriver();
                    break;
                case 130:
                    replyWithDriverInfo();
                    break;
                case 131:
                    createMemoryMappedFile();
                    break;
                default:
                    break;
            }
        }
        void registerDriver() {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "Global\\graŻabkaPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
            pipeClient.Connect();
            string driverPath = Stream_ReadString(pipeClient);
            string driverName = Stream_ReadString(pipeClient);
            if (!File.Exists("C:\\Windows\\Sysnative\\drivers\\" + driverName + ".sys")) File.Copy(driverPath + driverName + ".sys", "C:\\Windows\\Sysnative\\drivers\\" + driverName + ".sys");
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\" + driverName);
            if (driverKey == null)
            {
                driverKey = Registry.LocalMachine.CreateSubKey("System\\CurrentControlSet\\Services\\" + driverName);
                driverKey.SetValue("DisplayName", driverName, RegistryValueKind.String);
                driverKey.SetValue("ErrorControl", 1, RegistryValueKind.DWord);
                driverKey.SetValue("ImagePath", "system32\\drivers\\" + driverName + ".sys", RegistryValueKind.ExpandString);
                driverKey.SetValue("Start", 1, RegistryValueKind.DWord);
                driverKey.SetValue("Type", 1, RegistryValueKind.DWord);
            }
        }
        void replyWithDriverInfo() {
            NamedPipeClientStream pipeClient130 = new NamedPipeClientStream(".", "Global\\graŻabkaPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
            pipeClient130.Connect();
            while (pipeClient130.ReadByte() == 1)
            {
                
                string subKeyName130 = Stream_ReadString(pipeClient130);
                RegistryKey subKey130 = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\" + subKeyName130);
                int valueType = Int32.MaxValue;
                object obj = subKey130.GetValue("Type");
                if (obj != null) valueType = (int)obj;
                Stream_WriteInt(pipeClient130, (uint)valueType);
                int valueStart = Int32.MaxValue;
                obj = subKey130.GetValue("Start");
                if (obj != null) valueStart = (int)obj;
                Stream_WriteInt(pipeClient130, (uint)valueStart);
                string valueImagePath = "";
                obj = subKey130.GetValue("ImagePath");
                if (obj != null) valueImagePath = (string)obj;
                Stream_WriteString(pipeClient130, valueImagePath);
                subKey130.Close();
            }
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

        void loadUnloadDriver() {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "Global\\graŻabkaPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
            pipeClient.Connect();
            string baseName = Stream_ReadString(pipeClient);
            UNICODE_STRING unicodeString = new UNICODE_STRING();
            RtlInitUnicodeString(ref unicodeString, "\\Registry\\Machine\\System\\CurrentControlSet\\Services\\" + baseName);
            uint wynik = ZwLoadDriver(ref unicodeString);
            if (wynik == 0)
            {
                Stream_WriteInt( pipeClient, wynik);
                return;
            }
            wynik = ZwUnloadDriver(ref unicodeString);
            Stream_WriteInt(pipeClient, wynik);
            return;
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
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}

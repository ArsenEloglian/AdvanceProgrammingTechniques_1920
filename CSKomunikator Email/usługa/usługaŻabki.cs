using Microsoft.Win32;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace gra
{
    public partial class usługaŻabki : ServiceBase
    {
        privilege driverPrivilege= new privilege("SeLoadDriverPrivilege"), debugPrivilege = new privilege("SeDebugPrivilege");
        System.Timers.Timer timer = new System.Timers.Timer();
        static MemoryMappedFile mmfKernel;
        static MemoryMappedViewStream mmvStreamKernel;

        public usługaŻabki()
        {
            InitializeComponent();

/*            bool czyZnalazłSemaphore = true;
            SemaphoreSecurity semSec = new SemaphoreSecurity();
            SemaphoreAccessRule rule = new SemaphoreAccessRule("OKARDAKONUR\\okardak", SemaphoreRights.FullControl, AccessControlType.Allow);
            semSec.AddAccessRule(rule);
            Semaphore semaphoreObject = new Semaphore(1, 1, @"Global\graŻabkaSemaphoree", out czyZnalazłSemaphore, semSec);
            
            string user = Environment.UserDomainName + "\\" + Environment.UserName;
            WriteToFile("::"+user);
            semaphoreObject.WaitOne();*/
      /*      mmfKernel = MemoryMappedFile.CreateOrOpen("mmf1.data", 1024, MemoryMappedFileAccess.ReadWrite);
            mmvStreamKernel = mmfKernel.CreateViewStream(0, 1024);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mmvStreamKernel, (int)34);
            mmvStreamKernel.Seek(0, SeekOrigin.Begin);*/
        }
        protected override void OnCustomCommand(int command)
        {
            switch (command)
            {
                case 128:
                    Semaphore semaphore128 = new Semaphore(1, 1, @"Global\graŻabkaSemaphore");
                    string mappedFileName = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf('\\'));
                    mappedFileName = mappedFileName.Substring(0, mappedFileName.LastIndexOf('\\'));
                    mappedFileName = mappedFileName.Substring(0, mappedFileName.LastIndexOf('\\'));
                    mappedFileName = mappedFileName.Substring(0, mappedFileName.LastIndexOf('\\'))+ "\\mappedFile";
                    string subKeyName=File.ReadAllText(mappedFileName);
                    RegistryKey subKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\" + subKeyName);
                    using (FileStream stream = new FileStream(mappedFileName, FileMode.Create))
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            int valueType = -1;
                            object obj = subKey.GetValue("Type");
                            if (obj != null) valueType = (int)obj;
                            writer.Write(valueType);
                            int valueStart = -1;
                            obj = subKey.GetValue("Start");
                            if (obj != null) valueStart = (int)obj;
                            writer.Write(valueStart);
                            string valueImagePath = "";
                            obj = subKey.GetValue("ImagePath");
                            if (obj != null) valueImagePath = (string)obj;
                            writer.Write(valueImagePath);
                            writer.Close();
                            writer.Dispose();
                            //WriteToFile("P:"+subKeyName+",T:"+valueType.ToString()+",S:"+valueStart.ToString()+",P:"+valueImagePath);
                        }
                        stream.Close();
                        stream.Dispose();
                    }
                    subKey.Close();
                    semaphore128.Release();
                    break;
                case 129:
                    string driverName = "npcap";
                    if (!File.Exists("C:\\Windows\\Sysnative\\drivers\\" + driverName + ".sys")) File.Copy(AppDomain.CurrentDomain.BaseDirectory + driverName + ".sys", "C:\\Windows\\Sysnative\\drivers\\" + driverName + ".sys");
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
                    WriteToFile(driverName+": " + DateTime.Now);
                    break;
                case 130:
                    Semaphore semaphoreObjectr = new Semaphore(1, 1, @"Global\graŻabkaSemaphoree");
                    //semaphoreObjectr.WaitOne();
                    WriteToFile("130:");
                    semaphoreObjectr.Release();
                    break;
                default:
                    break;
            }
        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("usługa rozpoczęła: " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile("usługa zakończona: " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //WriteToFile(DateTime.Now+"");
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

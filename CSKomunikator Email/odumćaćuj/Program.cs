using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;

namespace odumćaćuj
{
    static class Program
    {
        static void uninstallAllSysFromTeczkaRing0()
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Program.gamePath + "ring0\\").GetFiles("*.sys")) Registry.LocalMachine.DeleteSubKeyTree("System\\CurrentControlSet\\Services\\" + file.Name.Substring(0, file.Name.Length - 4));
            }
            catch (Exception ex)
            {
            }
        }

        static ServiceController GetInstalledService(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services) if (service.ServiceName == serviceName) return service;
            return null;
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            uninstallRing1Service();
            uninstallRegistryEntries();
            uninstallAllSysFromTeczkaRing0();
            uninstallSendTo();
        }

        private static void uninstallRegistryEntries()
        {
            try
            {
                RegistryKey śćskaczKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\directory\\shell", true);
                śćskaczKey.DeleteSubKeyTree("śćskacz");
            }
            catch (Exception ex) { }
            try
            {
                RegistryKey śćskaczKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\*\\shell", true);
                śćskaczKey.DeleteSubKeyTree("śćskacz");
            }
            catch (Exception ex) { }
            try
            {
                Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue("graŻabka");
            }
            catch (Exception ex) { }
        }

        private static void uninstallRing1Service()
        {
            ServiceController sc = GetInstalledService(ring1ServiceName);
            if (sc != null && sc.Status == ServiceControllerStatus.Running) sc.Stop();
            Process serviceRemove = Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\instsrv.exe", ring1ServiceName + " REMOVE") { WindowStyle = ProcessWindowStyle.Hidden, RedirectStandardError = false, RedirectStandardOutput = false, UseShellExecute = true, Verb = "runas" });
        }

        private static void uninstallSendTo()
        {
            try {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\SendTo\\ściskacz.lnk");
            }catch(Exception ex) { }
        }

        public static string gamePath = Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\') - 1) - 1) + 1);
        public static string ring1ServiceName = "_graŻabkaUsługa";
    }
}

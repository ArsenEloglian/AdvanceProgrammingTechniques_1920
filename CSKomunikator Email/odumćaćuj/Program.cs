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
            ServiceController sc = GetInstalledService(ring1ServiceName);
            if (sc != null && sc.Status == ServiceControllerStatus.Running) sc.Stop();
            Process serviceRemove = Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\Debug\\instsrv.exe", ring1ServiceName + " REMOVE") { WindowStyle = ProcessWindowStyle.Hidden, RedirectStandardError = false, RedirectStandardOutput = false, UseShellExecute = true, Verb = "runas" });
        }
        public static string gamePath = Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\') - 1) - 1) - 1) + 1);
        public static string ring1ServiceName = "_graŻabkaUsługa";
    }
}

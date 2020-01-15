using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace gra
{
    static partial class Program
    {
        private static void installServiceGameZipper() {
            installZipper();
            installGameGraŻabka();
            installAdminService();
        }
        private static void installZipper()
        {
            createLink(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\SendTo", "śćskacz");
            string staryZIP = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\SendTo\\Compressed (zipped) Folder.ZFSendToTarget";
            if (File.Exists(staryZIP)) File.Delete(staryZIP);
            RegistryKey śćskaczKey = Registry.CurrentUser.OpenSubKey("Software\\Classes", true);
            śćskaczKey = śćskaczKey.CreateSubKey("directory");
            śćskaczKey = śćskaczKey.CreateSubKey("shell");
            śćskaczKey = śćskaczKey.CreateSubKey("śćskacz");
            śćskaczKey.SetValue("icon", gamePath + "gra\\żaba.ico", RegistryValueKind.String);
            śćskaczKey = śćskaczKey.CreateSubKey("command");
            śćskaczKey.SetValue("", Application.ExecutablePath.ToString() + " %1");
            śćskaczKey = Registry.CurrentUser.OpenSubKey("Software\\Classes", true);
            śćskaczKey = śćskaczKey.CreateSubKey("*");
            śćskaczKey = śćskaczKey.CreateSubKey("shell");
            śćskaczKey = śćskaczKey.CreateSubKey("śćskacz");
            śćskaczKey.SetValue("icon", gamePath + "gra\\żaba.ico", RegistryValueKind.String);
            śćskaczKey = śćskaczKey.CreateSubKey("command");
            śćskaczKey.SetValue("", Application.ExecutablePath.ToString() + " %1");
        }
        public static void becomeAdmin()
        {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                Process.Start(new ProcessStartInfo(Application.ExecutablePath.ToString(), "") { UseShellExecute = true, Verb = "runas" });
                Environment.Exit(0);
            }
        }
        public static void installAdminService()
        {
            ServiceController sc = GetServiceInstalled(ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running)
            {
                becomeAdmin();
                Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\instsrv.exe", ring1ServiceName + " REMOVE") { WindowStyle = ProcessWindowStyle.Hidden, UseShellExecute = true, Verb = "runas" });
                Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\instsrv.exe", ring1ServiceName + " \"" + gamePath + "usługa\\bin\\usługa.exe\"") { WindowStyle = ProcessWindowStyle.Hidden, UseShellExecute = true, Verb = "runas" });
                Thread.Sleep(500);//daj czas na wgranie
                sc = GetServiceInstalled(ring1ServiceName);
                if (sc == null) MessageBox.Show("za mało czasu na wgranie ring1");
                sc.Start();
            }
        }
        public static void installGameGraŻabka()
        {
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue("graŻabka", Application.ExecutablePath.ToString());
        }
        public static ServiceController GetServiceInstalled(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services) if (service.ServiceName == serviceName) return service;
            return null;
        }

    }
}

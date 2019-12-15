using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace onOff
{
    static class Program
    {
        static ServiceController GetInstalledService(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services) if (service.ServiceName == serviceName) return service;
            return null;
        }
        static void displayServiceState()
        {
            ServiceController sc = GetInstalledService(ring1ServiceName);
            if (sc!=null&&sc.Status == ServiceControllerStatus.Running) notifyIcon.Icon = icoChmura;
            else notifyIcon.Icon = icoBrakUsługi;
        }
        static void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) uninstallService();
            if (e.Button == MouseButtons.Left) installService();
        }
        private static void installService()
        {
            Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\instsrv.exe", ring1ServiceName + " " + gamePath + "usługa\\bin\\usługa.exe") { WindowStyle = ProcessWindowStyle.Hidden, UseShellExecute = true, Verb = "runas" });
            Thread.Sleep(500);
            ServiceController sc = GetInstalledService(ring1ServiceName);
            if (sc != null && sc.Status != ServiceControllerStatus.Running) sc.Start();
            Thread.Sleep(500);
            displayServiceState();
        }

        private static void uninstallService()
        {
            ServiceController sc = GetInstalledService(ring1ServiceName);
            if (sc != null && sc.Status == ServiceControllerStatus.Running) sc.Stop();
            Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\instsrv.exe", ring1ServiceName + " REMOVE") { WindowStyle = ProcessWindowStyle.Hidden, UseShellExecute = true, Verb = "runas" });
            displayServiceState();
        }
        static void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) Environment.Exit(0);
        }
        static NotifyIcon notifyIcon;
        static System.Timers.Timer timer = new System.Timers.Timer();
        static private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            displayServiceState();
        }
        static void setNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            displayServiceState();
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;

        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            setNotifyIcon();
            Application.Run();
        }
        static string gamePath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf('\\', Directory.GetCurrentDirectory().LastIndexOf('\\') - 1) + 1);
        static Icon icoChmura = new Icon(gamePath + "rysunki\\chmura.ico");
        static Icon icoBrakUsługi = new Icon(gamePath + "rysunki\\usługa.ico");
        static string ring1ServiceName = "_graŻabkaUsługa";
    }
}

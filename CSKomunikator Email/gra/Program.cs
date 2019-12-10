using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Ionic.Zip;
using System.Security.AccessControl;

namespace gra
{
    static class Program
    {
        private static void taskBarGraj_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem GrajMenuItem = (ToolStripMenuItem)sender;
            if (GrajMenuItem.Text == "Graj") {
                if (oknoGry == null) oknoGry = new glowne();
                oknoGry.Show();
            }
            else
            {
                if (getMail == null) getMail = new GetMail();
                getMail.WantedInbox(GrajMenuItem.Text);
            }
        }
        public static NotifyIcon notifyIcon;
        public static RegistryKey emailLoginsKey;
        static void setNotifyIcon() {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = żabaIcon;
            notifyIcon.Visible = true;
            ToolStripMenuItem GrajMenuItem = new ToolStripMenuItem();
            GrajMenuItem.Text = "Graj";
            GrajMenuItem.Click += new EventHandler(taskBarGraj_Click);
            ContextMenuStrip taskBarIconMenuStrip = new ContextMenuStrip();
            taskBarIconMenuStrip.Items.AddRange(new ToolStripItem[] { GrajMenuItem });
            if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(żabkaMailLogins, true)) == null) emailLoginsKey = Registry.CurrentUser.CreateSubKey(żabkaMailLogins);
            foreach (string emailName in emailLoginsKey.GetSubKeyNames()) {
                GrajMenuItem = new ToolStripMenuItem();
                GrajMenuItem.Text = emailName;
                GrajMenuItem.Click += new EventHandler(taskBarGraj_Click);
                taskBarIconMenuStrip.Items.Add(GrajMenuItem);
            }
            notifyIcon.ContextMenuStrip = taskBarIconMenuStrip;
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
        }
        private static void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                if (getMail != null) getMailDmuchawce.noMailsAgain();
                if (zmeczenieGrającego != null) zmeczenieGrającego.DiscardRubbish();
            }
            if (e.Button == MouseButtons.Left)
            {
                if (getMail != null) getMailDmuchawce.ReceiveUnreadMailsAgain();
                if (oknoGry==null|| oknoGry.Visible==false) {
                    if (sendMail == null) sendMail = new SendMail();
                    sendMail.Show();
                }
            }
        }
        private static void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) Environment.Exit(0);
            if (e.Button == MouseButtons.Left) {
                if (sendMail != null) sendMail.Hide();
                if (oknoGry == null) oknoGry = new glowne();
                oknoGry.Show();
            }
        }
        public static void becomeAdmin() {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                Process.Start(new ProcessStartInfo(Application.ExecutablePath.ToString(), "") { UseShellExecute = true, Verb = "runas" });
                Environment.Exit(0);
            }
        }
        public static void installDriverService()
        {
            ServiceController sc = GetServiceInstalled(ring1ServiceName);
            if (sc == null|| sc.Status != ServiceControllerStatus.Running) {
                becomeAdmin();
                Process.Start(new ProcessStartInfo(gamePath + "onOff\\bin\\Debug\\instsrv.exe", ring1ServiceName + " " + gamePath + "usługa\\bin\\Debug\\usługa.exe") { WindowStyle = ProcessWindowStyle.Hidden, UseShellExecute = true, Verb = "runas" });
                Thread.Sleep(500);//daj czas na wgranie
                sc = GetServiceInstalled(ring1ServiceName);
                if (sc == null) MessageBox.Show("za mało czasu na wgranie ring1");
                sc.Start();
            }
        }
        public static void InstallGameGraŻabka()
        {//no admin
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue("graŻabka", Application.ExecutablePath.ToString());
        }
        public static ServiceController GetServiceInstalled(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services) if (service.ServiceName == serviceName) return service;
            return null;
        }
        public static void processCommandLineArguments(string[] args) {
            installZipper();
            if (args.Length == 0) return;
            if (args.Length != 1) MessageBox.Show(args.Length.ToString());
            bool areAllZipFiles = true;
            foreach (string arg in args) {
                if (Directory.Exists(arg) || (File.Exists(arg) && !arg.ToLower().EndsWith(".zip"))) {
                    areAllZipFiles = false;
                    break;
                } 
            }
            if (areAllZipFiles) unzipAll(args);
            else zipAll(args);
            Environment.Exit(0);
        }

        private static void installZipper()
        {
            RegistryKey śćskaczKey = Registry.CurrentUser.OpenSubKey("Software\\Classes", true);
            śćskaczKey= śćskaczKey.CreateSubKey("directory");
            śćskaczKey= śćskaczKey.CreateSubKey("shell");
            śćskaczKey=śćskaczKey.CreateSubKey("śćskacz");
            śćskaczKey.SetValue("icon", gamePath + "gra\\żaba.ico", RegistryValueKind.String);
            śćskaczKey = śćskaczKey.CreateSubKey("command");
            śćskaczKey.SetValue("", Application.ExecutablePath.ToString()+ " %1");
            śćskaczKey = Registry.CurrentUser.OpenSubKey("Software\\Classes", true);
            śćskaczKey = śćskaczKey.CreateSubKey("*");
            śćskaczKey = śćskaczKey.CreateSubKey("shell");
            śćskaczKey = śćskaczKey.CreateSubKey("śćskacz");
            śćskaczKey.SetValue("icon", gamePath + "gra\\żaba.ico", RegistryValueKind.String);
            śćskaczKey = śćskaczKey.CreateSubKey("command");
            śćskaczKey.SetValue("", Application.ExecutablePath.ToString() + " %1");
        }

        private static void zipAll(string[] args)
        {
            for (int i = 0; i < args.Length; i++) args[i] += "\\";
            if (zipUnzip.zipAll(args)=="") MessageBox.Show("błądTUzipAllprogramCS");
        }

        private static void unzipAll(string[] args)
        {
            foreach(string arg in args) using (ZipFile zip = ZipFile.Read(arg)) zip.ExtractAll(arg.Substring(0, arg.Length - 4));
        }
        static void showIt()
        {
            MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("mmf1.data");
            MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, 1024);
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] buffer = new byte[1024];
            mmvStream.Read(buffer, 0, 1024);
            int val1 = (int)formatter.Deserialize(new MemoryStream(buffer));
            MessageBox.Show(val1.ToString());
        }
        static MemoryMappedFile mmfKernel;
        static MemoryMappedViewStream mmvStreamKernel;
        static void doFirst()
        {
            mmfKernel = MemoryMappedFile.CreateOrOpen("mmf1.data", 1024, MemoryMappedFileAccess.ReadWrite);
            mmvStreamKernel = mmfKernel.CreateViewStream(0, 1024);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mmvStreamKernel, (int)34);
            mmvStreamKernel.Seek(0, SeekOrigin.Begin);
        }
        [STAThread]
        static void Main(string[] args)
        {
            //semaphoreObject.WaitOne();

            /*            SemaphoreSecurity semSec = new SemaphoreSecurity();
                        SemaphoreAccessRule rule = new SemaphoreAccessRule("OKARDAKONUR\\okardak", SemaphoreRights.FullControl, AccessControlType.Allow);
                        semSec.AddAccessRule(rule);
                        Semaphore semaphoreObjec2t = new Semaphore(1, 1, @"Global\graŻabkaSemaphoree",out czyZnalazłSemaphore,semSec);
            string user = Environment.UserDomainName + "\\" + Environment.UserName;
                        semaphoreObject.WaitOne();
            MessageBox.Show(user);

            mmfKernel = MemoryMappedFile.CreateFromFile("mmf1.data");
            mmvStreamKernel = mmfKernel.CreateViewStream(0, 1024);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mmvStreamKernel, (int)34);
            mmvStreamKernel.Seek(0, SeekOrigin.Begin);
            */
//            MessageBox.Show("jj");
  //              Environment.Exit(0);
            //showIt();
            //doFirst();
            processCommandLineArguments(args);
            if (isAlreadyOpened()) Environment.Exit(0);
            InstallGameGraŻabka();
            installDriverService();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            setNotifyIcon();
            getMailDmuchawce = new GetMailDmuchawce(notifyIcon);//wyświetlańe dmóchawców
            Application.Run();
        }
        public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0) throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0) throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        public static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0) throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0) throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0) throw new ArgumentNullException("IV");
            string plaintext = null;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        public static string[] pathsToFiles(string rootPath, string fileName)
        {
            string[] filePaths = Directory.GetFiles(rootPath, fileName, SearchOption.AllDirectories);
            return filePaths;
        }
        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);
        public static void WNDtoPNG(IntPtr hwnd, string fName)
        {
            IntPtr wndDC = GetDC(hwnd);
            Graphics g1 = Graphics.FromHdc(wndDC);
            Rectangle rect;
            GetWindowRect(hwnd, out rect);
            int x = rect.Width - rect.X, y = rect.Height - rect.Y;
            Image myImage = new Bitmap(x, y, g1);
            Graphics g2 = Graphics.FromImage(myImage);
            IntPtr dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();
            BitBlt(dc2, 0, 0, x, y, dc1, 0, 0, 13369376);
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);
            myImage.Save(fName, ImageFormat.Png);
        }
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        public static void DesktopBMP(string fName)
        {
            WNDtoPNG(GetDesktopWindow(), fName);
        }
        public static string getWebPage(string from)
        {
            var request = (HttpWebRequest)WebRequest.Create(from);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        public static bool isAlreadyOpened()
        {
            Process[] processTable = Process.GetProcesses();
            Process current = Process.GetCurrentProcess();
            foreach (Process pr in processTable)
            {
                try
                {
                    if (pr.MainModule.FileName == current.MainModule.FileName && pr.Id != current.Id) return true;
                }
                catch (Exception ex)
                {
                }
            }
            return false;
        }
        public static string gamePath = Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\') - 1) - 1) - 1) + 1);
        public static DataClasses1DataContext database = new DataClasses1DataContext();
        public static string loggedUser = "";
        public static Icon żabaIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        public static string żabkaMailLogins = "żabkaMail";
        public static long DopuszczalneZmęczenie = 1024*1024;
        public static zmeczenieGracza zmeczenieGrającego = new zmeczenieGracza();
        public static SendMail sendMail;
        public static GetMail getMail;
        public static GetMailDmuchawce getMailDmuchawce;
        public static glowne oknoGry;
        public static string ring1ServiceName = "_graŻabkaUsługa";
        }
}

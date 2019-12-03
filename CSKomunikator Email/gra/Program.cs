using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace gra
{
    static class Program
    {
        private static void taskBarGraj_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem GrajMenuItem=(ToolStripMenuItem)sender;
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
            ContextMenuStrip taskBarIconMenuStrip= new ContextMenuStrip();
            taskBarIconMenuStrip.Items.AddRange(new ToolStripItem[] {GrajMenuItem});
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
                if (getMail != null) getMailDmuchawce.ReceiveUnreadMailsAgain();
            }
            if (e.Button == MouseButtons.Left)
            {
                if(sendMail==null) sendMail = new SendMail();
                sendMail.Show();
            }
        }
        private static void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) Environment.Exit(0);
            if (e.Button == MouseButtons.Left) {
                //podwójnie lewy              
            }
        }
        [STAThread]
        static void Main()
        {
            if (isAlreadyOpened()) Environment.Exit(0);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            setNotifyIcon();
            getMailDmuchawce = new GetMailDmuchawce(notifyIcon);//wyświetlańe dmóchawców
            for (;;) Application.DoEvents();
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
        public static string gamePath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf('\\', Directory.GetCurrentDirectory().LastIndexOf('\\', Directory.GetCurrentDirectory().LastIndexOf('\\') - 1) - 1) + 1);
        public static DataClasses1DataContext database = new DataClasses1DataContext();
        public static string loggedUser = "";
        public static Icon żabaIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        public static string żabkaMailLogins = "żabkaMail";
        public static string zmęczeńeGdzie = "zmęczeńe";
        public static long DopuszczalneZmęczenie = 1024*1024;
        public static zmeczenieGracza zmeczenieOkno = new zmeczenieGracza(); //zmęczeńeGdzie = "zmęczeńe"
        public static SendMail sendMail;
        public static GetMail getMail;
        public static GetMailDmuchawce getMailDmuchawce;
        public static glowne oknoGry;
        }
}

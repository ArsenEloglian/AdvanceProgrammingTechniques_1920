using EAGetMail;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gra
{
    class GetMailDmuchawce
    {
        RegistryKey emailLoginsKey;
        NotifyIcon notifyIcon;
        public GetMailDmuchawce(NotifyIcon _notifyIcon)//wyświetlanie dmóchawców
        {
            notifyIcon = _notifyIcon;
            if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(Program.żabkaMailLogins, true)) == null) emailLoginsKey = Registry.CurrentUser.CreateSubKey(Program.żabkaMailLogins);
            ReceiveMails();
            SetDisplayTimer();
        }
        Timer timer;
        void SetDisplayTimer()
        {
            unreadMailIndex = -1;
            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
            timer.Start();
        }
        int unreadMailIndex;
        UnreadMail unreadMail;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (++unreadMailIndex >= unreadMails.Count)
            {
                timer.Enabled = false;
                int zliczByOdznaczyć = 0;
                foreach (UnreadMail unreadMailTemp in unreadMails) if (unreadMailTemp.shouldMarkAsRead) zliczByOdznaczyć++;
                if (zliczByOdznaczyć > 0)
                {
                    try
                    {
                        OdznaczJeNaSerwerze();
                        List<UnreadMail> unreadMailsTemp = new List<UnreadMail>();
                        foreach (UnreadMail unreadMailTemp in unreadMails) if (!unreadMailTemp.shouldMarkAsRead) unreadMailsTemp.Add(unreadMailTemp);
                        unreadMails = unreadMailsTemp;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                unreadMailIndex = -1;
                timer.Enabled = true;
                return;
            }
            if (unreadMailIndex < unreadMails.Count)
            {
                unreadMail = unreadMails.ElementAt(unreadMailIndex);
                notifyIcon.BalloonTipTitle = unreadMail.mailSubject;
                notifyIcon.BalloonTipText = unreadMail.mailFrom;
                notifyIcon.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
                notifyIcon.ShowBalloonTip(4000);
            }
        }
        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            unreadMail.shouldMarkAsRead = true;
        }
        void OdznaczJeNaSerwerze()
        {
            foreach (string emailName in emailLoginsKey.GetSubKeyNames())
            {
                RegistryKey currentLoginKey = emailLoginsKey.OpenSubKey(emailName, true);
                LoginInfo loginInfo = new LoginInfo() { email = emailName, portSMTP = currentLoginKey.GetValue("portSMTP").ToString(), portIMAP = currentLoginKey.GetValue("portIMAP").ToString(), serverSMTP = currentLoginKey.GetValue("serverSMTP").ToString(), serverIMAP = currentLoginKey.GetValue("serverIMAP").ToString(), contracena = Program.DecryptStringFromBytes((byte[])currentLoginKey.GetValue("contracena", RegistryValueKind.Binary), Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")) };
                MailServer mailServer = new MailServer(loginInfo.serverIMAP, loginInfo.email, loginInfo.contracena, ServerProtocol.Imap4) { SSLConnection = true, Port = Int32.Parse(loginInfo.portIMAP) };
                MailClient mailClient = new MailClient("TryIt");
                mailClient.Connect(mailServer);
                MailInfo[] infos = mailClient.GetMailInfos();
                foreach (MailInfo mailInfo in infos) foreach (UnreadMail unreadMailTemp in unreadMails) if (unreadMailTemp.shouldMarkAsRead && mailInfo.UIDL == unreadMailTemp.idUIDL && unreadMailTemp.emailLogin == emailName) mailClient.MarkAsRead(mailInfo, true);
            }
        }
        class LoginInfo
        {
            public string email, contracena, imap, portSMTP, serverSMTP, portIMAP, serverIMAP;
        }
        void AddUnreadMailsFromAccount(string emailName)
        {
            RegistryKey currentLoginKey = emailLoginsKey.OpenSubKey(emailName, true);
            LoginInfo loginInfo = new LoginInfo() { email = emailName, portSMTP = currentLoginKey.GetValue("portSMTP").ToString(), portIMAP = currentLoginKey.GetValue("portIMAP").ToString(), serverSMTP = currentLoginKey.GetValue("serverSMTP").ToString(), serverIMAP = currentLoginKey.GetValue("serverIMAP").ToString(), contracena = Program.DecryptStringFromBytes((byte[])currentLoginKey.GetValue("contracena", RegistryValueKind.Binary), Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")) };
            MailServer mailServer = new MailServer(loginInfo.serverIMAP, loginInfo.email, loginInfo.contracena, ServerProtocol.Imap4) { SSLConnection = true, Port = Int32.Parse(loginInfo.portIMAP) };
            MailClient mailClient = new MailClient("TryIt");
            try
            {
                mailClient.Connect(mailServer);
                MailInfo[] infos = mailClient.GetMailInfos();
                foreach (MailInfo mailInfo in infos) if (!mailInfo.Read)
                    {
                        Mail mail = new Mail("TryIt");
                        mail.Load(mailClient.GetMailHeader(mailInfo));
                        unreadMails.Add(new UnreadMail() { emailLogin = emailName, idUIDL = mailInfo.UIDL, mailFrom = mail.From.Address, mailSubject = mail.Subject });
                    }
            }
            catch (Exception ex)
            {
                unreadMails.Add(new UnreadMail() { emailLogin = "", idUIDL = "", mailFrom = emailName, mailSubject = "błąd połączeńa" });
            }
        }
        class UnreadMail
        {
            public string emailLogin, mailFrom, mailSubject, idUIDL;
            public bool shouldMarkAsRead = false;
        }
        List<UnreadMail> unreadMails = null;
        void ReceiveMails()
        {
            unreadMailIndex = -1;
            unreadMails = new List<UnreadMail>();
            foreach (string emailName in emailLoginsKey.GetSubKeyNames()) AddUnreadMailsFromAccount(emailName);
            return;
        }
        public void ReceiveUnreadMailsAgain()
        {
            timer.Enabled = false;
            ReceiveMails();
            timer.Enabled = true;
        }
    }
}

using System;
using System.Windows.Forms;
using Ionic.Zip;
using System.IO;
using EASendMail;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;
using EAGetMail;
using gra.Properties;

namespace gra
{
    public partial class SendMail : Form
    {
        RegistryKey emailLoginsKey = null;
        RegistryKey emailRecepientsKey = null;
        void InitializeComponentHere()
        {
            Icon = Program.żabaIcon;
            Cursor = new Cursor(Resources.osoitin.Handle);
        }
        public SendMail()
        {
            InitializeComponent();
            InitializeComponentHere();
            if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(Program.żabkaMailLogins, true)) == null) emailLoginsKey = Registry.CurrentUser.CreateSubKey(Program.żabkaMailLogins);
            if ((emailRecepientsKey = Registry.CurrentUser.OpenSubKey("żabkaRecepients", true)) == null) emailRecepientsKey = Registry.CurrentUser.CreateSubKey("żabkaRecepients");
            fillCombobox();
            fillRecepientsEmails();
            checkRegisterForRowSizes();
        }
        public void replyThisOne(string thisOne) {
            toEmailLogins.Text = thisOne;
        }
        void fillRecepientsEmails() {
            toEmailLogins.Items.Clear();
            toEmailLogins.Items.AddRange(emailRecepientsKey.GetValueNames());
        }
        void checkRegisterForRowSizes()
        {
            int rodzajOkna = (int)emailLoginsKey.GetValue("SMTProdzajOkna");
            if (rodzajOkna == 1) pełneOkno = true;
            else pełneOkno = false;
            decideUponWindowRows();
        }
        void fillAllFields() {
            RegistryKey currentLoginKey = emailLoginsKey.OpenSubKey(fromEmailLogins.SelectedItem.ToString(), true);
            tbFROM.Text = fromEmailLogins.SelectedItem.ToString();
            tbPASSWORD.Text = Program.DecryptStringFromBytes((byte[])currentLoginKey.GetValue("contracena", RegistryValueKind.Binary), Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456"));
            txtPortSMTP.Text = currentLoginKey.GetValue("portSMTP").ToString();
            txtServerSMTP.Text = currentLoginKey.GetValue("serverSMTP").ToString();
            txtPortIMAP.Text = currentLoginKey.GetValue("portIMAP").ToString();
            txtServerIMAP.Text = currentLoginKey.GetValue("serverIMAP").ToString();
        }
        void fillCombobox()
        {
            fromEmailLogins.Items.Clear();
            foreach (string emailName in emailLoginsKey.GetSubKeyNames()) fromEmailLogins.Items.Add(emailName);
            if (fromEmailLogins.Items.Count != 0) fromEmailLogins.Enabled = true;
            else fromEmailLogins.Enabled = false;
        }
        private void SendMail_Load(object sender, EventArgs e)
        {

        }
        void addRecepient(string toEmailLogins) {
            foreach (string toEmailLogin in toEmailLogins.Split(new char[] { ',', ';' })) emailRecepientsKey.SetValue(toEmailLogin, "", RegistryValueKind.String);
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            using (ZipFile zip = ZipFile.Read(filenames[0]))
            {
                zip.ExtractAll(Path.GetDirectoryName(filenames[0]));
            }
        }
        private void emailLogins_Click(object sender, EventArgs e)
        {
            btnSEND.Text = "SFOCIARE";
        }

        private void tbTO_Click(object sender, EventArgs e)
        {
            btnSEND.Text = "SFOCIARE";
        }

        private void txtServerIMAP_Click(object sender, EventArgs e)
        {
            btnSEND.Text = "SFOCIARE";
        }

        private void txtPortIMAP_Click(object sender, EventArgs e)
        {
            btnSEND.Text = "SFOCIARE";
        }

        private void emailLogins_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillAllFields();
        }
        bool pełneOkno = true;
        void setWindowRows(int[] RowStylesValues) {

            for (int i = 0; i < 8; i++) tableLayoutPanel1.RowStyles[i].Height = RowStylesValues[i];
        }
        void decideUponWindowRows() {
            if (pełneOkno)
            {
                emailLoginsKey.SetValue("SMTProdzajOkna", 1, RegistryValueKind.DWord);
                setWindowRows(new int[] { 6, 31, 5, 5, 27, 6, 8, 12 });
            }
            else
            {
                emailLoginsKey.SetValue("SMTProdzajOkna", 0, RegistryValueKind.DWord);
                setWindowRows(new int[] { 6, 0, 5, 5, 58, 6, 8, 12 });
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            pełneOkno = !pełneOkno;
            decideUponWindowRows();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            pełneOkno = !pełneOkno;
            decideUponWindowRows();
        }

        private void SendMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.sendMail = null;
        }
        string baseDirectory(string fPath) {
            if (File.GetAttributes(fPath).HasFlag(FileAttributes.Directory)) return fPath;//skiva lub teczka
            string tmpPath = fPath.Substring(0, fPath.LastIndexOf("\\"));
            return tmpPath.Substring(0, tmpPath.LastIndexOf("\\")+1);
        }
        string cuttedPathToDirectory(string cuttedPath) {
            int ostatniaTeczka = cuttedPath.LastIndexOf("\\");
            if (ostatniaTeczka == -1) return "";
            return cuttedPath.Substring(0,ostatniaTeczka+1);
        }
        string getCommonPath(List<string> allPaths) {
            if (allPaths.Count == 0) return "";
            string commonPath = baseDirectory(allPaths[0]);
            if (allPaths.Count == 1) return commonPath;
            for (int i = 1; i < allPaths.Count; i++)
            {
                int j;
                string tmpPath = baseDirectory(allPaths[i]);
                for (j = 0; j < commonPath.Length && j < tmpPath.Length && char.ToUpper(commonPath[j]) == char.ToUpper(tmpPath[j]); j++) ;
                if (j < commonPath.Length) commonPath = cuttedPathToDirectory(commonPath.Substring(0, j));
            }
            return commonPath;
        }
        private void dołączZałączńkZip_Click(object sender, EventArgs e)
        {
            List<string> zwróćWybrane = new List<string>();
            FilesOrFolders fOrF = new FilesOrFolders(out zwróćWybrane);
            string commonPath = getCommonPath(zwróćWybrane);
            if (commonPath != "") {
                try
                {
                    string lastDirectory = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(commonPath);
                    ZipFile zip = new ZipFile();
                    foreach (string wybrany in zwróćWybrane)
                    {
                        if (File.GetAttributes(wybrany).HasFlag(FileAttributes.Directory))
                        {
                            if (commonPath == wybrany) zip.AddDirectory(wybrany);
                            else zip.AddDirectory(wybrany, wybrany.Substring(commonPath.Length, wybrany.Length - commonPath.Length - 1));
                        }else zip.AddFile(wybrany.Substring(commonPath.Length, wybrany.Length - 1 - commonPath.Length));
                    }
                    Directory.SetCurrentDirectory(Path.GetPathRoot(lastDirectory));
                    string[] zipFileNames = commonPath.Split(new char[] {'\\'});
                    string zipFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + zipFileNames[zipFileNames.Length - 2].Replace(':', '_') + ".zip";
                    listBox1.Items.Add(zipFileName);
                    zip.Save(zipFileName);
                }
                catch (Exception ex) {
                    textBox1.Text = "BŁĄD";
                }
            }
            fOrF.Close();
            return;
        }
        private void btnUSUN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox1.Refresh();
        }

        private void btnSEND_Click(object sender, EventArgs e)
        {
            btnSEND.Text = "????";
            try
            {
                //------------- wysyłanie
                SmtpMail smtpMail = new SmtpMail("TryIt") { From = tbFROM.Text, To = toEmailLogins.Text, Subject = tbSUBJECT.Text, TextBody = tbMESSAGE.Text };
                foreach (string attachment in listBox1.Items) smtpMail.AddAttachment(attachment);
                SmtpServer smtpServer = new SmtpServer(txtServerSMTP.Text) { Port = int.Parse(txtPortSMTP.Text), ConnectType = SmtpConnectType.ConnectSSLAuto, User = tbFROM.Text, Password = tbPASSWORD.Text };
                new SmtpClient().SendMail(smtpServer, smtpMail);
                addRecepient(toEmailLogins.Text);
                //------------- wysyłanie
                if (pełneOkno)
                {
                    //------------- odbiór
                    MailServer mailServer = new MailServer(txtServerIMAP.Text, tbFROM.Text, tbPASSWORD.Text, EAGetMail.ServerProtocol.Imap4) { SSLConnection = true, Port = Int32.Parse(txtPortIMAP.Text) };
                    MailClient mailClient = new MailClient("TryIt");
                    mailClient.Connect(mailServer);
                    MailInfo[] infos = mailClient.GetMailInfos();
                    RegistryKey currentLoginKey = emailLoginsKey.CreateSubKey(tbFROM.Text);
                    currentLoginKey.SetValue("contracena", Program.EncryptStringToBytes(tbPASSWORD.Text, Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")), RegistryValueKind.Binary);
                    currentLoginKey.SetValue("portSMTP", txtPortSMTP.Text, RegistryValueKind.String);
                    currentLoginKey.SetValue("serverSMTP", txtServerSMTP.Text, RegistryValueKind.String);
                    currentLoginKey.SetValue("portIMAP", txtPortIMAP.Text, RegistryValueKind.String);
                    currentLoginKey.SetValue("serverIMAP", txtServerIMAP.Text, RegistryValueKind.String);
                    fillCombobox();
                    //------------- odbiór
                }
                btnSEND.Text = "POWODZENIE";
                fillRecepientsEmails();
            }
            catch (Exception ex)
            {
                btnSEND.Text = ex.Message;
            }

        }

        private void wypakujZip_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Title = "wybierz plik ZIP" , RestoreDirectory = true , InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) , Filter = "ZIPY (*.zip)|*.zip" };
            if (openFileDialog.ShowDialog() == DialogResult.OK) using (ZipFile zip = ZipFile.Read(openFileDialog.FileName)) zip.ExtractAll(openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Windows.Forms;
using Ionic.Zip;
using EASendMail;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;
using EAGetMail;
using gra.Properties;
using System.Linq;
using System.Drawing;

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
            fromEmailLogins.Cursor = Cursor;
            textBox1.Cursor = Cursor;
            textBox2.Cursor = Cursor;
            textBox5.Cursor = Cursor;
            textBox3.Cursor = Cursor;
            textBox7.Cursor = Cursor;
            textBox4.Cursor = Cursor;
            textBox8.Cursor = Cursor;
            textBox10.Cursor = Cursor;
            tbFROM.Cursor = Cursor;
            tbPASSWORD.Cursor = Cursor;
            txtPortSMTP.Cursor = Cursor;
            txtServerSMTP.Cursor = Cursor;
            txtPortIMAP.Cursor = Cursor;
            txtServerIMAP.Cursor = Cursor;
            toEmailLogins.Cursor = Cursor;
            tbSUBJECT.Cursor = Cursor;
            tbMESSAGE.Cursor = Cursor;
            btnGROMADŹ.Cursor = Cursor;
            btnSEND.Cursor = Cursor;
            btnGROMADŹ.BackgroundImageLayout = ImageLayout.Stretch;
            btnGROMADŹ.BackgroundImage= new Bitmap(Program.gamePath + "rysunki\\gromadź.png");
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
        private void dołączZałączńkZip_Click(object sender, EventArgs e)
        {
            List<string> zwróćWybrane = new List<string>();
            FilesOrFolders fOrF = new FilesOrFolders(out zwróćWybrane);
            string zipFileName = Program.zipAll(zwróćWybrane.ToArray());
            if (zipFileName != "") listBox1.Items.Add(zipFileName);
            else textBox1.Text = "BŁĄD";
            fOrF.Close();
            return;
        }
        private void btnUSUN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox1.Refresh();
        }
        private void wypakujZip_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Title = "wybierz plik ZIP", RestoreDirectory = true, InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Filter = "ZIPY (*.zip)|*.zip" };
            if (openFileDialog.ShowDialog() == DialogResult.OK) using (ZipFile zip = ZipFile.Read(openFileDialog.FileName)) zip.ExtractAll(openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4));
        }

        private void toEmailLogins_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void toEmailLogins_TextChanged(object sender, EventArgs e)
        {
            if (!shouldProcessToEmailLogins_TextChanged) return;
            shouldProcessToEmailLogins_TextChanged = false;
            string[] receivers = toEmailLogins.Text.Split(new char[] { ';', ',' });
            string lastReceiver = receivers[receivers.Length - 1];
            if (lastReceiver == "") return;
            string allReceivers = "";
            foreach (string previousReceiver in receivers) if (previousReceiver != lastReceiver) allReceivers += previousReceiver + ",";
            foreach (string toEmailLogin in toEmailLogins.Items) {
                if (toEmailLogin.StartsWith(lastReceiver)&&!receivers.Contains(toEmailLogin)) {
                    toEmailLogins.Text = allReceivers + toEmailLogin;
                    toEmailLogins.SelectionStart = allReceivers.Length + lastReceiver.Length;
                    toEmailLogins.SelectionLength = toEmailLogin.Length - lastReceiver.Length;
                    break;
                }
            }
        }
        string[] discardDuplicates(string[] receivers)
        {
            List<string> listOfReceivers = new List<string>();
            foreach (string receiver in receivers) if (!listOfReceivers.Contains(receiver)) listOfReceivers.Add(receiver);
            return listOfReceivers.ToArray();
        }
        string joinReceivers(string[] receivers) {
            string allReceivers = "";
            foreach (string receiver in receivers) if (receiver != "") allReceivers += receiver + ",";
            if (allReceivers.Length > 0) allReceivers = allReceivers.Substring(0, allReceivers.Length - 1);
            return allReceivers;
        }
        bool shouldProcessToEmailLogins_TextChanged = false;
        private void toEmailLogins_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((int)e.KeyChar == 8|| (int)e.KeyChar == 27) return;//not bcsp||esc
            if ((int)e.KeyChar == 13) {
                toEmailLogins.SelectionStart = toEmailLogins.Text.Length;
                toEmailLogins.SelectionLength = 0;
                return;
            }
            //if 13 ENTER
            shouldProcessToEmailLogins_TextChanged = true;
        }
        private void toEmailLogins_KeyUp(object sender, KeyEventArgs e)
        {
            string proposedText = joinReceivers(discardDuplicates(toEmailLogins.Text.Split(new char[] { ';', ',' })));
            if (toEmailLogins.Text.Length > 0 && (toEmailLogins.Text[toEmailLogins.Text.Length - 1] == ';' || toEmailLogins.Text[toEmailLogins.Text.Length - 1] == ',')) proposedText += ',';
            if (toEmailLogins.Text != proposedText)
            {
                toEmailLogins.Text = proposedText;
                toEmailLogins.SelectionStart = toEmailLogins.Text.Length;
                toEmailLogins.SelectionLength = 0;
            }
        }
        bool czyGromadźić = false;
        Imap4Folder znajdźTeczkęIMAP4(MailClient mailClient,string nazwaTeczki)
        {
            Imap4Folder imap4Folder = new Imap4Folder(nazwaTeczki);
            if (!mailClient.ExistFolder(imap4Folder)) imap4Folder = mailClient.CreateFolder(null, nazwaTeczki);
            return imap4Folder;
        }
        private void btnSENDwtórny_Click(object sender, EventArgs e)
        {
            btnSEND.Text = "????";
            try {
                //------------- wysyłanie
                SmtpMail smtpMail = new SmtpMail("TryIt") { From = tbFROM.Text, To = toEmailLogins.Text + "," + tbFROM.Text, Subject = tbSUBJECT.Text, TextBody = tbMESSAGE.Text };
                foreach (string attachment in listBox1.Items) smtpMail.AddAttachment(attachment);
                SmtpServer smtpServer = new SmtpServer(txtServerSMTP.Text) { Port = int.Parse(txtPortSMTP.Text), ConnectType = SmtpConnectType.ConnectSSLAuto, User = tbFROM.Text, Password = tbPASSWORD.Text };
                new SmtpClient().SendMail(smtpServer, smtpMail);
                addRecepient(toEmailLogins.Text);
                //------------- wysyłanie
                    //------------- odbiór
                    MailServer mailServer = new MailServer(txtServerIMAP.Text, tbFROM.Text, tbPASSWORD.Text, EAGetMail.ServerProtocol.Imap4) { SSLConnection = true, Port = Int32.Parse(txtPortIMAP.Text) };
                    MailClient mailClient = new MailClient("TryIt");
                    mailClient.Connect(mailServer);
                    MailInfo[] infos = mailClient.GetMailInfos();
                Imap4Folder właściwaTeczka = null;
                if (czyGromadźić) właściwaTeczka = znajdźTeczkęIMAP4(mailClient, "Gromadzone");
                else właściwaTeczka = znajdźTeczkęIMAP4(mailClient, "Sent");
                btnSEND.Text = "W ODBIORCZEJ?";
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
                        //MessageBox.Show(messageID+"-"+ smtpMail.MessageID+"-"+ (messageID == smtpMail.MessageID));
                        if (messageID == smtpMail.MessageID){
                            mailClient.Move(mailInfo, właściwaTeczka);
                            btnSEND.Text = "POWODZENIE";
                            break;
                            }
                        }
                    RegistryKey currentLoginKey = emailLoginsKey.CreateSubKey(tbFROM.Text);
                    currentLoginKey.SetValue("contracena", Program.EncryptStringToBytes(tbPASSWORD.Text, Encoding.ASCII.GetBytes("1234567890123456"), Encoding.ASCII.GetBytes("1234567890123456")), RegistryValueKind.Binary);
                    currentLoginKey.SetValue("portSMTP", txtPortSMTP.Text, RegistryValueKind.String);
                    currentLoginKey.SetValue("serverSMTP", txtServerSMTP.Text, RegistryValueKind.String);
                    currentLoginKey.SetValue("portIMAP", txtPortIMAP.Text, RegistryValueKind.String);
                    currentLoginKey.SetValue("serverIMAP", txtServerIMAP.Text, RegistryValueKind.String);
                    fillCombobox();
                    //------------- odbiór
                
                fillRecepientsEmails();
            } catch (Exception ex) { btnSEND.Text = ex.Message;}
        }

        private void btnGROMADŹ_Click(object sender, EventArgs e)
        {
            czyGromadźić = true;
            btnSENDwtórny_Click(sender,e);
            czyGromadźić = false;
        }

        private void fromEmailLogins_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12) {
                RegistryKey emailLoginsKey;
                if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(Program.żabkaMailLogins, true)) != null&&fromEmailLogins.Text!=null) emailLoginsKey.SetValue("alCorreo", fromEmailLogins.Text);
            }
        }
    }
}

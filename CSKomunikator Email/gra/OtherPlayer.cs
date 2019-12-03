using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace gra
{
    public partial class OtherPlayer : Form
    {
        RegistryKey gameKey = Registry.CurrentUser.OpenSubKey("żabka", true);
        void fillCombobox()
        {
            login.Items.Clear();
            String[] players = gameKey.GetValueNames();
            foreach (string player in players)
            {
                login.Items.Add(player);
            }
            if (login.Items.Count != 0)
            {
                login.SelectedIndex = 0;
                bLogin.Enabled = true;
            }
        }
        void InitializeComponentHere()
        {
            Icon = Program.żabaIcon;
        }
        public OtherPlayer()
        {
            InitializeComponent();
            InitializeComponentHere();
        }
        glowne mainForm = null;
        public OtherPlayer(glowne formMain)
        {
            mainForm = formMain;
            if (Program.loggedUser!="")
            {
                mainForm.changeToLogin();
                Close();
            }
            else
            {
                InitializeComponent();
                if (gameKey == null) gameKey = Registry.CurrentUser.CreateSubKey("żabka");
                fillCombobox();
                Show();
            }
        }

        private void OtherPlayer_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (tbFIND.Text=="") return;
        }
        private void tbbFIND_Click(object sender, EventArgs e)
        {
            int index = 0;
            string temp = rtbFIND.Text;
            rtbFIND.Text = "";
            rtbFIND.Text = temp;
            while (index<rtbFIND.Text.LastIndexOf(tbFIND.Text))
            {
                rtbFIND.Find(tbFIND.Text,index,rtbFIND.TextLength,RichTextBoxFinds.None);
                rtbFIND.SelectionBackColor = Color.Gray;
                rtbFIND.SelectionColor = Color.Red;
                rtbFIND.SelectionFont = new Font("MV Boli", 12, FontStyle.Bold);
                index = rtbFIND.Text.IndexOf(tbFIND.Text,index)+1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Program.gamePath;
            openFileDialog1.Filter = "rtf files (*.rtf)|*.rtf";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtbFIND.LoadFile(openFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Program.gamePath;
            saveFileDialog1.Filter = "rtf files (*.rtf)|*.rtf";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtbFIND.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = Encoding.ASCII.GetBytes("1234567890123456");
                myRijndael.IV = Encoding.ASCII.GetBytes("1234567890123456");
                byte[] encrypted = Program.EncryptStringToBytes("\n"+tbPassword.Text, myRijndael.Key, myRijndael.IV);
                gameKey.SetValue(login.Text,encrypted, RegistryValueKind.Binary);
            }
            fillCombobox();
            bCreate.Enabled = false;
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (login.Text == "")
            {
                bCreate.Enabled = false;
                bLogin.Enabled = false;
                login.SelectedIndex = -1;
                return;
            }
            foreach (string player in login.Items)
            {
                if (login.Text == player)
                {
                    bCreate.Enabled = false;
                    bLogin.Enabled = true;
                    login.SelectedItem = login.Text;
                    return;
                }
            }
            bCreate.Enabled = true;
            bLogin.Enabled = false;
            login.SelectedIndex = -1;
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            string decrypted;
            using (Rijndael myRijndael = Rijndael.Create())
            {
                byte[] encrypted = (byte[])gameKey.GetValue(login.SelectedItem as string, RegistryValueKind.Binary);
                myRijndael.Key = Encoding.ASCII.GetBytes("1234567890123456");
                myRijndael.IV = Encoding.ASCII.GetBytes("1234567890123456");
                decrypted = Program.DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);
            }
            string password= decrypted.Split(new char[] { '\n'},2, StringSplitOptions.None)[1]; ;
            if (password==tbPassword.Text)
            {
                bLogin.ForeColor = Color.Black;
                Program.loggedUser = (string)login.SelectedItem;
                mainForm.changeToLogout();
                Close();
            }
            else
            {
                bLogin.ForeColor = Color.Red;
            }
        }
    }
}

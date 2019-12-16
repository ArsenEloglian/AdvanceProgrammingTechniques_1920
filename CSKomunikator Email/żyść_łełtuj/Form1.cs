using System;
using System.IO;
using System.Windows.Forms;

namespace żyść_łełtuj
{
    public partial class Form1 : Form
    {
        void czyśćOgólnie(string teczkaŚcieżka, string[] inneTeczki, string[] innePliki) {
            try {
                Directory.Delete(teczkaŚcieżka + ".vs", true);
            } catch (Exception ex) { }
            try
            {
                Directory.Delete(teczkaŚcieżka + "obj", true);
            } catch (Exception ex) { }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka).GetFiles("*.ldf"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka).GetFiles("*.mdf"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka + "bin\\").GetFiles("*.ldf"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka + "bin\\").GetFiles("*.mdf"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka+"bin\\").GetFiles("Microsoft.*"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka + "bin\\").GetFiles("System.*"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka + "bin\\").GetFiles("*.pdb"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (FileInfo file in new DirectoryInfo(teczkaŚcieżka + "bin\\").GetFiles("*.config"))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) { }
            }
            foreach (string innaTeczka in inneTeczki) {
                try
                {
                    Directory.Delete(teczkaŚcieżka + innaTeczka,true);
                }
                catch (Exception ex) { }
            }
            foreach (string innyPlik in innePliki)
            {
                try
                {
                    File.Delete(teczkaŚcieżka + innyPlik);
                }
                catch (Exception ex) { }
            }
        }
        void śćeżńjNaczelny(string teczkaZNaczelnego) {
            try{
                Directory.Delete(gamePath + teczkaZNaczelnego, true);
            } catch (Exception ex) { }
        }
        public Form1()
        {
            InitializeComponent();
            śćeżńjNaczelny("packages");
            śćeżńjNaczelny(".vs");
            śćeżńjNaczelny(".git");
            czyśćOgólnie(gamePath+ "żyść_łełtuj\\", new string[]{ }, new string[] { });
            czyśćOgólnie(gamePath+ "weekQuarters\\", new string[] { }, new string[] { "bin\\weekQuarters.dll" });
            czyśćOgólnie(gamePath + "usługa\\", new string[] { "bin\\Release" }, new string[] { });
            czyśćOgólnie(gamePath + "onOff\\", new string[] { }, new string[] { });
            czyśćOgólnie(gamePath + "odumćaćuj\\", new string[] { }, new string[] { });
            czyśćOgólnie(gamePath + "gra\\", new string[] { }, new string[] { });
        }
        string gamePath = Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\', Application.ExecutablePath.ToString().LastIndexOf('\\') - 1) - 1) + 1);

        private void Form1_Load(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) Close();
        }
    }
}

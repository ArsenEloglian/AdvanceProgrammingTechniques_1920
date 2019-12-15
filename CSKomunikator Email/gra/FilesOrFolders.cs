using gra.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gra
{
    public partial class FilesOrFolders : Form
    {
        void InitializeComponentHere()
        {
            Icon = Program.żabaIcon;
            Cursor = new Cursor(Resources.osoitin.Handle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ImageList imageList = new ImageList();
            imageList.Images.Add("zpTNC", Resources.zpTNC);
            imageList.Images.Add("skiva", Resources.skiva);
            imageList.Images.Add("teczka", Resources.teczka);
            imageList.Images.Add("innyPlik", Resources.innyPlik);
            imageList.Images.Add("lenzole", Resources.lenzole);
            imageList.Images.Add("pulpit", Resources.pulpit);
            imageList.Images.Add("usb", Resources.usb);
            imageList.Images.Add("śćągńęte", Resources.śćągńęte);
            imageList.Images.Add("zip", Resources.zip);
            imageList.Images.Add("txt", Resources.txt);
            imageList.Images.Add("bmp", Resources.bmp);
            treeView.ImageList = imageList;
        }
        public FilesOrFolders()
        {
            InitializeComponent();
            InitializeComponentHere();
            TwóżWgląd();
            Show();
        }
        bool filesAndFoldersAreSelected = false;
        public FilesOrFolders(out List<string> zwróćWybrane)
        {
            InitializeComponent();
            InitializeComponentHere();
            TwóżWgląd();
            Show();
            while (!filesAndFoldersAreSelected) {
                Thread.Sleep(200);
                Application.DoEvents();
            }
            zwróćWybrane = wybrane;
        }
        public void TwóżWgląd()
        {
            try
            {
                TreeNode pulpit = new TreeNode("Pulpit",new TreeNode[] { new TreeNode("") });
                pulpit.SelectedImageKey = "pulpit";
                pulpit.ImageKey = "pulpit";
                treeView.Nodes.Add(pulpit);
                foreach (DriveInfo drv in DriveInfo.GetDrives())
                {
                    TreeNode fChild = new TreeNode(drv.Name, new TreeNode[] { new TreeNode("") });
                    if (drv.DriveType == DriveType.CDRom) fChild.ImageKey = "zpTNC";
                    else if (drv.DriveType == DriveType.Removable) fChild.ImageKey = "usb";
                    else if (drv.DriveType == DriveType.Fixed) fChild.ImageKey = "skiva";
                    if (drv.DriveType == DriveType.CDRom) fChild.SelectedImageKey = "zpTNC";
                    else if (drv.DriveType == DriveType.Removable) fChild.SelectedImageKey = "usb";
                    else if (drv.DriveType == DriveType.Fixed) fChild.SelectedImageKey = "skiva";
                    treeView.Nodes.Add(fChild);
                }
                TreeNode lenzole = new TreeNode("Lenzole", new TreeNode[] { new TreeNode("") });
                lenzole.SelectedImageKey = "lenzole";
                lenzole.ImageKey = "lenzole";
                treeView.Nodes.Add(lenzole);
                TreeNode śćągńęte = new TreeNode("Śćągńęte", new TreeNode[] { new TreeNode("") });
                śćągńęte.SelectedImageKey = "śćągńęte";
                śćągńęte.ImageKey = "śćągńęte";
                treeView.Nodes.Add(śćągńęte);
            }
            catch (Exception ex)
            {
            }
        }
        string realPath(string pereGałąź) {
            string[] poszczególneTeczkiPoDrodze = pereGałąź.Split(new char[] { '\\' });
            if (poszczególneTeczkiPoDrodze.GetValue(0).ToString() == "Pulpit")
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                for (int i = 1; i < poszczególneTeczkiPoDrodze.Length; i++) path = path + "\\" + poszczególneTeczkiPoDrodze[i];
                return path+"\\";
            }
            else if (poszczególneTeczkiPoDrodze.GetValue(0).ToString() == "Lenzole")
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                for (int i = 1; i < poszczególneTeczkiPoDrodze.Length; i++) path = path + "\\" + poszczególneTeczkiPoDrodze[i];
                return path + "\\";
            }
            else if (poszczególneTeczkiPoDrodze.GetValue(0).ToString() == "Śćągńęte")
            {
                string path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Downloads");
                for (int i = 1; i < poszczególneTeczkiPoDrodze.Length; i++) path = path + "\\" + poszczególneTeczkiPoDrodze[i];
                return path + "\\";
            }
            else
            {
                string path = poszczególneTeczkiPoDrodze[0];
                for (int i = 1; i < poszczególneTeczkiPoDrodze.Length; i++) if(poszczególneTeczkiPoDrodze[i]!="") path = path + "\\" + poszczególneTeczkiPoDrodze[i];
                return path + "\\";
            }
        }
        public void TwóżPodwgląd(TreeNode pereGałąź)
        {
            try
            {
                DirectoryInfo rootDir= new DirectoryInfo(realPath(pereGałąź.FullPath));
                pereGałąź.Nodes[0].Remove();
                foreach (DirectoryInfo dir in rootDir.GetDirectories())
                {
                    TreeNode node = new TreeNode(dir.Name,2,2);
                    node.Nodes.Add("");
                    pereGałąź.Nodes.Add(node);
                }
                foreach (FileInfo file in rootDir.GetFiles())
                {
                    int whichIcon = 3;
                    if (file.Name.EndsWith(".zip")) whichIcon = 8;
                    else if (file.Name.EndsWith(".txt") || file.Name.EndsWith(".rtf") || file.Name.EndsWith(".doc") || file.Name.EndsWith(".docx")) whichIcon = 9;
                    else if (file.Name.EndsWith(".bmp") || file.Name.EndsWith(".png") || file.Name.EndsWith(".jpg") || file.Name.EndsWith(".gif") || file.Name.EndsWith(".ico")) whichIcon = 10;
                    TreeNode node = new TreeNode(file.Name, whichIcon, whichIcon);
                    pereGałąź.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "") TwóżPodwgląd(e.Node);
        }
        public List<string> wybrane = new List<string>();
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked) wybrane.Add(realPath(e.Node.FullPath));
            else wybrane.Remove(realPath(e.Node.FullPath)); 
        }
        private void wtórnijWgląd(TreeNode osnownaGałąź, bool dźałańe)
        {
            foreach (TreeNode node in osnownaGałąź.Nodes)
            {
                wtórnijWgląd(node, dźałańe);
                node.Checked = dźałańe;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (TreeNode gałąź in treeView.Nodes) wtórnijWgląd( gałąź, false);
            wybrane.Clear();
        }

        private void FilesOrFolders_FormClosing(object sender, FormClosingEventArgs e)
        {
            wybrane.Clear();
            filesAndFoldersAreSelected = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filesAndFoldersAreSelected = true;
        }
        void cancelAllUp(TreeNode pereGałąź) {
            if (pereGałąź != null) {
                pereGałąź.Checked = false;
                cancelAllUp(pereGałąź.Parent);
            } 
        }
        void cancelAllDown(TreeNode gałąź) {
            gałąź.Checked = false;
            foreach (TreeNode childNode in gałąź.Nodes) cancelAllDown(childNode);
        }
        bool samoczynneNastrojki = false;
        private void treeView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (samoczynneNastrojki) return;
            samoczynneNastrojki = true;
            if (e.Node.FullPath.Length <= 3) e.Cancel = true;
            else {
                if (e.Node.Checked == false)
                {
                    cancelAllUp(e.Node.Parent);
                    cancelAllDown(e.Node);
                }
            }
            samoczynneNastrojki = false;
        }
    }
}

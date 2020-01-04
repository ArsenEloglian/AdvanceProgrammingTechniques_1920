using gra.Properties;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace gra
{
    static partial class Program
    {
        private static void taskBarGraj_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem GrajMenuItem = (ToolStripMenuItem)sender;
            if (GrajMenuItem.Text == "Graj")
            {
                if (oknoGry == null) oknoGry = new OknoGry();
                oknoGry.Show();
            }
            else
            {
                if (getMail == null) getMail = new GetMail();
                getMail.WantedInbox(GrajMenuItem.Text);
            }
        }
        static bool[,,] getMarkedQuarters() {
            RegistryKey gameKey = Registry.CurrentUser.CreateSubKey("żabka\\startup", true);
            bool[,,] markedQuarters = new bool[7, 24, 4];
            string sAllMarkedQuarters = (string)gameKey.GetValue("markedQuarters");
            if (sAllMarkedQuarters != null)
            {
                string[] sMarkedQuarters = sAllMarkedQuarters.Split(new char[] { ' ' });
                foreach (string sMarkedQuarter in sMarkedQuarters) if (sMarkedQuarter != "")
                    {
                        string[] markedQuarter = sMarkedQuarter.Split(new char[] { ',' });
                        markedQuarters[int.Parse(markedQuarter[0]), int.Parse(markedQuarter[1]), int.Parse(markedQuarter[2])] = true;
                    }
            }
            gameKey.Close();
            return markedQuarters;
        }
        static bool[,,] markThisQuarter(bool[,,] markedQuarters) {
            RegistryKey gameKey = Registry.CurrentUser.CreateSubKey("żabka\\startup", true);
            DateTime dateTime = DateTime.Now;
            int val0 = ((int)dateTime.DayOfWeek - 1) % 7;
            if (val0 == -1) val0 = 6;
            int val1 = dateTime.Hour;
            int val2 = dateTime.Minute / 15;
                markedQuarters[val0, val1, val2] = true;
                string sAllMarkedQuarters = "";
                for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) if (markedQuarters[x, y, z])
                            {
                                sAllMarkedQuarters += x.ToString() + "," + y.ToString() + "," + z.ToString() + " ";
                            }
                gameKey.SetValue("markedQuarters", sAllMarkedQuarters);
                
            gameKey.Close();
            return markedQuarters;
        }
        static bool[,,] unmarkThisQuarter(bool[,,] markedQuarters)
        {
            RegistryKey gameKey = Registry.CurrentUser.CreateSubKey("żabka\\startup", true);
            DateTime dateTime = DateTime.Now;
            int val0 = ((int)dateTime.DayOfWeek - 1) % 7;
            if (val0 == -1) val0 = 6;
            int val1 = dateTime.Hour;
            int val2 = dateTime.Minute / 15;
            markedQuarters[val0, val1, val2] = false;
            string sAllMarkedQuarters = "";
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) if (markedQuarters[x, y, z])
                        {
                            sAllMarkedQuarters += x.ToString() + "," + y.ToString() + "," + z.ToString() + " ";
                        }
            gameKey.SetValue("markedQuarters", sAllMarkedQuarters);
            gameKey.Close();
            return markedQuarters;
        }
        static void startupChecks()
        {
            bool[,,] markedQuarters = markThisQuarter(getMarkedQuarters()); 
        }
        public static NotifyIcon notifyIcon;
        public static RegistryKey emailLoginsKey;
        static void setNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.aleKokosy;
            notifyIcon.Visible = true;
            ToolStripMenuItem GrajMenuItem = new ToolStripMenuItem();
            GrajMenuItem.Text = "Graj";
            GrajMenuItem.Click += new EventHandler(taskBarGraj_Click);
            ContextMenuStrip taskBarIconMenuStrip = new ContextMenuStrip();
            taskBarIconMenuStrip.Items.AddRange(new ToolStripItem[] { GrajMenuItem });
            if ((emailLoginsKey = Registry.CurrentUser.OpenSubKey(żabkaEmailLoginsKeyName, true)) == null) emailLoginsKey = Registry.CurrentUser.CreateSubKey(żabkaEmailLoginsKeyName);
            foreach (string emailName in emailLoginsKey.GetSubKeyNames())
            {
                GrajMenuItem = new ToolStripMenuItem();
                GrajMenuItem.Text = emailName;
                GrajMenuItem.Click += new EventHandler(taskBarGraj_Click);
                taskBarIconMenuStrip.Items.Add(GrajMenuItem);
            }
            notifyIcon.ContextMenuStrip = taskBarIconMenuStrip;
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            startupChecks();
        }
        private static void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (getMail != null) getMailDmuchawce.noMailsAgain();
                if (zmeczenieGrającego != null) zmeczenieGrającego.DiscardRubbish();
            }
            if (e.Button == MouseButtons.Left)
            {
                if (getMail != null) getMailDmuchawce.ReceiveUnreadMailsAgain();
                if (oknoGry == null || oknoGry.Visible == false)
                {
                    if (sendMail == null) sendMail = new SendMail();
                    sendMail.Show();
                }
            }
        }
        private static void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) Environment.Exit(0);
            if (e.Button == MouseButtons.Left)
            {
                if (sendMail != null) sendMail.Hide();
                //startupSelection startupWindow = new startupSelection();
                //startupWindow.Show();
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        static void checkScrollLock() {
            bool scrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
            if (scrollLock) {
                bool[,,] markedQuarters = markThisQuarter(getMarkedQuarters());
                notifyIcon.Icon = redŻabaIcon;
            }
        }
        public static void notifyIconProcessButton() {
            checkScrollLock();
        }
        static bool didAlready=false;
        public static void notifyIconProcessKey(int code) {
            if (code!=145) checkScrollLock();
            if (code == 3) {
                if (didAlready)
                {
                    startupSelection startupWindow = new startupSelection();
                    startupWindow.Show();
                }
                else {
                    bool[,,] markedQuarters = unmarkThisQuarter(getMarkedQuarters());
                    bool whichIcon = false;
                    for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) if (markedQuarters[x, y, z] == true) whichIcon = true;
                    if (whichIcon) notifyIcon.Icon = redŻabaIcon;
                    else notifyIcon.Icon = żabaIcon;
                    didAlready = true;
                }
            }
            //MessageBox.Show(code.ToString());
        }
    }
}

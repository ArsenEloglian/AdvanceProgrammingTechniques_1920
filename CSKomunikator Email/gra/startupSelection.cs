using Microsoft.Win32;
using System.Windows.Forms;

namespace gra
{
    public partial class startupSelection : Form
    {
        RegistryKey gameKey = Registry.CurrentUser.CreateSubKey("żabka\\startup", true);
        public startupSelection()
        {
            InitializeComponent();
            bool[,,] quarters = new bool[7, 24, 4];
            string sAllQuarters = (string)gameKey.GetValue("quarters");
            if (sAllQuarters != null) {
                string[] sQuarters = sAllQuarters.Split(new char[] { ' ' });
                foreach (string sQuarter in sQuarters) if (sQuarter != "")
                    {
                        string[] quarter = sQuarter.Split(new char[] { ',' });
                        quarters[int.Parse(quarter[0]), int.Parse(quarter[1]), int.Parse(quarter[2])] = true;
                    }
            }
            weekQuarter1.setQuarters(quarters);
            bool[,,] markedQuarters = new bool[7, 24, 4];
            string sAllMarkedQuarters= (string)gameKey.GetValue("markedQuarters");
            if (sAllMarkedQuarters!=null) {
                string[] sMarkedQuarters = sAllMarkedQuarters.Split(new char[] { ' ' });
                foreach (string sMarkedQuarter in sMarkedQuarters) if (sMarkedQuarter != "")
                    {
                        string[] markedQuarter = sMarkedQuarter.Split(new char[] { ',' });
                        markedQuarters[int.Parse(markedQuarter[0]), int.Parse(markedQuarter[1]), int.Parse(markedQuarter[2])] = true;
                    }
            }
            weekQuarter1.setMarkedQuarters(markedQuarters);
        }
        private void weekQuarter1_Load(object sender, System.EventArgs e)
        {

        }

        private void pcStartup_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool[,,] quarters= weekQuarter1.getQuarters();
            string sAllQuarters = "";
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) if (quarters[x, y, z]) {
                          sAllQuarters += x.ToString() + "," + y.ToString() + "," + z.ToString() + " ";
                    }
            gameKey.SetValue("quarters", sAllQuarters);
            bool[,,] markedQuarters = weekQuarter1.getMarkedQuarters();
            string sAllMarkedQuarters = "";
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) if (markedQuarters[x, y, z])
                        {
                            sAllMarkedQuarters += x.ToString() + "," + y.ToString() + "," + z.ToString() + " ";
                        }
            gameKey.SetValue("markedQuarters", sAllMarkedQuarters);
            bool whichIcon = false;
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) if (markedQuarters[x, y, z] == true) whichIcon = true;
            if (whichIcon) Program.notifyIcon.Icon = Program.redŻabaIcon;
            else Program.notifyIcon.Icon = Program.żabaIcon;
        }

        private void startupSelection_Load(object sender, System.EventArgs e)
        {

        }
    }
}

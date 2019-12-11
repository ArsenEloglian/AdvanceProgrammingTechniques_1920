using gra.Properties;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace gra
{
    public partial class Intro : Form
    {
        void InitializeComponentHere()
        {
            Icon = Program.żabaIcon;
            Cursor = new Cursor(Resources.osoitin.Handle);
        }
        public Intro()
        {
            InitializeComponent();
            InitializeComponentHere();
        }

        private void Intro_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = Program.gamePath + "rysunki\\intro.mp4";
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused) {
                Text = axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "/" + axWindowsMediaPlayer1.currentMedia.duration;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Image okImage = new Bitmap("ok.png");
        Image crossImage = new Bitmap("cross.png");
        Image rightArrow = new Bitmap("right_arrow.png");
        Image leftArrow = new Bitmap("left_arrow.png");
        Image upArrow = new Bitmap("up_arrow.png");
        Image downArrow = new Bitmap("down_arrow.png");
        Image startRecording = new Bitmap("start_recording.png");
        Image pauseRecording = new Bitmap("pause_recording.png");
        Image stopRecording = new Bitmap("stop_recording.png");
        Image returnImage = new Bitmap("return.png");
        Image startRecordingM = new Bitmap("start_recordingM.png");
        Image youtubeImage = new Bitmap("youtube.png");
        Image tellyImage = new Bitmap("telly.png");
        Image resizeImage = new Bitmap("resize.png");
        Image openTapImage = new Bitmap("openTap.png");
        Image gearImage = new Bitmap("gear.png");
        Image litter_bin = new Bitmap("litter_bin.png");
        Image banana_litter = new Bitmap("banana_litter.png");
        Image carretImage = new Bitmap("carret.png");
        Image wielkoscImage = new Bitmap("wielkosc.png");
        Image soundImage = new Bitmap("sound.png");
        Image googleImage = new Bitmap("google.png");

        int resizing = 0;
        public Form1()
        {
            InitializeComponent();
			setButton1Icon();
            button2.BackgroundImage = leftArrow;
            button3.BackgroundImage = rightArrow;
            button4.BackgroundImage = upArrow;
            button5.BackgroundImage = downArrow;
            setDisplayIcon();
            button7.BackgroundImage = crossImage;
            button8.BackgroundImage = googleImage;
            button9.BackgroundImage = soundImage;
            displayWindowSize();
        }
        void displayWindowSize() {
            textBox1.Text = Width.ToString()+"x"+Height.ToString();
        }
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        private System.Timers.Timer aTimer = null;
        int sequence = 0;
		double fps=100;
        private void SetTimer()
        {
            File.Delete("recorded.mp4");
            foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "recorded*.png"))
            {
                System.IO.File.Delete(sFile);
            }
            aTimer = new System.Timers.Timer((int)(1000/fps));
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            WNDtoPNG("recorded"+sequence.ToString("D9")+".png");
            if (sequence == 999999999) button3_Click(null, null);
            sequence++;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (resizing == 0)
            {
                resizing++;
                setButton1Icon();
                button6.BackgroundImage = returnImage;
                tableLayoutPanel2.Controls.Add(button6);
            }
            else if (resizing == 1)
            {
                resizing++;
                setButton1Icon();
                button2.BackgroundImage = stopRecording;
                tableLayoutPanel2.Controls.Remove(button3);
                tableLayoutPanel2.Controls.Remove(button4);
                tableLayoutPanel2.Controls.Remove(button5);
                tableLayoutPanel2.Controls.Remove(button6);
                SetTimer();
            }
            else if (resizing == 2)
            {
                resizing++;
                aTimer.Enabled = false;
                setButton1Icon();
            }
            else if (resizing == 3)
            {
                resizing--;
                aTimer.Enabled = true;
                setButton1Icon();
            }
        }
        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);
        public void WNDtoPNG(string fName)
        {
            Image myImage = new Bitmap(Size.Width, Size.Height, PixelFormat.Format24bppRgb);
            Graphics g1 = Graphics.FromHdc(GetDC(GetDesktopWindow()));
            Graphics g2 = Graphics.FromImage(myImage);
            IntPtr dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();
            BitBlt(dc2, 0,0,Size.Width, Size.Height, dc1, Left, Top, 13369376);
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);
            myImage.Save(fName, ImageFormat.Png);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (resizing == 0)
            {
                Left += 1;
            }
            else if (resizing == 1)
            {
                Width += 1;
                displayWindowSize();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (resizing == 0)
            {
                Left -= 1;
            }
            else if (resizing == 1)
            {
                Width -= 1;
                displayWindowSize();
            }
            else if (resizing == 2 || resizing == 3)
            {
                aTimer.Enabled = false;
                String sound = "";
                if (File.Exists("recorded.mp3")) sound = " -i recorded.mp3";
                Process makeMovie = System.Diagnostics.Process.Start("ffmpeg.exe", "-r "+(int)fps+" -i recorded%09d.png" + sound + " -vcodec libx264 -pix_fmt yuv420p recorded.mp4");
                while (!makeMovie.HasExited) ;
                while (!File.Exists(Directory.GetCurrentDirectory() + "\\recorded.mp4")) ;
                System.Diagnostics.Process.Start("recorded.mp4", "");
                foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "recorded*.png"))
                {
                    System.IO.File.Delete(sFile);
                }
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (resizing == 0)
            {
                Top += 1;
            }
            else if (resizing == 1)
            {
                Height += 1;
                displayWindowSize();
            }
        }
        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (resizing < 2 && e.Button == MouseButtons.Left)
            {
                where = e.Location;
                whereB6.X = Width;
                whereB6.Y = Height;
            }
        }

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
        }
        Point where, whereB6;
        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (resizing == 0 && e.Button == MouseButtons.Left)
            {
                Left += e.Location.X - where.X;
                Top += e.Location.Y - where.Y;
            }
            else if (resizing == 1 && e.Button == MouseButtons.Left)
            {
                Width = whereB6.X - (where.X - e.Location.X);
                Height = whereB6.Y - (where.Y - e.Location.Y);
                displayWindowSize();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (resizing == 0)
            {
                Top -= 1;
            }
            else if (resizing == 1)
            {
                Height -= 1;
                displayWindowSize();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public bool isYoutube()
        {
            if ((Width == 854 && Height == 480) || (Width == 1280 && Height == 720) || (Width == 426 && Height == 240) || (Width == 640 && Height == 360)) return true;
            return false;
        }
        public bool isTelly()
        {
            if ((Width == 640 && Height == 480) || (Width == 720 && Height == 576) || (Width == 720 && Height == 480) || (Width == 480 && Height == 360)) return true;
            return false;
        }
        void setDisplayIcon()
        {
            if (isYoutube()) button6.BackgroundImage = youtubeImage;
            else if (isTelly()) button6.BackgroundImage = tellyImage;
            else button6.BackgroundImage = resizeImage;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (resizing == 0)
            {
                if (Width == 854 && Height == 480)
                {
                    Width = 1280; Height = 720;
                }
                else if (Width == 1280 && Height == 720)
                {
                    Width = 426; Height = 240;
                }
                else if (Width == 426 && Height == 240)
                {
                    Width = 480; Height = 360;
                }
                else if (Width == 480 && Height == 360)
                {
                    Width = 640; Height = 360;
                }
                else if (Width == 640 && Height == 360)
                {
                    Width = 640; Height = 480;
                }
                else if (Width == 640 && Height == 480)
                {
                    Width = 720; Height = 480;
                }
                else if (Width == 720 && Height == 480)
                {
                    Width = 720; Height = 576;
                }
                else if (Width == 720 && Height == 576)
                {
                    Width = 854; Height = 480;
                }
                else
                {
                    Width = 1280; Height = 720;
                }
                setDisplayIcon();
                displayWindowSize();
            }
            else if (resizing == 1)
            {
                resizing--;
                setButton1Icon();
                setDisplayIcon();
            }
        }
        bool waitingToFinish = false;
        private void button7_DragOver(object sender, DragEventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txtFPS(string fName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "ffmpeg.exe";
            process.StartInfo.Arguments = "-i " + fName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            Match matchFPS = Regex.Match(err, @"(?<fps>(\.|\d)+)\s+fps");
            File.WriteAllText("output.txt", matchFPS.Groups["fps"].Value.ToString());
            process.WaitForExit();
        }
        private void button7_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Button)))
            {
                foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "recorded*.png"))
                {
                    System.IO.File.Delete(sFile);
                }
                foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "output*.png"))
                {
                    System.IO.File.Delete(sFile);
                }
                foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.mp3"))
                {
                    if(sFile!= Directory.GetCurrentDirectory()+ "\\recorded.mp3") System.IO.File.Delete(sFile);
                }
                foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.mp4"))
                {
                    System.IO.File.Delete(sFile);
                }
                System.IO.File.Delete("output.txt");
                button7.BackgroundImage = crossImage;
                if (resizing == 0) setDisplayIcon();
                if (resizing == 1) button6.BackgroundImage = returnImage;
            }
            else if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string fName = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
                if (fName.EndsWith(".mp4"))
                {
                    waitingToFinish = true;
                    File.Delete("output.mp3");
                    foreach (string sFile in System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "output*.png"))
                    {
                        System.IO.File.Delete(sFile);
                    }
                    txtFPS(fName);
                    Process extractImage = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName + " output%09d.png");
                    Process extractVoice = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName + " -f mp3 -vn output.mp3");
                    Process.Start(Directory.GetCurrentDirectory(), "");
                    while (!extractImage.HasExited) ;
                    while (!extractVoice.HasExited) ;
                    waitingToFinish = false;
                    notReady = false;
                    button7.BackgroundImage = crossImage;
                }
                else if (fName.EndsWith(".png") || fName.EndsWith(".mp3") || fName.EndsWith(".txt"))
                {
                    waitingToFinish = true;
                    File.Delete("output.mp4");
                    String sound = "";
                    if (File.Exists("output.mp3")) sound = " -i output.mp3";
                    string fps = "1";
                    if (File.Exists("output.txt")) fps = File.ReadAllText("output.txt");
                    Process makeMovie = System.Diagnostics.Process.Start("ffmpeg.exe", "-r " + fps + " -i output%09d.png" + sound + " -vcodec libx264 -pix_fmt yuv420p output.mp4");
                    while (!makeMovie.HasExited) ;
                    waitingToFinish = false;
                    notReady = false;
                    button7.BackgroundImage = crossImage;
                }
            }

        }
        private bool isInOutput(string dragFileName) {
            if (dragFileName == Directory.GetCurrentDirectory() + "\\output.txt") return true;
            if (dragFileName == Directory.GetCurrentDirectory() + "\\output.mp3") return true;
            string[] allowedFileNames = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "output*.png");
            for (int i=0;i< allowedFileNames.Length;i++) {
                if (allowedFileNames[i]==dragFileName) return true;
            }
            return false;
        }
        bool notReady = false;
        private void button7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Button)))
            {
                button7.BackgroundImage = litter_bin;
                e.Effect = DragDropEffects.Move;
            }
            else if (!notReady && e.Data.GetDataPresent(DataFormats.FileDrop) && ((string[])e.Data.GetData(DataFormats.FileDrop, false)).Length == 1 && (isInOutput(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0])||((((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]).EndsWith(".mp4"))))
            {
                notReady = true;
                e.Effect = DragDropEffects.Copy;
                string fName = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
                if (fName.EndsWith(".mp4"))
                {
                    button7.BackgroundImage = openTapImage;
                }
                else if (fName.EndsWith(".png") || fName.EndsWith(".mp3") || fName.EndsWith(".txt"))
                {
                    button7.BackgroundImage = gearImage;
                }
            }
            else
                e.Effect = DragDropEffects.None;
        }
        Point MMb6;
        bool b6DragDrop = false;
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            MMb6 = e.Location;
            if (e.Button == MouseButtons.Right)
            {
                button6.BackgroundImage = banana_litter;
                button6.Refresh();
                Thread.Sleep(500);
                button6.BackgroundImage = carretImage;
                button6.Refresh();
                Thread.Sleep(500);
                setDisplayIcon();
                button6.Refresh();
            }
        }

        private void button6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (Math.Abs(MMb6.X - e.Location.X) > 20 || Math.Abs(MMb6.Y - e.Location.Y) > 20))
            {
                b6DragDrop = true;
                button6.BackgroundImage = banana_litter;
                button6.DoDragDrop(button6, DragDropEffects.Move);
            }
            else if(b6DragDrop)
            {
                b6DragDrop = false;
                if(resizing==0)setDisplayIcon();
                if (resizing == 1) button6.BackgroundImage = returnImage;
            }
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
        }

        private void button6_DragLeave(object sender, EventArgs e)
        {
            if (resizing == 0) setDisplayIcon();
            if (resizing == 1) button6.BackgroundImage = returnImage;
            acceptAnotherConversion = true;
        }
        bool acceptAnotherConversion=true;
        private void button6_DragEnter(object sender, DragEventArgs e)
        {
            if (!acceptAnotherConversion) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                bool goodFiles = true;
                for (int i = 0; goodFiles&&i < ((string[])e.Data.GetData(DataFormats.FileDrop, false)).Length;i++) {
                    goodFiles = false;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".mp3")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".wav")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".avi")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".mpg")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".mov")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".flv")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".wmv")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".ogg")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".aac")) goodFiles = true;
                    if ((((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]).EndsWith(".aiff")) goodFiles = true;
                }
                if (goodFiles) {
                    acceptAnotherConversion = false;
                    button6.BackgroundImage = carretImage;
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void button5_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void button6_DragDrop(object sender, DragEventArgs e)
        {
            for (int i = 0; i < ((string[])e.Data.GetData(DataFormats.FileDrop, false)).Length; i++)
            {
                string fName = (((string[])e.Data.GetData(DataFormats.FileDrop, false))[i]);
                if (fName.EndsWith(".mp3"))
                {
                    Process convertMP3toWAV = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName + " "+ fName.Substring(0, fName.Length - 4) + ".wav");
                }
                if (fName.EndsWith(".wav")|| fName.EndsWith(".ogg")||fName.EndsWith(".aac"))
                {
                    Process convertWAVtoMP3 = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName + " " + fName.Substring(0, fName.Length - 4) + ".mp3");
                }
                if (fName.EndsWith(".aiff"))
                {
                    Process convertAIFFtoMP3 = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName + " " + fName.Substring(0, fName.Length - 5) + ".mp3");
                }
                if (fName.EndsWith(".avi")|| fName.EndsWith(".mpg")|| fName.EndsWith(".mov")|| fName.EndsWith(".flv") || fName.EndsWith(".wmv"))
                {
                    Process convertAVItoMP4 = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName + " " + fName.Substring(0, fName.Length - 4) + ".mp4");
                }
            }
            if (resizing == 0) setDisplayIcon();
            if (resizing == 1) button6.BackgroundImage = returnImage;
            acceptAnotherConversion = true;
        }
        bool acceptAnotherResizing = true;
        private void button1_DragEnter(object sender, DragEventArgs e)
        {
            if (!acceptAnotherResizing|| !e.Data.GetDataPresent(DataFormats.FileDrop)||((string[])e.Data.GetData(DataFormats.FileDrop, false)).Length!=1) return;
            string fName = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
            if (!fName.EndsWith(".mp4")) return;
            acceptAnotherResizing = false;
            button1.BackgroundImage = wielkoscImage;
            e.Effect = DragDropEffects.Move;
        }

        private void button1_DragLeave(object sender, EventArgs e)
        {
            if (!acceptAnotherResizing)
            {
                setButton1Icon();
                acceptAnotherResizing = true;
            }
        }
        void setButton1Icon()
        {
            if (resizing == 0)
            {
                button1.BackgroundImage = okImage;

            }
            else if (resizing == 1)
            {
                if (File.Exists("recorded.mp3")) button1.BackgroundImage = startRecordingM; else button1.BackgroundImage = startRecording;
            }
            else if (resizing == 2)
            {
                button1.BackgroundImage = pauseRecording;
            }
            else if (resizing == 3)
            {
                if (File.Exists("recorded.mp3")) button1.BackgroundImage = startRecordingM; else button1.BackgroundImage = startRecording;
            }
        }
        private void button1_DragDrop(object sender, DragEventArgs e)
        {
            string fName = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
            Process convertResize = System.Diagnostics.Process.Start("ffmpeg.exe", "-i " + fName+ " -s "+Width+"x"+Height+" "+fName.Substring(0, fName.Length - 4) + "_.mp4");
            setButton1Icon();
            acceptAnotherResizing = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process getIT = System.Diagnostics.Process.Start("getIT.exe", "-f bestvideo[ext=mp4]+bestaudio[ext=mp3]/best[ext=mp4]/best " + textBox2.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process getIT = System.Diagnostics.Process.Start("getIT.exe", "-x --audio-format mp3 " + textBox2.Text);
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                button7.BackgroundImage = openTapImage;
                button7.Refresh();
                Thread.Sleep(500);
                button7.BackgroundImage = gearImage;
                button7.Refresh();
                Thread.Sleep(500);
                button7.BackgroundImage = litter_bin;
                button7.Refresh();
                Thread.Sleep(500);
                button7.BackgroundImage = crossImage;
                button7.Refresh();
            }
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                button1.BackgroundImage = startRecording;
                button1.Refresh();
                Thread.Sleep(500);
                button1.BackgroundImage = pauseRecording;
                button1.Refresh();
                Thread.Sleep(500);
                button1.BackgroundImage = startRecordingM;
                button1.Refresh();
                Thread.Sleep(500);
                button1.BackgroundImage = wielkoscImage;
                button1.Refresh();
                Thread.Sleep(500);
                setButton1Icon();
                button1.Refresh();
            }
        }

        private void button7_DragLeave(object sender, EventArgs e)
        {
            if (!waitingToFinish)
            {
                button7.BackgroundImage = crossImage;
                notReady = false;
            }
            if (b6DragDrop) {
                button7.BackgroundImage = crossImage;
            }
        }
    }
}

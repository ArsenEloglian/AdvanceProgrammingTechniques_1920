using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using gra.Properties;

namespace gra
{
    public partial class OknoGry : Form
    {
        Bitmap gameView = null;
        Bitmap sky;
        Bitmap frog;
        Bitmap leaf;
        Bitmap food;
        Bitmap bird;
        Point frogLocation;
        Point[] leafs;
        Point[] foods;
        Point[] birds;
        int frogJump = 0;
        int eaten = 0;
        bool gameOver = false;
        public void DrawBMP(Bitmap BMP, Point where)
        {
            Rectangle gameViewRectangle = new Rectangle(0,0,gameView.Width,gameView.Height);
            Rectangle drawingRectangle = new Rectangle(where.X,where.Y,BMP.Width,BMP.Height);
            Rectangle outcomeRectangle = Rectangle.Intersect(gameViewRectangle,drawingRectangle);
            if (outcomeRectangle.IsEmpty) return;
            Point BMPbegin = new Point(outcomeRectangle.X-where.X,outcomeRectangle.Y-where.Y);
            Point tmp = new Point(0, 0);
            while (tmp.Y < outcomeRectangle.Height)
            {
                while (tmp.X<outcomeRectangle.Width)
                {
                    Color pixel = BMP.GetPixel(BMPbegin.X+tmp.X, BMPbegin.Y+tmp.Y);
                    if (pixel.R > 240 && pixel.G > 240 && pixel.B > 240)
                    {
                    }
                    else
                    {
                        (widokGry.Image as Bitmap).SetPixel(outcomeRectangle.X + tmp.X, outcomeRectangle.Y + tmp.Y, pixel);
                    }
                    tmp.X++;
                }
                tmp.X = 0;
                tmp.Y++;
            }
        }
        public void drawGameOver()
        {
            Graphics g = Graphics.FromImage(gameView);
            Font drawFont = new Font("Segoe Script", 50);
            SolidBrush drawBrush = new SolidBrush(Color.DarkBlue);
            g.DrawString("Koniec", drawFont, drawBrush, new Point(100, 100));
        }
        public void DrawAllObjects() {
            DrawSky();
            foreach(Point p in leafs) DrawBMP(leaf,p);
            foreach(Point p in birds) DrawBMP(bird,p);
            foreach(Point p in foods) DrawBMP(food,p);
            DrawBMP(frog, new Point(frogLocation.X, frogLocation.Y));
            if (gameOver) drawGameOver();
            widokGry.Refresh();
        }
        public void DrawSky() {
            Point tmp = new Point(0,0);
            while (tmp.Y < widokGry.Image.Height)
            {
                while (tmp.X < widokGry.Image.Width) {
                    DrawBMP(sky, tmp);
                    tmp.X += sky.Width;
                }
                tmp.X = 0;
                tmp.Y += sky.Height;
            }
        }
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        bool IsCollision(Rectangle r)
        {
            Rectangle object1 = new Rectangle(frogLocation.X, frogLocation.Y,frog.Width,frog.Height);
            if (!Rectangle.Intersect(object1, r).IsEmpty) return true;
            foreach(Point p in leafs)
            {
                object1 = new Rectangle(p.X, p.Y, leaf.Width, leaf.Height);
                if (!Rectangle.Intersect(object1, r).IsEmpty) return true;
            }
            foreach (Point p in foods)
            {
                object1 = new Rectangle(p.X, p.Y, food.Width, food.Height);
                if (!Rectangle.Intersect(object1, r).IsEmpty) return true;
            }
            foreach (Point p in birds)
            {
                object1 = new Rectangle(p.X, p.Y, food.Width, food.Height);
                if (!Rectangle.Intersect(object1, r).IsEmpty) return true;
            }
            return false;
        }
        int ile = 0;
        void DoBlink(object sender, EventArgs e)
        {
            Text = ile.ToString();
            Refresh();
        }
        Random rdm = new Random();
        bool isDatabase;
        void checkWhetherDatabaseExist() {
            if (!File.Exists("Database1.mdf"))isDatabase = false;
            else isDatabase = true;
        }

        void InitializeComponentHere()
        {
            guzikZdjęcie.BackgroundImage = new Bitmap(Program.gamePath + "rysunki\\aparatFotograficzny.jpg");
            guzikZrzutEkranu.BackgroundImage = new Bitmap(Program.gamePath + "rysunki\\ekran.jpg");
            guzikZegar.BackgroundImage = new Bitmap(Program.gamePath + "rysunki\\zegar.jpg");
            guzikTłoGry.BackgroundImage = new Bitmap(Program.gamePath + "rysunki\\pędzle.jpg");
            Icon = Program.żabaIcon;
            Cursor = new Cursor(Resources.osoitin.Handle);
            toolStripSplitButton1.Image = Program.żabaIcon.ToBitmap();
        }
        public OknoGry()
        {
            File.Delete(Program.gamePath+ "desktopShot.png");
            File.Delete(Program.gamePath + "windowShot.png");
            InitializeComponent();//najpierw wspólne.gamePath aby znać ścieżkę
            InitializeComponentHere();//obrazki wgraj
            checkWhetherDatabaseExist();
            frog = new Bitmap(Program.gamePath + "rysunki\\żaba.jpg");
            leaf = new Bitmap(Program.gamePath + "rysunki\\liść.jpg");
            sky = new Bitmap(Program.gamePath + "rysunki\\staw.jpg");
            bird = new Bitmap(Program.gamePath + "rysunki\\ptak.jpg");
            food = new Bitmap(Program.gamePath + "rysunki\\owad.jpg");
            widokGry.Image = gameView = new Bitmap(widokGry.Width, widokGry.Height);
            toolStripProgressBar1.Maximum = gameView.Width * 10 + 1;
            frogLocation = new Point(widokGry.Width / 2 - frog.Width / 2, widokGry.Height / 2- frog.Height);
            leafs = new Point[rdm.Next(20,70)];
            foods = new Point[rdm.Next(10,100)];
            birds = new Point[rdm.Next(0,25)];
            leafs[0] = new Point((frogLocation.X + frog.Width / 2 - leaf.Width / 2), frogLocation.Y + frog.Height);
            leafs[1] = new Point(gameView.Width * 10, rdm.Next(0, gameView.Height));
            for (int i = 2; i < leafs.Length;)
            {
                Point p = new Point(rdm.Next(0,gameView.Width*10), rdm.Next(0,gameView.Height));
                if (!IsCollision(new Rectangle(p.X,p.Y,leaf.Width,leaf.Height)))
                {
                    leafs[i]=p;
                    i++;
                }
            }
            for (int i = 0; i < foods.Length;)
            {
                Point p = new Point(rdm.Next(0, gameView.Width * 10), rdm.Next(0, gameView.Height));
                if (!IsCollision(new Rectangle(p.X, p.Y, food.Width, food.Height)))
                {
                    foods[i]=p;
                    i++;
                }
            }
            for (int i = 0; i < birds.Length;)
            {
                Point p = new Point(rdm.Next(0, gameView.Width * 10), rdm.Next(0, gameView.Height));
                if (!IsCollision(new Rectangle(p.X, p.Y, bird.Width, bird.Height)))
                {
                    birds[i]=p;
                    i++;
                }
            }
            player.URL = Program.gamePath + "rysunki\\hudba.mp3";
            player.settings.setMode("loop",true);//będzie wtórnione
            player.controls.stop();//nie graj
            DrawAllObjects();//niech wygląda na początek
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rozpoczęcieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.oknoGry = null;
            Hide();
            Dispose();            
        }

        private void wstzrymajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tykacz.Enabled = false;
            player.controls.stop();
        }
        private void przemyśleniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Wyświetlono zapisy o grze...";
            GameNote gameNote = new GameNote();
            gameNote.Show();
        }

        private void guzikZdjęcie_Click(object sender, EventArgs e)
        {
            Program.WNDtoPNG(tableLayoutPanel1.Handle, Program.gamePath+"windowShot.png");
            toolStripStatusLabel1.Text = "Zapisano zdjęcie okna w bieżącej teczce w pliku windowShot.png...";
        }

        private void guzikZrzutEkranu_Click(object sender, EventArgs e)
        {
            Program.ScreenToPNG(Program.gamePath + "desktopShot.png");
            toolStripStatusLabel1.Text = "Zapisano zrzut ekranu w teczce gry w pliku desktopShot.png...";
        }

        private void guzikZegar_Click(object sender, EventArgs e)
        {
            string result = Program.getWebPage("https://www.timeanddate.com/worldclock/poland");
            Match matchTime = Regex.Match(result, @"\s+id=ct(\s+)*(\w|=)*>(?<time>[^<]+)");
            Match matchDate = Regex.Match(result, @"\s+id=ctdat(\s+)*(\w|=)*>(?<date>[^<]+)");
            if (matchTime.Success && matchDate.Success)toolStripStatusLabel1.Text = "Dzisiaj mamy godzinę "+ matchTime.Groups["time"].Value+" w dniu "+ matchDate.Groups["date"].Value;
        }
        public class MyFontDialog : FontDialog
        {
            [DllImport("user32.dll")]
            static extern bool SetWindowText(IntPtr hWnd, string lpString);
            private string title = "Czcionka paska powiadomień";
            private bool titleSet = false;
            protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
            {
                if (!titleSet)
                {
                    SetWindowText(hWnd, title);
                    titleSet = true;
                }

                return base.HookProc(hWnd, msg, wparam, lparam);
            }
        }
        private void inneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new MyFontDialog();
            fontDialog.ShowColor = true;
            fontDialog.Color= toolStripStatusLabel1.ForeColor;
            fontDialog.Font = toolStripStatusLabel1.Font;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Font= fontDialog.Font;
                toolStripStatusLabel1.ForeColor=fontDialog.Color;
                toolStripStatusLabel1.Text = "Zmiana czcionki tego paska powiadomień przebiegła pomyślnie...";
            }
        }
        public class MyColorDialog : ColorDialog
        {
            [DllImport("user32.dll")]
            static extern bool SetWindowText(IntPtr hWnd, string lpString);
            private string title = "Barwy paska wyboru";
            private bool titleSet = false;
            protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
            {
                if (!titleSet)
                {
                    SetWindowText(hWnd, title);
                    titleSet = true;
                }

                return base.HookProc(hWnd, msg, wparam, lparam);
            }
        }
        private void barwyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new MyColorDialog();
            colorDialog.Color = wybornikGłówny.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                wybornikGłówny.BackColor = colorDialog.Color;
            }
        }

        private void guzikTłoGry_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "wszystkie (*.*)|*.*|jpeg (*.gif)|*.gif|png (*.png)|*.png|bmp (*.bmp)|*.bmp|jpeg (*.jpeg)|*.jpeg|jpg (*.jpg)|*.jpg";
            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Program.gamePath;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sky = new Bitmap(openFileDialog.FileName);
                toolStripStatusLabel1.Text = "Udało ci się graczu zmienić tło gry...";
                DrawAllObjects();
                widokGry.Refresh();
            }
        }

        private void wtórnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            wstzrymajToolStripMenuItem_Click(null, null);
            Program.oknoGry = new OknoGry();
            Program.oknoGry.Show();
            Dispose();
        }

        private void guzikZdjęcie_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tym przyciskiem możesz wykonać zdjęcie widoku gry...";
            toolTip1.SetToolTip(guzikZdjęcie, File.Exists(Program.gamePath + "windowShot.png")?"Nadpiszesz plik "+ Program.gamePath+"windowShot.png": "Wciśnij aby wykonać nowy plik windowShot.png w "+ Program.gamePath.Remove(Program.gamePath.ToString().LastIndexOf("\\"), 1));
        }

        private void guzikZrzutEkranu_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tym przyciskiem możesz wykonać zdjęcie całego ekranu...";
            toolTip1.SetToolTip(guzikZrzutEkranu, File.Exists(Program.gamePath + "desktopShot.png") ? "Nadpiszesz plik " + Program.gamePath + "desktopShot.png" : "Wciśnij aby wykonać nowy plik desktopShot.png w " + Program.gamePath.Remove(Program.gamePath.ToString().LastIndexOf("\\"), 1));
        }

        private void guzikZegar_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tym przyciskiem pobierzesz obecny czas...";
            toolTip1.SetToolTip(guzikZegar, "Będzie pobranie czasu ze strony https://www.timeanddate.com/worldclock/poland");
        }

        private void guzikTłoGry_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tym przyciskiem zmienisz tło planszy...";
            toolTip1.SetToolTip(guzikTłoGry, "Zmienisz tło z obecnego");
        }
        private void glowne_Load(object sender, EventArgs e)
        {
            if (Program.loggedUser != "") changeToLogout();
        }

        private void bibliotekiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Wyświetlono załadowane biblioteki...";
            Biblioteki biblioteki = new Biblioteki();
            biblioteki.Show();
        }

        private void przemyśleniaToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu redagujesz swoje myśli...";
        }

        private void bibliotekiToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu wejrzysz w załadowane biblioteki...";
        }

        private void oGrzeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu dowiesz się więcej o grze...";
        }

        private void barwyToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zmienisz barwę menu...";
        }

        private void inneToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zmienisz tą czcionkę...";
        }

        private void wyświetlToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zajrzysz w pomoc...";
        }

        private void zakończToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zakończysz grę...";
        }
        private void innyGraczToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu się utożsamisz...";
        }
        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void wtórnaToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu rozpoczniesz wtórną rozgrywkę...";
        }

        private void zapiszToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zachowasz rozgrywkę...";
        }

        private void wczytajToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu wczytasz rozgrywkę...";
        }

        private void pomocToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Wciśnij by uzyskać pomoc...";
        }

        private void ustawieniaToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Wciśnij by nastroić swojstwa...";
        }

        private void graToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Wciśnij by przełączyć grę...";
        }

        private void toolStripSplitButton1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Czekaj lub puść dążenia...";
        }

        private void odtwarzajToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Chcesz dalej się zabawiać...";
        }

        private void wstzrymajToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Chcesz przerwać zabawę...";
        }

        private void zrzutyToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zrobisz inny zrzut wciskając dwa razy w wybrane potem okno...";
        }

        private void zrzutyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Proszę robić sobie inny zrzut wciskając podwójnie wybrane okno...";
            zrzuty zrzutyOkno = new zrzuty();
            zrzutyOkno.Show();
        }

        private void sterownikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Wyświetlono dostępne sterowniki...";
            sterowniki sterownikiOkno = new sterowniki();
            sterownikiOkno.Show();
        }

        private void sterownikiToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\drivers\\... HKLM\\System\\CurrentControlSet\\Services\\";
        }
        private void nieeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage()) { widokGry.Image = Clipboard.GetImage(); }
        }
        private void taakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(gameView);
        }

        private void minimalizujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void zarządcaZdjęćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zarzadcaZdjęć zarzadcaOkno = new zarzadcaZdjęć();
            zarzadcaOkno.Show();
        }

        private void minimalizujToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Zmniejsz okno na pasek zadań...";
        }

        private void toolStripProgressBar1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tak daleko zaszła gra...";
        }

        private void toolStripStatusLabel1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Odtwarzam..."+player.controls.currentPosition.ToString();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Życzymy udanej i przyjemnej zabawy...";
        }

        private void wodospadGuzików_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "W tej częsci są zgromadzone szybkie wywołania...";
        }

        private void zarządcaZdjęćToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Zachowaj prawym przyciskiem myszy zdjęcie z obszaru gry by tu je zachować...";
        }

        private void odtwarzajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                tykacz.Enabled = true;
                player.controls.play();
            }
        }

        private void inneToolStripMenuItem1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Rozmieszczono inne rzeczy...";
        }

        private void introToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Obejrzyj film Intro...";
        }

        private void introToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Intro intro = new Intro();
            intro.Show();
        }
        private void wyślijMailemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Program.sendMail==null) Program.sendMail = new SendMail();
            Program.sendMail.Show();
        }

        private void innyGraczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherPlayer otherPlayer = new OtherPlayer(this);
        }
        int justUnderLeaf;
        bool isFrogOnLeaf()
        {
            Rectangle frogRectangle = new Rectangle(frogLocation.X, frogLocation.Y, frog.Width, frog.Height);
            foreach (Point p in leafs)
            {
                Rectangle leafRectangle = new Rectangle(p.X, p.Y, leaf.Width, leaf.Height);
                if (!Rectangle.Intersect(frogRectangle, leafRectangle).IsEmpty)
                {
                    justUnderLeaf = leaf.Height + p.Y;
                    return true;
                }
            }
            return false;
        }
        bool isFrogEating()
        {
            Rectangle frogRectangle = new Rectangle(frogLocation.X, frogLocation.Y, frog.Width, frog.Height);
            foreach (Point p in foods)
            {
                Rectangle foodRectangle = new Rectangle(p.X, p.Y, leaf.Width, leaf.Height);
                if (!Rectangle.Intersect(frogRectangle, foodRectangle).IsEmpty) return true;
            }
            return false;
        }
        bool isBirdEatingFrog()
        {
            Rectangle frogRectangle = new Rectangle(frogLocation.X, frogLocation.Y, frog.Width, frog.Height);
            foreach (Point p in birds)
            {
                Rectangle birdRectangle = new Rectangle(p.X, p.Y, leaf.Width, leaf.Height);
                if (!Rectangle.Intersect(frogRectangle, birdRectangle).IsEmpty) return true;
            }
            return false;
        }
        private void glowne_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Hide();
            if (tykacz.Enabled && e.KeyCode == Keys.Left)
            {
                toolStripStatusLabel1.Text = "W trakcie: " + player.controls.currentPosition.ToString();
                wstzrymajToolStripMenuItem_Click(null, null);
            }
            else if (!tykacz.Enabled && e.KeyCode == Keys.Right)
            {
                odtwarzajToolStripMenuItem_Click(null, null);
            }
            else if (tykacz.Enabled && e.KeyCode == Keys.Up && isFrogOnLeaf())
            {
                e.Handled = true;
                frogJump = 100;
            }
            else if (tykacz.Enabled && e.KeyCode == Keys.Down && isFrogOnLeaf())
            {
                e.Handled = true;
                if (frogJump != 0)
                {
                    frogJump = 0;
                }
                else
                {
                    frogLocation.Y += 27;
                }
            }
        }
        private void guzikPożywienie_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu zlicza się zdobyte punkty...";
            toolTip1.SetToolTip(guzikPożywienie, "Po naciśnięciu ukażą się statystyki");
        }
        private void guzikPożywienie_Click(object sender, EventArgs e)
        {
            if (!isDatabase) MessageBox.Show("no database>16mb...no sql server for creating one");
            else {
                statystics StatysticsWindow = new statystics();
                StatysticsWindow.Show();
                toolStripStatusLabel1.Text = "Ukazano statystyki";
            }
        }
        public void changeToLogout()
        {
            utożsamienieToolStripMenuItem.Text = "Wyloguj";
            Text = Program.loggedUser + " - Gra zręcznościowa żabka";
        }
        public void changeToLogin()
        {
            utożsamienieToolStripMenuItem.Text = "Logowanie";
            Text = "Gra zręcznościowa żabka";
            Program.loggedUser = "";
        }
        private void tykacz_Tick_1(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = gameView.Width * 10 - leafs[1].X;
            if (isFrogEating())
            {
                eaten++;
                guzikPożywienie.Text = eaten.ToString();
            };
            if (frogJump > 0)
            {
                frogLocation.Y -= 9;
                frogJump -= 10;
            }
            else
            {
                if (!isFrogOnLeaf()) frogLocation.Y += 13;
            }
            for (int i = 0; i < leafs.Length; i++) leafs[i].X -= 14;
            for (int i = 0; i < foods.Length; i++) foods[i].X -= 14;
            for (int i = 0; i < birds.Length; i++) birds[i].X -= 14;
            if (isBirdEatingFrog() || frogLocation.Y > gameView.Height || leafs[1].X<0)
            {
                gameOver = true;
                wstzrymajToolStripMenuItem_Click(null, null);
                if (Program.loggedUser != ""&&isDatabase)
                {
                    gameResult gr = new gameResult() { name = Program.loggedUser, points = eaten };
                    Program.database.gameResult.InsertOnSubmit(gr);
                    Program.database.SubmitChanges();
                }
                else if (isDatabase)
                {
                    gameResult gr = new gameResult() { name = "niewiadomy", points = eaten };
                    Program.database.gameResult.InsertOnSubmit(gr);
                    Program.database.SubmitChanges();
                }
            }
            DrawAllObjects();
        }
        private void glowne_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
        private void wyślijMailemToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tu wyślesz nam maila lub komu kolwiek...";
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

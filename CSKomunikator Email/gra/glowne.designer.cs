using System;
using System.Drawing;
using System.Windows.Forms;

namespace gra
{
    partial class glowne
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.wybornikGłówny = new System.Windows.Forms.MenuStrip();
            this.graToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wtórnaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakonczToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utożsamienieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przemyśleniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barwyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zarządcaZdjęćToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.introToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wyślijMailemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bibliotekiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zrzutyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sterownikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widokGry = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.taakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wodospadGuzików = new System.Windows.Forms.FlowLayoutPanel();
            this.guzikZdjęcie = new System.Windows.Forms.Button();
            this.guzikZrzutEkranu = new System.Windows.Forms.Button();
            this.guzikZegar = new System.Windows.Forms.Button();
            this.guzikTłoGry = new System.Windows.Forms.Button();
            this.guzikPożywienie = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.odtwarzajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wstzrymajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimalizujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tykacz = new System.Windows.Forms.Timer(this.components);
            this.wybornikGłówny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widokGry)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.wodospadGuzików.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wybornikGłówny
            // 
            this.wybornikGłówny.BackColor = System.Drawing.SystemColors.ControlDark;
            this.helpProvider1.SetHelpString(this.wybornikGłówny, "");
            this.wybornikGłówny.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graToolStripMenuItem,
            this.ustawieniaToolStripMenuItem,
            this.inneToolStripMenuItem1,
            this.pomocToolStripMenuItem});
            this.wybornikGłówny.Location = new System.Drawing.Point(0, 0);
            this.wybornikGłówny.Name = "wybornikGłówny";
            this.helpProvider1.SetShowHelp(this.wybornikGłówny, false);
            this.wybornikGłówny.Size = new System.Drawing.Size(873, 24);
            this.wybornikGłówny.TabIndex = 0;
            this.wybornikGłówny.Text = "wybornikGłówny";
            // 
            // graToolStripMenuItem
            // 
            this.graToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wtórnaToolStripMenuItem,
            this.zakonczToolStripMenuItem,
            this.utożsamienieToolStripMenuItem});
            this.graToolStripMenuItem.Name = "graToolStripMenuItem";
            this.graToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.graToolStripMenuItem.Text = "Gra";
            this.graToolStripMenuItem.MouseHover += new System.EventHandler(this.graToolStripMenuItem_MouseHover);
            // 
            // wtórnaToolStripMenuItem
            // 
            this.wtórnaToolStripMenuItem.Name = "wtórnaToolStripMenuItem";
            this.wtórnaToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.wtórnaToolStripMenuItem.Text = "Nowa";
            this.wtórnaToolStripMenuItem.Click += new System.EventHandler(this.wtórnaToolStripMenuItem_Click);
            this.wtórnaToolStripMenuItem.MouseHover += new System.EventHandler(this.wtórnaToolStripMenuItem_MouseHover);
            // 
            // zakonczToolStripMenuItem
            // 
            this.zakonczToolStripMenuItem.Name = "zakonczToolStripMenuItem";
            this.zakonczToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.zakonczToolStripMenuItem.Text = "Zakończ";
            this.zakonczToolStripMenuItem.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            this.zakonczToolStripMenuItem.MouseEnter += new System.EventHandler(this.zakończToolStripMenuItem_MouseHover);
            // 
            // utożsamienieToolStripMenuItem
            // 
            this.utożsamienieToolStripMenuItem.Name = "utożsamienieToolStripMenuItem";
            this.utożsamienieToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.utożsamienieToolStripMenuItem.Text = "Logowanie";
            this.utożsamienieToolStripMenuItem.Click += new System.EventHandler(this.innyGraczToolStripMenuItem_Click);
            this.utożsamienieToolStripMenuItem.MouseHover += new System.EventHandler(this.innyGraczToolStripMenuItem_MouseHover);
            // 
            // ustawieniaToolStripMenuItem
            // 
            this.ustawieniaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.przemyśleniaToolStripMenuItem,
            this.barwyToolStripMenuItem,
            this.inneToolStripMenuItem,
            this.zarządcaZdjęćToolStripMenuItem});
            this.ustawieniaToolStripMenuItem.Name = "ustawieniaToolStripMenuItem";
            this.ustawieniaToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.ustawieniaToolStripMenuItem.Text = "Ustawienia";
            this.ustawieniaToolStripMenuItem.MouseHover += new System.EventHandler(this.ustawieniaToolStripMenuItem_MouseHover);
            // 
            // przemyśleniaToolStripMenuItem
            // 
            this.przemyśleniaToolStripMenuItem.Name = "przemyśleniaToolStripMenuItem";
            this.przemyśleniaToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.przemyśleniaToolStripMenuItem.Text = "Przemyślenia";
            this.przemyśleniaToolStripMenuItem.Click += new System.EventHandler(this.przemyśleniaToolStripMenuItem_Click);
            this.przemyśleniaToolStripMenuItem.MouseEnter += new System.EventHandler(this.przemyśleniaToolStripMenuItem_MouseEnter);
            // 
            // barwyToolStripMenuItem
            // 
            this.barwyToolStripMenuItem.Name = "barwyToolStripMenuItem";
            this.barwyToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.barwyToolStripMenuItem.Text = "Barwy paska wyboru";
            this.barwyToolStripMenuItem.Click += new System.EventHandler(this.barwyToolStripMenuItem_Click);
            this.barwyToolStripMenuItem.MouseEnter += new System.EventHandler(this.barwyToolStripMenuItem_MouseEnter);
            // 
            // inneToolStripMenuItem
            // 
            this.inneToolStripMenuItem.Name = "inneToolStripMenuItem";
            this.inneToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.inneToolStripMenuItem.Text = "Czcionka paska powiadomień";
            this.inneToolStripMenuItem.Click += new System.EventHandler(this.inneToolStripMenuItem_Click);
            this.inneToolStripMenuItem.MouseEnter += new System.EventHandler(this.inneToolStripMenuItem_MouseEnter);
            // 
            // zarządcaZdjęćToolStripMenuItem
            // 
            this.zarządcaZdjęćToolStripMenuItem.Name = "zarządcaZdjęćToolStripMenuItem";
            this.zarządcaZdjęćToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.zarządcaZdjęćToolStripMenuItem.Text = "Zarządca zdjęć";
            this.zarządcaZdjęćToolStripMenuItem.Click += new System.EventHandler(this.zarządcaZdjęćToolStripMenuItem_Click);
            this.zarządcaZdjęćToolStripMenuItem.MouseHover += new System.EventHandler(this.zarządcaZdjęćToolStripMenuItem_MouseHover);
            // 
            // inneToolStripMenuItem1
            // 
            this.inneToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.introToolStripMenuItem,
            this.wyślijMailemToolStripMenuItem});
            this.inneToolStripMenuItem1.Name = "inneToolStripMenuItem1";
            this.inneToolStripMenuItem1.Size = new System.Drawing.Size(42, 20);
            this.inneToolStripMenuItem1.Text = "Inne";
            this.inneToolStripMenuItem1.MouseHover += new System.EventHandler(this.inneToolStripMenuItem1_MouseHover);
            // 
            // introToolStripMenuItem
            // 
            this.introToolStripMenuItem.Name = "introToolStripMenuItem";
            this.introToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.introToolStripMenuItem.Text = "Intro";
            this.introToolStripMenuItem.Click += new System.EventHandler(this.introToolStripMenuItem_Click);
            this.introToolStripMenuItem.MouseHover += new System.EventHandler(this.introToolStripMenuItem_MouseHover);
            // 
            // wyślijMailemToolStripMenuItem
            // 
            this.wyślijMailemToolStripMenuItem.Name = "wyślijMailemToolStripMenuItem";
            this.wyślijMailemToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.wyślijMailemToolStripMenuItem.Text = "Wyślij mailem";
            this.wyślijMailemToolStripMenuItem.Click += new System.EventHandler(this.wyślijMailemToolStripMenuItem_Click);
            this.wyślijMailemToolStripMenuItem.MouseHover += new System.EventHandler(this.wyślijMailemToolStripMenuItem_MouseHover);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bibliotekiToolStripMenuItem,
            this.zrzutyToolStripMenuItem,
            this.sterownikiToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            this.pomocToolStripMenuItem.MouseHover += new System.EventHandler(this.pomocToolStripMenuItem_MouseHover);
            // 
            // bibliotekiToolStripMenuItem
            // 
            this.bibliotekiToolStripMenuItem.Name = "bibliotekiToolStripMenuItem";
            this.bibliotekiToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.bibliotekiToolStripMenuItem.Text = "Biblioteki";
            this.bibliotekiToolStripMenuItem.Click += new System.EventHandler(this.bibliotekiToolStripMenuItem_Click);
            this.bibliotekiToolStripMenuItem.MouseEnter += new System.EventHandler(this.bibliotekiToolStripMenuItem_MouseEnter);
            // 
            // zrzutyToolStripMenuItem
            // 
            this.zrzutyToolStripMenuItem.Name = "zrzutyToolStripMenuItem";
            this.zrzutyToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zrzutyToolStripMenuItem.Text = "Zrzuty";
            this.zrzutyToolStripMenuItem.Click += new System.EventHandler(this.zrzutyToolStripMenuItem_Click);
            this.zrzutyToolStripMenuItem.MouseHover += new System.EventHandler(this.zrzutyToolStripMenuItem_MouseHover);
            // 
            // sterownikiToolStripMenuItem
            // 
            this.sterownikiToolStripMenuItem.Name = "sterownikiToolStripMenuItem";
            this.sterownikiToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.sterownikiToolStripMenuItem.Text = "Sterowniki";
            this.sterownikiToolStripMenuItem.Click += new System.EventHandler(this.sterownikiToolStripMenuItem_Click);
            this.sterownikiToolStripMenuItem.MouseHover += new System.EventHandler(this.sterownikiToolStripMenuItem_MouseHover);
            // 
            // widokGry
            // 
            this.widokGry.ContextMenuStrip = this.contextMenuStrip1;
            this.widokGry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpProvider1.SetHelpString(this.widokGry, "Tutaj wykonuje się gra...");
            this.widokGry.Location = new System.Drawing.Point(3, 3);
            this.widokGry.Name = "widokGry";
            this.helpProvider1.SetShowHelp(this.widokGry, true);
            this.widokGry.Size = new System.Drawing.Size(555, 398);
            this.widokGry.TabIndex = 0;
            this.widokGry.TabStop = false;
            this.widokGry.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taakToolStripMenuItem,
            this.nieeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 48);
            // 
            // taakToolStripMenuItem
            // 
            this.taakToolStripMenuItem.Name = "taakToolStripMenuItem";
            this.taakToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.taakToolStripMenuItem.Text = "Zachowaj";
            this.taakToolStripMenuItem.Click += new System.EventHandler(this.taakToolStripMenuItem_Click);
            // 
            // nieeToolStripMenuItem
            // 
            this.nieeToolStripMenuItem.Name = "nieeToolStripMenuItem";
            this.nieeToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.nieeToolStripMenuItem.Text = "Odtwórz";
            this.nieeToolStripMenuItem.Click += new System.EventHandler(this.nieeToolStripMenuItem_Click);
            // 
            // wodospadGuzików
            // 
            this.wodospadGuzików.Controls.Add(this.guzikZdjęcie);
            this.wodospadGuzików.Controls.Add(this.guzikZrzutEkranu);
            this.wodospadGuzików.Controls.Add(this.guzikZegar);
            this.wodospadGuzików.Controls.Add(this.guzikTłoGry);
            this.wodospadGuzików.Controls.Add(this.guzikPożywienie);
            this.wodospadGuzików.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpProvider1.SetHelpString(this.wodospadGuzików, "");
            this.wodospadGuzików.Location = new System.Drawing.Point(564, 3);
            this.wodospadGuzików.Name = "wodospadGuzików";
            this.helpProvider1.SetShowHelp(this.wodospadGuzików, true);
            this.wodospadGuzików.Size = new System.Drawing.Size(306, 398);
            this.wodospadGuzików.TabIndex = 1;
            this.wodospadGuzików.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            this.wodospadGuzików.MouseHover += new System.EventHandler(this.wodospadGuzików_MouseHover);
            // 
            // guzikZdjęcie
            // 
            this.guzikZdjęcie.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guzikZdjęcie.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.helpProvider1.SetHelpString(this.guzikZdjęcie, "Zrób zrzut okna gry...");
            this.guzikZdjęcie.Location = new System.Drawing.Point(3, 3);
            this.guzikZdjęcie.Name = "guzikZdjęcie";
            this.helpProvider1.SetShowHelp(this.guzikZdjęcie, true);
            this.guzikZdjęcie.Size = new System.Drawing.Size(106, 98);
            this.guzikZdjęcie.TabIndex = 0;
            this.guzikZdjęcie.TabStop = false;
            this.guzikZdjęcie.UseVisualStyleBackColor = true;
            this.guzikZdjęcie.Click += new System.EventHandler(this.guzikZdjęcie_Click);
            this.guzikZdjęcie.MouseEnter += new System.EventHandler(this.guzikZdjęcie_MouseEnter);
            // 
            // guzikZrzutEkranu
            // 
            this.guzikZrzutEkranu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guzikZrzutEkranu.Cursor = System.Windows.Forms.Cursors.PanSE;
            this.helpProvider1.SetHelpString(this.guzikZrzutEkranu, "Zrób zrzut ekranu...");
            this.guzikZrzutEkranu.Location = new System.Drawing.Point(115, 3);
            this.guzikZrzutEkranu.Name = "guzikZrzutEkranu";
            this.helpProvider1.SetShowHelp(this.guzikZrzutEkranu, true);
            this.guzikZrzutEkranu.Size = new System.Drawing.Size(154, 126);
            this.guzikZrzutEkranu.TabIndex = 1;
            this.guzikZrzutEkranu.TabStop = false;
            this.guzikZrzutEkranu.UseVisualStyleBackColor = true;
            this.guzikZrzutEkranu.Click += new System.EventHandler(this.guzikZrzutEkranu_Click);
            this.guzikZrzutEkranu.MouseEnter += new System.EventHandler(this.guzikZrzutEkranu_MouseEnter);
            // 
            // guzikZegar
            // 
            this.guzikZegar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guzikZegar.Cursor = System.Windows.Forms.Cursors.Cross;
            this.helpProvider1.SetHelpString(this.guzikZegar, "Pobierz czas z serwera...");
            this.guzikZegar.Location = new System.Drawing.Point(3, 135);
            this.guzikZegar.Name = "guzikZegar";
            this.helpProvider1.SetShowHelp(this.guzikZegar, true);
            this.guzikZegar.Size = new System.Drawing.Size(106, 117);
            this.guzikZegar.TabIndex = 2;
            this.guzikZegar.TabStop = false;
            this.guzikZegar.UseVisualStyleBackColor = true;
            this.guzikZegar.Click += new System.EventHandler(this.guzikZegar_Click);
            this.guzikZegar.MouseEnter += new System.EventHandler(this.guzikZegar_MouseEnter);
            // 
            // guzikTłoGry
            // 
            this.guzikTłoGry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guzikTłoGry.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.helpProvider1.SetHelpString(this.guzikTłoGry, "Zmień tło gry...");
            this.guzikTłoGry.Location = new System.Drawing.Point(115, 135);
            this.guzikTłoGry.Name = "guzikTłoGry";
            this.helpProvider1.SetShowHelp(this.guzikTłoGry, true);
            this.guzikTłoGry.Size = new System.Drawing.Size(108, 93);
            this.guzikTłoGry.TabIndex = 3;
            this.guzikTłoGry.TabStop = false;
            this.guzikTłoGry.UseVisualStyleBackColor = true;
            this.guzikTłoGry.Click += new System.EventHandler(this.guzikTłoGry_Click);
            this.guzikTłoGry.MouseEnter += new System.EventHandler(this.guzikTłoGry_MouseEnter);
            // 
            // guzikPożywienie
            // 
            this.guzikPożywienie.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.guzikPożywienie.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.helpProvider1.SetHelpString(this.guzikPożywienie, "Zobacz osiągnięcia...");
            this.guzikPożywienie.Location = new System.Drawing.Point(3, 258);
            this.guzikPożywienie.Name = "guzikPożywienie";
            this.helpProvider1.SetShowHelp(this.guzikPożywienie, true);
            this.guzikPożywienie.Size = new System.Drawing.Size(159, 82);
            this.guzikPożywienie.TabIndex = 4;
            this.guzikPożywienie.TabStop = false;
            this.guzikPożywienie.Text = "00000";
            this.guzikPożywienie.UseVisualStyleBackColor = true;
            this.guzikPożywienie.Click += new System.EventHandler(this.guzikPożywienie_Click);
            this.guzikPożywienie.MouseHover += new System.EventHandler(this.guzikPożywienie_MouseHover);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(873, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1555";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.odtwarzajToolStripMenuItem,
            this.wstzrymajToolStripMenuItem,
            this.minimalizujToolStripMenuItem});
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(16, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.MouseHover += new System.EventHandler(this.toolStripSplitButton1_MouseHover);
            // 
            // odtwarzajToolStripMenuItem
            // 
            this.odtwarzajToolStripMenuItem.Name = "odtwarzajToolStripMenuItem";
            this.odtwarzajToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.odtwarzajToolStripMenuItem.Text = "Odtwarzaj";
            this.odtwarzajToolStripMenuItem.Click += new System.EventHandler(this.odtwarzajToolStripMenuItem_Click);
            this.odtwarzajToolStripMenuItem.MouseHover += new System.EventHandler(this.odtwarzajToolStripMenuItem_MouseHover);
            // 
            // wstzrymajToolStripMenuItem
            // 
            this.wstzrymajToolStripMenuItem.Name = "wstzrymajToolStripMenuItem";
            this.wstzrymajToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.wstzrymajToolStripMenuItem.Text = "Wstrzymaj";
            this.wstzrymajToolStripMenuItem.Click += new System.EventHandler(this.wstzrymajToolStripMenuItem_Click);
            this.wstzrymajToolStripMenuItem.MouseHover += new System.EventHandler(this.wstzrymajToolStripMenuItem_MouseHover);
            // 
            // minimalizujToolStripMenuItem
            // 
            this.minimalizujToolStripMenuItem.Name = "minimalizujToolStripMenuItem";
            this.minimalizujToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.minimalizujToolStripMenuItem.Text = "Minimalizuj";
            this.minimalizujToolStripMenuItem.Click += new System.EventHandler(this.minimalizujToolStripMenuItem_Click);
            this.minimalizujToolStripMenuItem.MouseHover += new System.EventHandler(this.minimalizujToolStripMenuItem_MouseHover);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Maximum = 1000;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.RightToLeftLayout = true;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.toolStripProgressBar1.MouseHover += new System.EventHandler(this.toolStripProgressBar1_MouseHover);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "Zaczynamy zabawę...";
            this.toolStripStatusLabel1.MouseHover += new System.EventHandler(this.toolStripStatusLabel1_MouseHover);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.375F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.625F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.widokGry, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.wodospadGuzików, 1, 0);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.No;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(873, 404);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 50;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 10;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // tykacz
            // 
            this.tykacz.Interval = 10;
            this.tykacz.Tick += new System.EventHandler(this.tykacz_Tick_1);
            // 
            // glowne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.wybornikGłówny);
            this.HelpButton = true;
            this.helpProvider1.SetHelpKeyword(this, "");
            this.helpProvider1.SetHelpString(this, "");
            this.KeyPreview = true;
            this.MainMenuStrip = this.wybornikGłówny;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "glowne";
            this.helpProvider1.SetShowHelp(this, false);
            this.Text = "Gra zręcznościowa żabka";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.glowne_FormClosing);
            this.Load += new System.EventHandler(this.glowne_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glowne_KeyUp);
            this.wybornikGłówny.ResumeLayout(false);
            this.wybornikGłówny.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widokGry)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.wodospadGuzików.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip wybornikGłówny;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem graToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wtórnaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakonczToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ustawieniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przemyśleniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barwyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wstzrymajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odtwarzajToolStripMenuItem;
        private System.Windows.Forms.PictureBox widokGry;
        private System.Windows.Forms.FlowLayoutPanel wodospadGuzików;
        private System.Windows.Forms.Button guzikZdjęcie;
        private System.Windows.Forms.Button guzikZrzutEkranu;
        private System.Windows.Forms.Button guzikZegar;
        private System.Windows.Forms.Button guzikTłoGry;
        private System.Windows.Forms.ToolStripMenuItem bibliotekiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zrzutyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sterownikiToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem taakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nieeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimalizujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zarządcaZdjęćToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem inneToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem introToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wyślijMailemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utożsamienieToolStripMenuItem;
        private System.Windows.Forms.Button guzikPożywienie;
        private System.Windows.Forms.Timer tykacz;
        private HelpProvider helpProvider1;
    }
}


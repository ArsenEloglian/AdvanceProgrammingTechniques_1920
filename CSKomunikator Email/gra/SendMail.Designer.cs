namespace gra
{
    partial class SendMail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
 
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbPASSWORD = new System.Windows.Forms.TextBox();
            this.tbFROM = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tbSUBJECT = new System.Windows.Forms.TextBox();
            this.tbMESSAGE = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtServerSMTP = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.txtPortSMTP = new System.Windows.Forms.TextBox();
            this.fromEmailLogins = new System.Windows.Forms.ComboBox();
            this.txtPortIMAP = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.txtServerIMAP = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toEmailLogins = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUSUN = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSEND = new System.Windows.Forms.Button();
            this.btnGROMADŹ = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPASSWORD
            // 
            this.tbPASSWORD.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPASSWORD.HideSelection = false;
            this.tbPASSWORD.Location = new System.Drawing.Point(240, 39);
            this.tbPASSWORD.Margin = new System.Windows.Forms.Padding(4);
            this.tbPASSWORD.Name = "tbPASSWORD";
            this.tbPASSWORD.PasswordChar = '~';
            this.tbPASSWORD.Size = new System.Drawing.Size(432, 25);
            this.tbPASSWORD.TabIndex = 6;
            this.tbPASSWORD.Click += new System.EventHandler(this.tbTO_Click);
            // 
            // tbFROM
            // 
            this.tbFROM.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbFROM.HideSelection = false;
            this.tbFROM.Location = new System.Drawing.Point(240, 5);
            this.tbFROM.Margin = new System.Windows.Forms.Padding(4);
            this.tbFROM.Name = "tbFROM";
            this.tbFROM.Size = new System.Drawing.Size(432, 25);
            this.tbFROM.TabIndex = 1;
            this.tbFROM.Text = "piotr.rybak1@unilodz.eu";
            this.tbFROM.Click += new System.EventHandler(this.tbTO_Click);
            // 
            // textBox3
            // 
            this.textBox3.AllowDrop = true;
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox3.Location = new System.Drawing.Point(4, 39);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(227, 25);
            this.textBox3.TabIndex = 102;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "AL CONTRACENA";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(4, 4);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(330, 25);
            this.textBox1.TabIndex = 105;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "WYBIERZ TECZKI LUB PLIKI";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.Click += new System.EventHandler(this.dołączZałączńkZip_Click);
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(342, 4);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(331, 25);
            this.textBox2.TabIndex = 106;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "WYŁÓŻ ZIP";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.Click += new System.EventHandler(this.wypakujZip_Click);
            // 
            // tbSUBJECT
            // 
            this.tbSUBJECT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSUBJECT.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbSUBJECT.Location = new System.Drawing.Point(4, 297);
            this.tbSUBJECT.Margin = new System.Windows.Forms.Padding(4);
            this.tbSUBJECT.Name = "tbSUBJECT";
            this.tbSUBJECT.Size = new System.Drawing.Size(677, 25);
            this.tbSUBJECT.TabIndex = 107;
            this.tbSUBJECT.Text = "subject";
            this.tbSUBJECT.Click += new System.EventHandler(this.tbTO_Click);
            // 
            // tbMESSAGE
            // 
            this.tbMESSAGE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMESSAGE.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbMESSAGE.Location = new System.Drawing.Point(4, 332);
            this.tbMESSAGE.Margin = new System.Windows.Forms.Padding(4);
            this.tbMESSAGE.Multiline = true;
            this.tbMESSAGE.Name = "tbMESSAGE";
            this.tbMESSAGE.Size = new System.Drawing.Size(677, 177);
            this.tbMESSAGE.TabIndex = 108;
            this.tbMESSAGE.Text = "message";
            this.tbMESSAGE.Click += new System.EventHandler(this.tbTO_Click);
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox4.Location = new System.Drawing.Point(4, 107);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(227, 25);
            this.textBox4.TabIndex = 110;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "SERVER SMTP";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtServerSMTP
            // 
            this.txtServerSMTP.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtServerSMTP.HideSelection = false;
            this.txtServerSMTP.Location = new System.Drawing.Point(240, 107);
            this.txtServerSMTP.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerSMTP.Name = "txtServerSMTP";
            this.txtServerSMTP.Size = new System.Drawing.Size(432, 25);
            this.txtServerSMTP.TabIndex = 111;
            this.txtServerSMTP.Text = "smtp.office365.com";
            this.txtServerSMTP.Click += new System.EventHandler(this.tbTO_Click);
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox7.Location = new System.Drawing.Point(4, 73);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(227, 25);
            this.textBox7.TabIndex = 112;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PORT SMTP";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPortSMTP
            // 
            this.txtPortSMTP.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPortSMTP.HideSelection = false;
            this.txtPortSMTP.Location = new System.Drawing.Point(240, 73);
            this.txtPortSMTP.Margin = new System.Windows.Forms.Padding(4);
            this.txtPortSMTP.Name = "txtPortSMTP";
            this.txtPortSMTP.Size = new System.Drawing.Size(432, 25);
            this.txtPortSMTP.TabIndex = 113;
            this.txtPortSMTP.Text = "587";
            this.txtPortSMTP.Click += new System.EventHandler(this.tbTO_Click);
            // 
            // fromEmailLogins
            // 
            this.fromEmailLogins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromEmailLogins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromEmailLogins.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fromEmailLogins.FormattingEnabled = true;
            this.fromEmailLogins.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.fromEmailLogins.Location = new System.Drawing.Point(4, 4);
            this.fromEmailLogins.Margin = new System.Windows.Forms.Padding(4);
            this.fromEmailLogins.MaxDropDownItems = 100;
            this.fromEmailLogins.Name = "fromEmailLogins";
            this.fromEmailLogins.Size = new System.Drawing.Size(677, 25);
            this.fromEmailLogins.Sorted = true;
            this.fromEmailLogins.TabIndex = 114;
            this.fromEmailLogins.SelectedIndexChanged += new System.EventHandler(this.emailLogins_SelectedIndexChanged);
            this.fromEmailLogins.Click += new System.EventHandler(this.emailLogins_Click);
            this.fromEmailLogins.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fromEmailLogins_KeyDown);
            // 
            // txtPortIMAP
            // 
            this.txtPortIMAP.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPortIMAP.HideSelection = false;
            this.txtPortIMAP.Location = new System.Drawing.Point(240, 141);
            this.txtPortIMAP.Margin = new System.Windows.Forms.Padding(4);
            this.txtPortIMAP.Name = "txtPortIMAP";
            this.txtPortIMAP.Size = new System.Drawing.Size(432, 25);
            this.txtPortIMAP.TabIndex = 118;
            this.txtPortIMAP.Text = "993";
            this.txtPortIMAP.Click += new System.EventHandler(this.txtPortIMAP_Click);
            // 
            // textBox8
            // 
            this.textBox8.Enabled = false;
            this.textBox8.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox8.Location = new System.Drawing.Point(4, 141);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(227, 25);
            this.textBox8.TabIndex = 117;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "PORT IMAP";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtServerIMAP
            // 
            this.txtServerIMAP.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtServerIMAP.HideSelection = false;
            this.txtServerIMAP.Location = new System.Drawing.Point(240, 175);
            this.txtServerIMAP.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerIMAP.Name = "txtServerIMAP";
            this.txtServerIMAP.Size = new System.Drawing.Size(432, 25);
            this.txtServerIMAP.TabIndex = 116;
            this.txtServerIMAP.Text = "outlook.office365.com";
            this.txtServerIMAP.Click += new System.EventHandler(this.txtServerIMAP_Click);
            // 
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox10.Location = new System.Drawing.Point(4, 175);
            this.textBox10.Margin = new System.Windows.Forms.Padding(4);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(227, 25);
            this.textBox10.TabIndex = 115;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "SERVER IMAP";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toEmailLogins, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.fromEmailLogins, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSUBJECT, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tbMESSAGE, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.692165F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.56119F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.072334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.072334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.67672F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.011655F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.35514F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.01869F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 700);
            this.tableLayoutPanel1.TabIndex = 119;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // toEmailLogins
            // 
            this.toEmailLogins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toEmailLogins.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toEmailLogins.FormattingEnabled = true;
            this.toEmailLogins.ImeMode = System.Windows.Forms.ImeMode.HangulFull;
            this.toEmailLogins.Location = new System.Drawing.Point(4, 262);
            this.toEmailLogins.Margin = new System.Windows.Forms.Padding(4);
            this.toEmailLogins.MaxDropDownItems = 100;
            this.toEmailLogins.Name = "toEmailLogins";
            this.toEmailLogins.Size = new System.Drawing.Size(677, 25);
            this.toEmailLogins.Sorted = true;
            this.toEmailLogins.TabIndex = 116;
            this.toEmailLogins.Text = "piotr.rybak1@unilodz.eu";
            this.toEmailLogins.SelectedIndexChanged += new System.EventHandler(this.toEmailLogins_SelectedIndexChanged);
            this.toEmailLogins.TextChanged += new System.EventHandler(this.toEmailLogins_TextChanged);
            this.toEmailLogins.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toEmailLogins_KeyPress);
            this.toEmailLogins.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toEmailLogins_KeyUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbPASSWORD);
            this.panel1.Controls.Add(this.txtPortIMAP);
            this.panel1.Controls.Add(this.txtServerSMTP);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.tbFROM);
            this.panel1.Controls.Add(this.txtServerIMAP);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.txtPortSMTP);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 43);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 211);
            this.panel1.TabIndex = 115;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox5.Location = new System.Drawing.Point(4, 5);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(227, 25);
            this.textBox5.TabIndex = 5;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "AL CORREO";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 517);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(677, 33);
            this.tableLayoutPanel2.TabIndex = 117;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(4, 602);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(677, 94);
            this.listBox1.TabIndex = 118;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnUSUN, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 558);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(677, 36);
            this.tableLayoutPanel3.TabIndex = 119;
            // 
            // btnUSUN
            // 
            this.btnUSUN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUSUN.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUSUN.Location = new System.Drawing.Point(342, 4);
            this.btnUSUN.Margin = new System.Windows.Forms.Padding(4);
            this.btnUSUN.Name = "btnUSUN";
            this.btnUSUN.Size = new System.Drawing.Size(331, 28);
            this.btnUSUN.TabIndex = 1;
            this.btnUSUN.Text = "USUŃ ZAŁĄCZŃK";
            this.btnUSUN.UseVisualStyleBackColor = true;
            this.btnUSUN.Click += new System.EventHandler(this.btnUSUN_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel4.Controls.Add(this.btnSEND, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnGROMADŹ, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(332, 30);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnSEND
            // 
            this.btnSEND.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSEND.Location = new System.Drawing.Point(36, 3);
            this.btnSEND.Name = "btnSEND";
            this.btnSEND.Size = new System.Drawing.Size(293, 24);
            this.btnSEND.TabIndex = 0;
            this.btnSEND.Text = "SFOCIARE";
            this.btnSEND.UseVisualStyleBackColor = true;
            this.btnSEND.Click += new System.EventHandler(this.btnSENDwtórny_Click);
            // 
            // btnGROMADŹ
            // 
            this.btnGROMADŹ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGROMADŹ.Location = new System.Drawing.Point(3, 3);
            this.btnGROMADŹ.Name = "btnGROMADŹ";
            this.btnGROMADŹ.Size = new System.Drawing.Size(27, 24);
            this.btnGROMADŹ.TabIndex = 1;
            this.btnGROMADŹ.UseVisualStyleBackColor = true;
            this.btnGROMADŹ.Click += new System.EventHandler(this.btnGROMADŹ_Click);
            // 
            // SendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SendMail";
            this.Text = "SendMail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendMail_FormClosing);
            this.Load += new System.EventHandler(this.SendMail_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPASSWORD;
        private System.Windows.Forms.TextBox tbFROM;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbSUBJECT;
        private System.Windows.Forms.TextBox tbMESSAGE;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtServerSMTP;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox txtPortSMTP;
        private System.Windows.Forms.ComboBox fromEmailLogins;
        private System.Windows.Forms.TextBox txtPortIMAP;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox txtServerIMAP;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox toEmailLogins;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnUSUN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnSEND;
        private System.Windows.Forms.Button btnGROMADŹ;
    }
}
namespace gra
{
    partial class GetMail
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cmbTO = new System.Windows.Forms.ComboBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtOd = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.cmbAttachments = new System.Windows.Forms.ComboBox();
            this.btnSaveAttachmentsToDesktop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReplyTo = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTrash = new System.Windows.Forms.Button();
            this.btnSent = new System.Windows.Forms.Button();
            this.cmbFolders = new System.Windows.Forms.ComboBox();
            this.btnSpam = new System.Windows.Forms.Button();
            this.btnInbox = new System.Windows.Forms.Button();
            this.btnResize = new System.Windows.Forms.Button();
            this.btnKosz = new System.Windows.Forms.Button();
            this.btnBulb = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnUnread = new System.Windows.Forms.Button();
            this.btnReplyFrom = new System.Windows.Forms.Button();
            this.btnShouldShowTagInfo = new System.Windows.Forms.Button();
            this.btnGromadzone = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Segoe Script", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(3, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(977, 51);
            this.listBox1.TabIndex = 6;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // cmbTO
            // 
            this.cmbTO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTO.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbTO.FormattingEnabled = true;
            this.cmbTO.Location = new System.Drawing.Point(3, 3);
            this.cmbTO.Name = "cmbTO";
            this.cmbTO.Size = new System.Drawing.Size(908, 25);
            this.cmbTO.TabIndex = 7;
            // 
            // txtSubject
            // 
            this.txtSubject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubject.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSubject.Location = new System.Drawing.Point(3, 3);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = true;
            this.txtSubject.Size = new System.Drawing.Size(963, 25);
            this.txtSubject.TabIndex = 9;
            // 
            // txtDateTime
            // 
            this.txtDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDateTime.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDateTime.Location = new System.Drawing.Point(614, 3);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.ReadOnly = true;
            this.txtDateTime.Size = new System.Drawing.Size(360, 25);
            this.txtDateTime.TabIndex = 11;
            this.txtDateTime.Text = "Żdeć";
            // 
            // txtFrom
            // 
            this.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrom.Enabled = false;
            this.txtFrom.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtFrom.Location = new System.Drawing.Point(3, 3);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(95, 25);
            this.txtFrom.TabIndex = 12;
            this.txtFrom.Text = "FROM";
            // 
            // txtOd
            // 
            this.txtOd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOd.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOd.Location = new System.Drawing.Point(104, 3);
            this.txtOd.Name = "txtOd";
            this.txtOd.ReadOnly = true;
            this.txtOd.Size = new System.Drawing.Size(504, 25);
            this.txtOd.TabIndex = 13;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 202);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(977, 318);
            this.webBrowser1.TabIndex = 14;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // cmbAttachments
            // 
            this.cmbAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAttachments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttachments.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbAttachments.FormattingEnabled = true;
            this.cmbAttachments.Location = new System.Drawing.Point(3, 3);
            this.cmbAttachments.Name = "cmbAttachments";
            this.cmbAttachments.Size = new System.Drawing.Size(908, 25);
            this.cmbAttachments.TabIndex = 15;
            this.cmbAttachments.SelectedIndexChanged += new System.EventHandler(this.cmbAttachments_SelectedIndexChanged);
            // 
            // btnSaveAttachmentsToDesktop
            // 
            this.btnSaveAttachmentsToDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveAttachmentsToDesktop.Location = new System.Drawing.Point(917, 3);
            this.btnSaveAttachmentsToDesktop.Name = "btnSaveAttachmentsToDesktop";
            this.btnSaveAttachmentsToDesktop.Size = new System.Drawing.Size(43, 21);
            this.btnSaveAttachmentsToDesktop.TabIndex = 16;
            this.btnSaveAttachmentsToDesktop.UseVisualStyleBackColor = true;
            this.btnSaveAttachmentsToDesktop.Click += new System.EventHandler(this.btnSaveAttachmentsToDesktop_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.webBrowser1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(983, 523);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.Location = new System.Drawing.Point(3, 133);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(977, 63);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSubject);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(969, 33);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SUBJECT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(969, 33);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ATTACHMENTS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94.91174F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.088266F));
            this.tableLayoutPanel2.Controls.Add(this.cmbAttachments, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveAttachmentsToDesktop, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(963, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(969, 33);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TO";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.Controls.Add(this.cmbTO, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReplyTo, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(963, 27);
            this.tableLayoutPanel5.TabIndex = 8;
            // 
            // btnReplyTo
            // 
            this.btnReplyTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReplyTo.Location = new System.Drawing.Point(917, 3);
            this.btnReplyTo.Name = "btnReplyTo";
            this.btnReplyTo.Size = new System.Drawing.Size(43, 21);
            this.btnReplyTo.TabIndex = 8;
            this.btnReplyTo.UseVisualStyleBackColor = true;
            this.btnReplyTo.Click += new System.EventHandler(this.btnReplyTo_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.42945F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.30061F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.42331F));
            this.tableLayoutPanel3.Controls.Add(this.txtFrom, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDateTime, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtOd, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 97);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(977, 30);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 17;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel4.Controls.Add(this.btnTrash, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSent, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbFolders, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSpam, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnInbox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnResize, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnKosz, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnBulb, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRead, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnUnread, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnReplyFrom, 10, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnShouldShowTagInfo, 11, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnGromadzone, 12, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(977, 31);
            this.tableLayoutPanel4.TabIndex = 17;
            // 
            // btnTrash
            // 
            this.btnTrash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTrash.Location = new System.Drawing.Point(315, 3);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Size = new System.Drawing.Size(33, 25);
            this.btnTrash.TabIndex = 3;
            this.btnTrash.UseVisualStyleBackColor = true;
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // btnSent
            // 
            this.btnSent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSent.Location = new System.Drawing.Point(276, 3);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(33, 25);
            this.btnSent.TabIndex = 7;
            this.btnSent.UseVisualStyleBackColor = true;
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // cmbFolders
            // 
            this.cmbFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFolders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFolders.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbFolders.FormattingEnabled = true;
            this.cmbFolders.Location = new System.Drawing.Point(3, 3);
            this.cmbFolders.Name = "cmbFolders";
            this.cmbFolders.Size = new System.Drawing.Size(189, 25);
            this.cmbFolders.TabIndex = 8;
            this.cmbFolders.SelectedIndexChanged += new System.EventHandler(this.cmbFolders_SelectedIndexChanged_1);
            // 
            // btnSpam
            // 
            this.btnSpam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSpam.Location = new System.Drawing.Point(237, 3);
            this.btnSpam.Name = "btnSpam";
            this.btnSpam.Size = new System.Drawing.Size(33, 25);
            this.btnSpam.TabIndex = 6;
            this.btnSpam.UseVisualStyleBackColor = true;
            this.btnSpam.Click += new System.EventHandler(this.btnSpam_Click);
            // 
            // btnInbox
            // 
            this.btnInbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInbox.Location = new System.Drawing.Point(198, 3);
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.Size = new System.Drawing.Size(33, 25);
            this.btnInbox.TabIndex = 4;
            this.btnInbox.UseVisualStyleBackColor = true;
            this.btnInbox.Click += new System.EventHandler(this.btnInbox_Click);
            // 
            // btnResize
            // 
            this.btnResize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResize.Location = new System.Drawing.Point(354, 3);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(33, 25);
            this.btnResize.TabIndex = 9;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnKosz
            // 
            this.btnKosz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKosz.Location = new System.Drawing.Point(393, 3);
            this.btnKosz.Name = "btnKosz";
            this.btnKosz.Size = new System.Drawing.Size(33, 25);
            this.btnKosz.TabIndex = 10;
            this.btnKosz.UseVisualStyleBackColor = true;
            this.btnKosz.Click += new System.EventHandler(this.btnKosz_Click);
            // 
            // btnBulb
            // 
            this.btnBulb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBulb.Location = new System.Drawing.Point(432, 3);
            this.btnBulb.Name = "btnBulb";
            this.btnBulb.Size = new System.Drawing.Size(33, 25);
            this.btnBulb.TabIndex = 11;
            this.btnBulb.UseVisualStyleBackColor = true;
            this.btnBulb.Click += new System.EventHandler(this.btnBulb_Click);
            // 
            // btnRead
            // 
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRead.Location = new System.Drawing.Point(471, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(33, 25);
            this.btnRead.TabIndex = 12;
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnUnread
            // 
            this.btnUnread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUnread.Location = new System.Drawing.Point(510, 3);
            this.btnUnread.Name = "btnUnread";
            this.btnUnread.Size = new System.Drawing.Size(33, 25);
            this.btnUnread.TabIndex = 13;
            this.btnUnread.UseVisualStyleBackColor = true;
            this.btnUnread.Click += new System.EventHandler(this.btnUnread_Click);
            // 
            // btnReplyFrom
            // 
            this.btnReplyFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReplyFrom.Location = new System.Drawing.Point(549, 3);
            this.btnReplyFrom.Name = "btnReplyFrom";
            this.btnReplyFrom.Size = new System.Drawing.Size(33, 25);
            this.btnReplyFrom.TabIndex = 14;
            this.btnReplyFrom.UseVisualStyleBackColor = true;
            this.btnReplyFrom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnReply_MouseClick);
            // 
            // btnShouldShowTagInfo
            // 
            this.btnShouldShowTagInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShouldShowTagInfo.Location = new System.Drawing.Point(588, 3);
            this.btnShouldShowTagInfo.Name = "btnShouldShowTagInfo";
            this.btnShouldShowTagInfo.Size = new System.Drawing.Size(33, 25);
            this.btnShouldShowTagInfo.TabIndex = 15;
            this.btnShouldShowTagInfo.UseVisualStyleBackColor = true;
            this.btnShouldShowTagInfo.Click += new System.EventHandler(this.btnShouldShowTagInfo_Click);
            // 
            // btnGromadzone
            // 
            this.btnGromadzone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGromadzone.Location = new System.Drawing.Point(627, 3);
            this.btnGromadzone.Name = "btnGromadzone";
            this.btnGromadzone.Size = new System.Drawing.Size(33, 25);
            this.btnGromadzone.TabIndex = 16;
            this.btnGromadzone.UseVisualStyleBackColor = true;
            this.btnGromadzone.Click += new System.EventHandler(this.btnGromadzone_Click);
            // 
            // GetMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 523);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GetMail";
            this.Text = "GetMail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetMail_FormClosing);
            this.Load += new System.EventHandler(this.GetMail_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox cmbTO;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtOd;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox cmbAttachments;
        private System.Windows.Forms.Button btnSaveAttachmentsToDesktop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.Button btnInbox;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.Button btnSpam;
        private System.Windows.Forms.ComboBox cmbFolders;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Button btnKosz;
        private System.Windows.Forms.Button btnBulb;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnUnread;
        private System.Windows.Forms.Button btnReplyFrom;
        private System.Windows.Forms.Button btnShouldShowTagInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnReplyTo;
        private System.Windows.Forms.Button btnGromadzone;
    }
}
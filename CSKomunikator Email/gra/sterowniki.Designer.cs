namespace gra
{
    partial class sterowniki
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewSterownikiWpamięci = new System.Windows.Forms.DataGridView();
            this.dataGridViewSpisSterowników = new System.Windows.Forms.DataGridView();
            this.txtZeSpisuSterowników = new System.Windows.Forms.TextBox();
            this.txtSterownikiWpamięci = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSterownikiWpamięci)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpisSterowników)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSterownikiWpamięci
            // 
            this.dataGridViewSterownikiWpamięci.AllowUserToAddRows = false;
            this.dataGridViewSterownikiWpamięci.AllowUserToDeleteRows = false;
            this.dataGridViewSterownikiWpamięci.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewSterownikiWpamięci.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSterownikiWpamięci.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewSterownikiWpamięci.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewSterownikiWpamięci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSterownikiWpamięci.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewSterownikiWpamięci.Location = new System.Drawing.Point(4, 36);
            this.dataGridViewSterownikiWpamięci.Name = "dataGridViewSterownikiWpamięci";
            this.dataGridViewSterownikiWpamięci.ReadOnly = true;
            this.dataGridViewSterownikiWpamięci.RowHeadersVisible = false;
            this.dataGridViewSterownikiWpamięci.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSterownikiWpamięci.Size = new System.Drawing.Size(1078, 201);
            this.dataGridViewSterownikiWpamięci.StandardTab = true;
            this.dataGridViewSterownikiWpamięci.TabIndex = 0;
            this.dataGridViewSterownikiWpamięci.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dataGridViewSpisSterowników
            // 
            this.dataGridViewSpisSterowników.AllowUserToAddRows = false;
            this.dataGridViewSpisSterowników.AllowUserToDeleteRows = false;
            this.dataGridViewSpisSterowników.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewSpisSterowników.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSpisSterowników.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewSpisSterowników.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewSpisSterowników.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridViewSpisSterowników.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSpisSterowników.Location = new System.Drawing.Point(4, 243);
            this.dataGridViewSpisSterowników.MultiSelect = false;
            this.dataGridViewSpisSterowników.Name = "dataGridViewSpisSterowników";
            this.dataGridViewSpisSterowników.ReadOnly = true;
            this.dataGridViewSpisSterowników.RowHeadersVisible = false;
            this.dataGridViewSpisSterowników.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSpisSterowników.Size = new System.Drawing.Size(1078, 195);
            this.dataGridViewSpisSterowników.TabIndex = 3;
            this.dataGridViewSpisSterowników.SelectionChanged += new System.EventHandler(this.dataGridViewZeSpisuSterowników_SelectionChanged);
            this.dataGridViewSpisSterowników.DoubleClick += new System.EventHandler(this.dataGridView2_DoubleClick);
            this.dataGridViewSpisSterowników.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewSpisSterowników_KeyDown);
            // 
            // txtZeSpisuSterowników
            // 
            this.txtZeSpisuSterowników.Location = new System.Drawing.Point(4, 444);
            this.txtZeSpisuSterowników.Name = "txtZeSpisuSterowników";
            this.txtZeSpisuSterowników.Size = new System.Drawing.Size(429, 20);
            this.txtZeSpisuSterowników.TabIndex = 4;
            this.txtZeSpisuSterowników.Click += new System.EventHandler(this.txtSpisSterowników_Click);
            this.txtZeSpisuSterowników.TextChanged += new System.EventHandler(this.txtZeSpisuSterowników_TextChanged);
            // 
            // txtSterownikiWpamięci
            // 
            this.txtSterownikiWpamięci.Location = new System.Drawing.Point(4, 10);
            this.txtSterownikiWpamięci.Name = "txtSterownikiWpamięci";
            this.txtSterownikiWpamięci.Size = new System.Drawing.Size(429, 20);
            this.txtSterownikiWpamięci.TabIndex = 5;
            this.txtSterownikiWpamięci.Click += new System.EventHandler(this.txtSterownikiWpamięci_Click);
            this.txtSterownikiWpamięci.TextChanged += new System.EventHandler(this.txtSterownikiWpamięci_TextChanged);
            // 
            // sterowniki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1094, 470);
            this.Controls.Add(this.txtSterownikiWpamięci);
            this.Controls.Add(this.txtZeSpisuSterowników);
            this.Controls.Add(this.dataGridViewSpisSterowników);
            this.Controls.Add(this.dataGridViewSterownikiWpamięci);
            this.Name = "sterowniki";
            this.Text = "sterowniki";
            this.Load += new System.EventHandler(this.sterowniki_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSterownikiWpamięci)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpisSterowników)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewSterownikiWpamięci;
        private System.Windows.Forms.DataGridView dataGridViewSpisSterowników;
        private System.Windows.Forms.TextBox txtZeSpisuSterowników;
        private System.Windows.Forms.TextBox txtSterownikiWpamięci;
    }
}
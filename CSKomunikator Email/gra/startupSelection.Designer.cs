namespace gra
{
    partial class startupSelection
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
            this.weekQuarter1 = new weekQuarters.weekQuarter();
            this.SuspendLayout();
            // 
            // weekQuarter1
            // 
            this.weekQuarter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weekQuarter1.Location = new System.Drawing.Point(0, 0);
            this.weekQuarter1.Name = "weekQuarter1";
            this.weekQuarter1.Size = new System.Drawing.Size(612, 344);
            this.weekQuarter1.TabIndex = 0;
            // 
            // startupSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 344);
            this.Controls.Add(this.weekQuarter1);
            this.Name = "startupSelection";
            this.Text = "startup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.pcStartup_FormClosing);
            this.Load += new System.EventHandler(this.startupSelection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private weekQuarters.weekQuarter weekQuarter1;
    }
}
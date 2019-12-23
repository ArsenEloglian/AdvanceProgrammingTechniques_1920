namespace weekQuarters
{
    partial class weekQuarter
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

        #region Kod wygenerowany przez Projektanta składników

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // weekQuarter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "weekQuarter";
            this.Size = new System.Drawing.Size(800, 450);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.weekQuarter_Paint_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.weekQuarter_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.weekQuarter_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.weekQuarter_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.weekQuarter_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.weekQuarter_MouseUp);
            this.Resize += new System.EventHandler(this.weekQuarter_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

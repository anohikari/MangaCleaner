namespace ImageHandler
{
    partial class Cleaner
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlMain = new System.Windows.Forms.Panel();
            this.ucImageHandler = new ImageHandler.UcImageHandler();
            //this.PnlMain.ClientArea.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            // 
            // PnlMain.ClientArea
            // 
            this.PnlMain.Controls.Add(this.ucImageHandler);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(1143, 961);
            this.PnlMain.TabIndex = 0;
            // 
            // ucImageHandler
            // 
            this.ucImageHandler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucImageHandler.Location = new System.Drawing.Point(0, 0);
            this.ucImageHandler.Name = "ucImageHandler";
            this.ucImageHandler.Size = new System.Drawing.Size(1143, 961);
            this.ucImageHandler.TabIndex = 0;
            // 
            // Cleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 961);
            this.Controls.Add(this.PnlMain);
            this.Name = "Cleaner";
            this.Text = "Cleaner";
            this.PnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlMain;
        private ImageHandler.UcImageHandler ucImageHandler;
    }
}


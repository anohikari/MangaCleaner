namespace ImageHandler
{
    partial class UcImageHandler
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFileListOutput = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PbDisplay = new System.Windows.Forms.PictureBox();
            this.Level = new System.Windows.Forms.CheckBox();
            this.CmdUndo = new System.Windows.Forms.Button();
            this.CmdReload = new System.Windows.Forms.Button();
            this.CmdNextImage = new System.Windows.Forms.Button();
            this.LblDarkest = new System.Windows.Forms.Label();
            this.LblBrightest = new System.Windows.Forms.Label();
            this.CmdStartLevel = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileListOutput
            // 
            this.lblFileListOutput.AutoSize = true;
            this.lblFileListOutput.Location = new System.Drawing.Point(63, 24);
            this.lblFileListOutput.Name = "lblFileListOutput";
            this.lblFileListOutput.Size = new System.Drawing.Size(31, 13);
            this.lblFileListOutput.TabIndex = 5;
            this.lblFileListOutput.Text = "Files:";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(379, 24);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(63, 13);
            this.lblOutput.TabIndex = 4;
            this.lblOutput.Text = "Found Data";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.loadDataToolStripMenuItem.Text = "Load Data";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // PbDisplay
            // 
            this.PbDisplay.Dock = System.Windows.Forms.DockStyle.Left;
            this.PbDisplay.Location = new System.Drawing.Point(0, 24);
            this.PbDisplay.Name = "PbDisplay";
            this.PbDisplay.Size = new System.Drawing.Size(653, 976);
            this.PbDisplay.TabIndex = 7;
            this.PbDisplay.TabStop = false;
            this.PbDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PbDisplay_MouseClick);
            // 
            // Level
            // 
            this.Level.AutoSize = true;
            this.Level.Location = new System.Drawing.Point(672, 72);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(52, 17);
            this.Level.TabIndex = 9;
            this.Level.Text = "Level";
            this.Level.UseVisualStyleBackColor = true;
            this.Level.CheckedChanged += new System.EventHandler(this.Level_CheckedChanged);
            // 
            // CmdUndo
            // 
            this.CmdUndo.Location = new System.Drawing.Point(672, 351);
            this.CmdUndo.Name = "CmdUndo";
            this.CmdUndo.Size = new System.Drawing.Size(123, 44);
            this.CmdUndo.TabIndex = 15;
            this.CmdUndo.Text = "Undo last change";
            this.CmdUndo.UseVisualStyleBackColor = true;
            this.CmdUndo.Click += new System.EventHandler(this.CmdUndo_Click);
            // 
            // CmdReload
            // 
            this.CmdReload.Location = new System.Drawing.Point(672, 422);
            this.CmdReload.Name = "CmdReload";
            this.CmdReload.Size = new System.Drawing.Size(123, 44);
            this.CmdReload.TabIndex = 16;
            this.CmdReload.Text = "Reload current Image";
            this.CmdReload.UseVisualStyleBackColor = true;
            this.CmdReload.Click += new System.EventHandler(this.CmdReload_Click);
            // 
            // CmdNextImage
            // 
            this.CmdNextImage.Location = new System.Drawing.Point(672, 498);
            this.CmdNextImage.Name = "CmdNextImage";
            this.CmdNextImage.Size = new System.Drawing.Size(123, 46);
            this.CmdNextImage.TabIndex = 17;
            this.CmdNextImage.Text = "Save + Next Image";
            this.CmdNextImage.UseVisualStyleBackColor = true;
            this.CmdNextImage.Click += new System.EventHandler(this.CmdNextImage_Click);
            // 
            // LblDarkest
            // 
            this.LblDarkest.AutoSize = true;
            this.LblDarkest.Location = new System.Drawing.Point(669, 127);
            this.LblDarkest.Name = "LblDarkest";
            this.LblDarkest.Size = new System.Drawing.Size(147, 13);
            this.LblDarkest.TabIndex = 18;
            this.LblDarkest.Text = "Lower Brightness Threshhold:";
            // 
            // LblBrightest
            // 
            this.LblBrightest.AutoSize = true;
            this.LblBrightest.Location = new System.Drawing.Point(669, 194);
            this.LblBrightest.Name = "LblBrightest";
            this.LblBrightest.Size = new System.Drawing.Size(144, 13);
            this.LblBrightest.TabIndex = 19;
            this.LblBrightest.Text = "Upper Brightness Threshhold";
            // 
            // CmdStartLevel
            // 
            this.CmdStartLevel.Location = new System.Drawing.Point(772, 68);
            this.CmdStartLevel.Name = "CmdStartLevel";
            this.CmdStartLevel.Size = new System.Drawing.Size(137, 23);
            this.CmdStartLevel.TabIndex = 20;
            this.CmdStartLevel.Text = "Confirm Level Thresholds";
            this.CmdStartLevel.UseVisualStyleBackColor = true;
            this.CmdStartLevel.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // UcImageHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CmdStartLevel);
            this.Controls.Add(this.LblBrightest);
            this.Controls.Add(this.LblDarkest);
            this.Controls.Add(this.CmdNextImage);
            this.Controls.Add(this.CmdReload);
            this.Controls.Add(this.CmdUndo);
            this.Controls.Add(this.Level);
            this.Controls.Add(this.PbDisplay);
            this.Controls.Add(this.lblFileListOutput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UcImageHandler";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileListOutput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox PbDisplay;
        private System.Windows.Forms.CheckBox Level;
        private System.Windows.Forms.Button CmdUndo;
        private System.Windows.Forms.Button CmdReload;
        private System.Windows.Forms.Button CmdNextImage;
        private System.Windows.Forms.Label LblDarkest;
        private System.Windows.Forms.Label LblBrightest;
        private System.Windows.Forms.Button CmdStartLevel;
    }
}

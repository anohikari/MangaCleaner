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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PbDisplay = new System.Windows.Forms.PictureBox();
            this.CmdUndo = new System.Windows.Forms.Button();
            this.CmdReload = new System.Windows.Forms.Button();
            this.CmdNextImage = new System.Windows.Forms.Button();
            this.LblDarkest = new System.Windows.Forms.Label();
            this.LblBrightest = new System.Windows.Forms.Label();
            this.CmdStartLevel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Level = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadDataToolStripMenuItem.Text = "Load Image";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // PbDisplay
            // 
            this.PbDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PbDisplay.Location = new System.Drawing.Point(0, 0);
            this.PbDisplay.Name = "PbDisplay";
            this.PbDisplay.Size = new System.Drawing.Size(736, 976);
            this.PbDisplay.TabIndex = 7;
            this.PbDisplay.TabStop = false;
            this.PbDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PbDisplay_MouseClick);
            // 
            // CmdUndo
            // 
            this.CmdUndo.Location = new System.Drawing.Point(33, 343);
            this.CmdUndo.Name = "CmdUndo";
            this.CmdUndo.Size = new System.Drawing.Size(123, 44);
            this.CmdUndo.TabIndex = 15;
            this.CmdUndo.Text = "Undo last change";
            this.CmdUndo.UseVisualStyleBackColor = true;
            this.CmdUndo.Click += new System.EventHandler(this.CmdUndo_Click);
            // 
            // CmdReload
            // 
            this.CmdReload.Location = new System.Drawing.Point(33, 414);
            this.CmdReload.Name = "CmdReload";
            this.CmdReload.Size = new System.Drawing.Size(123, 44);
            this.CmdReload.TabIndex = 16;
            this.CmdReload.Text = "Reload current Image";
            this.CmdReload.UseVisualStyleBackColor = true;
            this.CmdReload.Click += new System.EventHandler(this.CmdReload_Click);
            // 
            // CmdNextImage
            // 
            this.CmdNextImage.Location = new System.Drawing.Point(33, 490);
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
            this.LblDarkest.Location = new System.Drawing.Point(30, 119);
            this.LblDarkest.Name = "LblDarkest";
            this.LblDarkest.Size = new System.Drawing.Size(147, 13);
            this.LblDarkest.TabIndex = 18;
            this.LblDarkest.Text = "Lower Brightness Threshhold:";
            // 
            // LblBrightest
            // 
            this.LblBrightest.AutoSize = true;
            this.LblBrightest.Location = new System.Drawing.Point(30, 186);
            this.LblBrightest.Name = "LblBrightest";
            this.LblBrightest.Size = new System.Drawing.Size(144, 13);
            this.LblBrightest.TabIndex = 19;
            this.LblBrightest.Text = "Upper Brightness Threshhold";
            // 
            // CmdStartLevel
            // 
            this.CmdStartLevel.Location = new System.Drawing.Point(33, 245);
            this.CmdStartLevel.Name = "CmdStartLevel";
            this.CmdStartLevel.Size = new System.Drawing.Size(137, 23);
            this.CmdStartLevel.TabIndex = 20;
            this.CmdStartLevel.Text = "Confirm Level Thresholds";
            this.CmdStartLevel.UseVisualStyleBackColor = true;
            this.CmdStartLevel.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LblDarkest);
            this.panel1.Controls.Add(this.CmdStartLevel);
            this.panel1.Controls.Add(this.Level);
            this.panel1.Controls.Add(this.LblBrightest);
            this.panel1.Controls.Add(this.CmdUndo);
            this.panel1.Controls.Add(this.CmdReload);
            this.panel1.Controls.Add(this.CmdNextImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 976);
            this.panel1.TabIndex = 21;
            // 
            // Level
            // 
            this.Level.AutoSize = true;
            this.Level.Location = new System.Drawing.Point(33, 64);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(52, 17);
            this.Level.TabIndex = 9;
            this.Level.Text = "Level";
            this.Level.UseVisualStyleBackColor = true;
            this.Level.CheckedChanged += new System.EventHandler(this.Level_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.PbDisplay);
            this.splitContainer1.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragDrop);
            this.splitContainer1.Panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragEnter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 976);
            this.splitContainer1.SplitterDistance = 736;
            this.splitContainer1.TabIndex = 22;
            // 
            // UcImageHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UcImageHandler";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox PbDisplay;
        private System.Windows.Forms.Button CmdUndo;
        private System.Windows.Forms.Button CmdReload;
        private System.Windows.Forms.Button CmdNextImage;
        private System.Windows.Forms.Label LblDarkest;
        private System.Windows.Forms.Label LblBrightest;
        private System.Windows.Forms.Button CmdStartLevel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox Level;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

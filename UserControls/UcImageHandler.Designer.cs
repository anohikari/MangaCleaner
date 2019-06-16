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
            this.components = new System.ComponentModel.Container();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PbDisplay = new System.Windows.Forms.PictureBox();
            this.CmdUndo = new System.Windows.Forms.Button();
            this.CmdReload = new System.Windows.Forms.Button();
            this.CmdNextImage = new System.Windows.Forms.Button();
            this.LblDarkest = new System.Windows.Forms.Label();
            this.LblBrightest = new System.Windows.Forms.Label();
            this.CmdStartLevel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CheckDebug = new System.Windows.Forms.CheckBox();
            this.Level = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.NumThreshholdLower = new System.Windows.Forms.NumericUpDown();
            this.NumThreshholdUp = new System.Windows.Forms.NumericUpDown();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LblNoFile = new System.Windows.Forms.Label();
            this.LblControlLevel = new System.Windows.Forms.Label();
            this.LblBufferSize = new System.Windows.Forms.Label();
            this.CmdLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PbDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumThreshholdLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumThreshholdUp)).BeginInit();
            this.SuspendLayout();
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
            this.PbDisplay.Size = new System.Drawing.Size(736, 1000);
            this.PbDisplay.TabIndex = 7;
            this.PbDisplay.TabStop = false;
            this.PbDisplay.SizeChanged += new System.EventHandler(this.PbDisplay_SizeChanged);
            this.PbDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PbDisplay_MouseClick);
            // 
            // CmdUndo
            // 
            this.CmdUndo.Location = new System.Drawing.Point(18, 376);
            this.CmdUndo.Name = "CmdUndo";
            this.CmdUndo.Size = new System.Drawing.Size(123, 44);
            this.CmdUndo.TabIndex = 15;
            this.CmdUndo.Text = "Undo last change";
            this.CmdUndo.UseVisualStyleBackColor = true;
            this.CmdUndo.Click += new System.EventHandler(this.CmdUndo_Click);
            // 
            // CmdReload
            // 
            this.CmdReload.Location = new System.Drawing.Point(18, 426);
            this.CmdReload.Name = "CmdReload";
            this.CmdReload.Size = new System.Drawing.Size(123, 44);
            this.CmdReload.TabIndex = 16;
            this.CmdReload.Text = "Reload current image";
            this.CmdReload.UseVisualStyleBackColor = true;
            this.CmdReload.Click += new System.EventHandler(this.CmdReload_Click);
            // 
            // CmdNextImage
            // 
            this.CmdNextImage.Location = new System.Drawing.Point(18, 324);
            this.CmdNextImage.Name = "CmdNextImage";
            this.CmdNextImage.Size = new System.Drawing.Size(123, 46);
            this.CmdNextImage.TabIndex = 17;
            this.CmdNextImage.Text = "Save + Next image";
            this.ToolTip.SetToolTip(this.CmdNextImage, "Saves Image to Resultfolder within the opened directory and loads next Image from" +
        " Buffer");
            this.CmdNextImage.UseVisualStyleBackColor = true;
            this.CmdNextImage.Click += new System.EventHandler(this.CmdNextImage_Click);
            // 
            // LblDarkest
            // 
            this.LblDarkest.AutoSize = true;
            this.LblDarkest.Location = new System.Drawing.Point(30, 119);
            this.LblDarkest.Name = "LblDarkest";
            this.LblDarkest.Size = new System.Drawing.Size(142, 13);
            this.LblDarkest.TabIndex = 18;
            this.LblDarkest.Text = "Lower brightness threshhold:";
            this.LblDarkest.Visible = false;
            // 
            // LblBrightest
            // 
            this.LblBrightest.AutoSize = true;
            this.LblBrightest.Location = new System.Drawing.Point(30, 159);
            this.LblBrightest.Name = "LblBrightest";
            this.LblBrightest.Size = new System.Drawing.Size(139, 13);
            this.LblBrightest.TabIndex = 19;
            this.LblBrightest.Text = "Upper brightness threshhold";
            this.LblBrightest.Visible = false;
            // 
            // CmdStartLevel
            // 
            this.CmdStartLevel.Location = new System.Drawing.Point(33, 201);
            this.CmdStartLevel.Name = "CmdStartLevel";
            this.CmdStartLevel.Size = new System.Drawing.Size(127, 23);
            this.CmdStartLevel.TabIndex = 20;
            this.CmdStartLevel.Text = "Confirm and level";
            this.CmdStartLevel.UseVisualStyleBackColor = true;
            this.CmdStartLevel.Visible = false;
            this.CmdStartLevel.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CmdLoad);
            this.panel1.Controls.Add(this.LblBufferSize);
            this.panel1.Controls.Add(this.LblControlLevel);
            this.panel1.Controls.Add(this.NumThreshholdUp);
            this.panel1.Controls.Add(this.NumThreshholdLower);
            this.panel1.Controls.Add(this.CheckDebug);
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
            this.panel1.Size = new System.Drawing.Size(260, 1000);
            this.panel1.TabIndex = 21;
            // 
            // CheckDebug
            // 
            this.CheckDebug.AutoSize = true;
            this.CheckDebug.Location = new System.Drawing.Point(21, 31);
            this.CheckDebug.Name = "CheckDebug";
            this.CheckDebug.Size = new System.Drawing.Size(153, 17);
            this.CheckDebug.TabIndex = 21;
            this.CheckDebug.Text = "Show Preprocessed Image";
            this.ToolTip.SetToolTip(this.CheckDebug, "Show the flattened image and its marked regions used for internal regioning");
            this.CheckDebug.UseVisualStyleBackColor = true;
            this.CheckDebug.CheckedChanged += new System.EventHandler(this.CheckDebug_CheckedChanged);
            // 
            // Level
            // 
            this.Level.Appearance = System.Windows.Forms.Appearance.Button;
            this.Level.AutoSize = true;
            this.Level.Location = new System.Drawing.Point(21, 93);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(77, 23);
            this.Level.TabIndex = 9;
            this.Level.Text = "Level  image";
            this.ToolTip.SetToolTip(this.Level, "Stretch the Color spectrum to intensify colors");
            this.Level.UseVisualStyleBackColor = true;
            this.Level.CheckedChanged += new System.EventHandler(this.Level_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.LblNoFile);
            this.splitContainer1.Panel1.Controls.Add(this.PbDisplay);
            this.splitContainer1.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragDrop);
            this.splitContainer1.Panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragEnter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 1000);
            this.splitContainer1.SplitterDistance = 736;
            this.splitContainer1.TabIndex = 22;
            // 
            // NumThreshholdLower
            // 
            this.NumThreshholdLower.Location = new System.Drawing.Point(33, 136);
            this.NumThreshholdLower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumThreshholdLower.Name = "NumThreshholdLower";
            this.NumThreshholdLower.Size = new System.Drawing.Size(120, 20);
            this.NumThreshholdLower.TabIndex = 22;
            this.ToolTip.SetToolTip(this.NumThreshholdLower, "Selects the Lower leveling threshhold");
            this.NumThreshholdLower.Visible = false;
            this.NumThreshholdLower.ValueChanged += new System.EventHandler(this.NumThreshholdLower_ValueChanged);
            // 
            // NumThreshholdUp
            // 
            this.NumThreshholdUp.Location = new System.Drawing.Point(33, 175);
            this.NumThreshholdUp.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumThreshholdUp.Name = "NumThreshholdUp";
            this.NumThreshholdUp.Size = new System.Drawing.Size(120, 20);
            this.NumThreshholdUp.TabIndex = 23;
            this.ToolTip.SetToolTip(this.NumThreshholdUp, "Selects the Upper leveling threshhold");
            this.NumThreshholdUp.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumThreshholdUp.Visible = false;
            this.NumThreshholdUp.ValueChanged += new System.EventHandler(this.NumThreshholdUp_ValueChanged);
            // 
            // LblNoFile
            // 
            this.LblNoFile.AutoSize = true;
            this.LblNoFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblNoFile.Location = new System.Drawing.Point(342, 494);
            this.LblNoFile.Name = "LblNoFile";
            this.LblNoFile.Size = new System.Drawing.Size(52, 13);
            this.LblNoFile.TabIndex = 8;
            this.LblNoFile.Text = "Loading";
            // 
            // LblControlLevel
            // 
            this.LblControlLevel.AutoSize = true;
            this.LblControlLevel.Location = new System.Drawing.Point(21, 231);
            this.LblControlLevel.Name = "LblControlLevel";
            this.LblControlLevel.Size = new System.Drawing.Size(120, 13);
            this.LblControlLevel.TabIndex = 25;
            this.LblControlLevel.Text = "This shouldn´t be visible";
            this.LblControlLevel.Visible = false;
            // 
            // LblBufferSize
            // 
            this.LblBufferSize.AutoSize = true;
            this.LblBufferSize.Location = new System.Drawing.Point(30, 483);
            this.LblBufferSize.Name = "LblBufferSize";
            this.LblBufferSize.Size = new System.Drawing.Size(108, 13);
            this.LblBufferSize.TabIndex = 26;
            this.LblBufferSize.Text = "Current Buffersize = 0";
            // 
            // CmdLoad
            // 
            this.CmdLoad.Location = new System.Drawing.Point(18, 277);
            this.CmdLoad.Name = "CmdLoad";
            this.CmdLoad.Size = new System.Drawing.Size(120, 41);
            this.CmdLoad.TabIndex = 27;
            this.CmdLoad.Text = "Load image";
            this.CmdLoad.UseVisualStyleBackColor = true;
            this.CmdLoad.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // UcImageHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UcImageHandler";
            this.Size = new System.Drawing.Size(1000, 1000);
            ((System.ComponentModel.ISupportInitialize)(this.PbDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumThreshholdLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumThreshholdUp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.CheckBox CheckDebug;
        private System.Windows.Forms.NumericUpDown NumThreshholdUp;
        private System.Windows.Forms.NumericUpDown NumThreshholdLower;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Label LblNoFile;
        private System.Windows.Forms.Label LblControlLevel;
        private System.Windows.Forms.Label LblBufferSize;
        private System.Windows.Forms.Button CmdLoad;
    }
}

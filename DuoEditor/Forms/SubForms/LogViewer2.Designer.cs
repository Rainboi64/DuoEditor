namespace DuoEditor.Forms.SubForms
{
    partial class LogViewer2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer2));
            this.ruler2 = new FastColoredTextBoxNS.Ruler();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopLoggingInspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SyncTimer = new System.Windows.Forms.Timer(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ruler2
            // 
            this.ruler2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ruler2.Location = new System.Drawing.Point(0, 24);
            this.ruler2.MaximumSize = new System.Drawing.Size(1073741824, 24);
            this.ruler2.MinimumSize = new System.Drawing.Size(0, 24);
            this.ruler2.Name = "ruler2";
            this.ruler2.Size = new System.Drawing.Size(1018, 24);
            this.ruler2.TabIndex = 2;
            this.ruler2.Target = this.fastColoredTextBox1;
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.CharHeight = 14;
            this.fastColoredTextBox1.CharWidth = 8;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Enabled = false;
            this.fastColoredTextBox1.HighlightingRangeType = FastColoredTextBoxNS.HighlightingRangeType.AllTextRange;
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 48);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
            this.fastColoredTextBox1.Size = new System.Drawing.Size(882, 567);
            this.fastColoredTextBox1.TabIndex = 8;
            this.fastColoredTextBox1.Zoom = 100;
            // 
            // documentMap1
            // 
            this.documentMap1.BackColor = System.Drawing.SystemColors.Control;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(882, 48);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.ScrollbarVisible = false;
            this.documentMap1.Size = new System.Drawing.Size(136, 567);
            this.documentMap1.TabIndex = 3;
            this.documentMap1.Target = this.fastColoredTextBox1;
            this.documentMap1.Text = "documentMap1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.liveLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // liveLogToolStripMenuItem
            // 
            this.liveLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startLoggingToolStripMenuItem,
            this.stopLoggingInspectToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.exportLogsToolStripMenuItem});
            this.liveLogToolStripMenuItem.Name = "liveLogToolStripMenuItem";
            this.liveLogToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.liveLogToolStripMenuItem.Text = "Live Log";
            // 
            // startLoggingToolStripMenuItem
            // 
            this.startLoggingToolStripMenuItem.Name = "startLoggingToolStripMenuItem";
            this.startLoggingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.startLoggingToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.startLoggingToolStripMenuItem.Text = "Start Logging";
            this.startLoggingToolStripMenuItem.Click += new System.EventHandler(this.StartLoggingToolStripMenuItem_Click);
            // 
            // stopLoggingInspectToolStripMenuItem
            // 
            this.stopLoggingInspectToolStripMenuItem.Name = "stopLoggingInspectToolStripMenuItem";
            this.stopLoggingInspectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Pause)));
            this.stopLoggingInspectToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.stopLoggingInspectToolStripMenuItem.Text = "Stop Logging (Inspect)";
            this.stopLoggingInspectToolStripMenuItem.Click += new System.EventHandler(this.StopLoggingInspectToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // exportLogsToolStripMenuItem
            // 
            this.exportLogsToolStripMenuItem.Name = "exportLogsToolStripMenuItem";
            this.exportLogsToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.exportLogsToolStripMenuItem.Text = "Export Logs";
            this.exportLogsToolStripMenuItem.Click += new System.EventHandler(this.ExportLogsToolStripMenuItem_Click);
            // 
            // SyncTimer
            // 
            this.SyncTimer.Interval = 500;
            this.SyncTimer.Tick += new System.EventHandler(this.SyncTimer_Tick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // LogViewer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 615);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Controls.Add(this.documentMap1);
            this.Controls.Add(this.ruler2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LogViewer2";
            this.ShowIcon = false;
            this.Text = "LogViewer (Only Console)";
            this.Load += new System.EventHandler(this.LogViewer2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.Ruler ruler2;
        private FastColoredTextBoxNS.DocumentMap documentMap1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveLogToolStripMenuItem;
        private System.Windows.Forms.Timer SyncTimer;
        private System.Windows.Forms.ToolStripMenuItem startLoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopLoggingInspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}
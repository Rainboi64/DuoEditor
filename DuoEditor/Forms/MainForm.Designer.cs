namespace DuoEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViewerOnlyConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVeritcalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logVeiwerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logVeiwerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.exportFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.endToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsToolStripMenuItem,
            this.consoleToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1844, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip_ItemClicked);
            this.menuStrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MenuStrip_MouseMove);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.logViewerToolStripMenuItem,
            this.logViewerOnlyConsoleToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.dToolStripMenuItem,
            this.tileVeritcalToolStripMenuItem,
            this.closeAllWindowsToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            this.windowsToolStripMenuItem.Click += new System.EventHandler(this.windowsToolStripMenuItem_Click);
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.N)));
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newWindowToolStripMenuItem.Text = "New Window";
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.newWindowToolStripMenuItem_Click);
            // 
            // logViewerToolStripMenuItem
            // 
            this.logViewerToolStripMenuItem.Name = "logViewerToolStripMenuItem";
            this.logViewerToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.logViewerToolStripMenuItem.Text = "Log Viewer";
            this.logViewerToolStripMenuItem.Click += new System.EventHandler(this.LogViewerToolStripMenuItem_Click);
            // 
            // logViewerOnlyConsoleToolStripMenuItem
            // 
            this.logViewerOnlyConsoleToolStripMenuItem.Name = "logViewerOnlyConsoleToolStripMenuItem";
            this.logViewerOnlyConsoleToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.logViewerOnlyConsoleToolStripMenuItem.Text = "Log Viewer (Live Console)";
            this.logViewerOnlyConsoleToolStripMenuItem.Click += new System.EventHandler(this.LogViewerOnlyConsoleToolStripMenuItem_Click);
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.H)));
            this.dToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.dToolStripMenuItem.Text = "Tile Horizontal";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // tileVeritcalToolStripMenuItem
            // 
            this.tileVeritcalToolStripMenuItem.Name = "tileVeritcalToolStripMenuItem";
            this.tileVeritcalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.V)));
            this.tileVeritcalToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.tileVeritcalToolStripMenuItem.Text = "Tile Veritcal";
            this.tileVeritcalToolStripMenuItem.Click += new System.EventHandler(this.tileVeritcalToolStripMenuItem_Click);
            // 
            // closeAllWindowsToolStripMenuItem
            // 
            this.closeAllWindowsToolStripMenuItem.Name = "closeAllWindowsToolStripMenuItem";
            this.closeAllWindowsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.closeAllWindowsToolStripMenuItem.Text = "Close All Windows";
            this.closeAllWindowsToolStripMenuItem.Click += new System.EventHandler(this.closeAllWindowsToolStripMenuItem_Click);
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideConsoleToolStripMenuItem,
            this.newServerToolStripMenuItem1,
            this.logVeiwerToolStripMenuItem,
            this.logVeiwerToolStripMenuItem1,
            this.exportLogsToolStripMenuItem1});
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.consoleToolStripMenuItem.Text = "Server Console";
            // 
            // showHideConsoleToolStripMenuItem
            // 
            this.showHideConsoleToolStripMenuItem.Name = "showHideConsoleToolStripMenuItem";
            this.showHideConsoleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.showHideConsoleToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.showHideConsoleToolStripMenuItem.Text = "Show/Hide Console";
            this.showHideConsoleToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // newServerToolStripMenuItem1
            // 
            this.newServerToolStripMenuItem1.Name = "newServerToolStripMenuItem1";
            this.newServerToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.newServerToolStripMenuItem1.Text = "New Server";
            this.newServerToolStripMenuItem1.Click += new System.EventHandler(this.newServerToolStripMenuItem_Click);
            // 
            // logVeiwerToolStripMenuItem
            // 
            this.logVeiwerToolStripMenuItem.Name = "logVeiwerToolStripMenuItem";
            this.logVeiwerToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.logVeiwerToolStripMenuItem.Text = "Log Veiwer";
            this.logVeiwerToolStripMenuItem.Click += new System.EventHandler(this.LogViewerToolStripMenuItem_Click);
            // 
            // logVeiwerToolStripMenuItem1
            // 
            this.logVeiwerToolStripMenuItem1.Name = "logVeiwerToolStripMenuItem1";
            this.logVeiwerToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.logVeiwerToolStripMenuItem1.Text = "Log Veiwer (Live Console)";
            this.logVeiwerToolStripMenuItem1.Click += new System.EventHandler(this.LogViewerOnlyConsoleToolStripMenuItem_Click);
            // 
            // exportLogsToolStripMenuItem1
            // 
            this.exportLogsToolStripMenuItem1.Name = "exportLogsToolStripMenuItem1";
            this.exportLogsToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.exportLogsToolStripMenuItem1.Text = "Export Logs";
            this.exportLogsToolStripMenuItem1.Click += new System.EventHandler(this.ExportLogsToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.statusStrip.Location = new System.Drawing.Point(0, 24);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1844, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportFilesToolStripMenuItem1,
            this.endToolStripMenuItem1,
            this.creditsToolStripMenuItem1});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // exportFilesToolStripMenuItem1
            // 
            this.exportFilesToolStripMenuItem1.Name = "exportFilesToolStripMenuItem1";
            this.exportFilesToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Space)));
            this.exportFilesToolStripMenuItem1.Size = new System.Drawing.Size(230, 22);
            this.exportFilesToolStripMenuItem1.Text = "Export Files";
            this.exportFilesToolStripMenuItem1.Click += new System.EventHandler(this.exportFilesToolStripMenuItem_Click);
            // 
            // endToolStripMenuItem1
            // 
            this.endToolStripMenuItem1.Name = "endToolStripMenuItem1";
            this.endToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.endToolStripMenuItem1.Size = new System.Drawing.Size(230, 22);
            this.endToolStripMenuItem1.Text = "End";
            this.endToolStripMenuItem1.Click += new System.EventHandler(this.EndToolStripMenuItem_Click);
            // 
            // creditsToolStripMenuItem1
            // 
            this.creditsToolStripMenuItem1.Name = "creditsToolStripMenuItem1";
            this.creditsToolStripMenuItem1.Size = new System.Drawing.Size(230, 22);
            this.creditsToolStripMenuItem1.Text = "Credits";
            this.creditsToolStripMenuItem1.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1844, 856);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DuoEditor Parent";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVeritcalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllWindowsToolStripMenuItem;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.ToolStripMenuItem logViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logViewerOnlyConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem endToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showHideConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newServerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logVeiwerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logVeiwerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportLogsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportFilesToolStripMenuItem1;
    }
}




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
            this.newJavaScriptEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWebBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVeritcalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logVeiwerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.exportFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.endToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabForms = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newWindowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newJavaScriptEditorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.consoleControl1 = new ConsoleControl.ConsoleControl();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsToolStripMenuItem,
            this.consoleToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1844, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.newJavaScriptEditorToolStripMenuItem,
            this.newWebBrowserToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.dToolStripMenuItem,
            this.tileVeritcalToolStripMenuItem,
            this.closeAllWindowsToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newWindowToolStripMenuItem.Text = "New HTML Editor";
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.newWindowToolStripMenuItem_Click);
            // 
            // newJavaScriptEditorToolStripMenuItem
            // 
            this.newJavaScriptEditorToolStripMenuItem.Name = "newJavaScriptEditorToolStripMenuItem";
            this.newJavaScriptEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newJavaScriptEditorToolStripMenuItem.Text = "New JavaScript Editor";
            this.newJavaScriptEditorToolStripMenuItem.Click += new System.EventHandler(this.NewJavaScriptEditorToolStripMenuItem_Click);
            // 
            // newWebBrowserToolStripMenuItem
            // 
            this.newWebBrowserToolStripMenuItem.Name = "newWebBrowserToolStripMenuItem";
            this.newWebBrowserToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newWebBrowserToolStripMenuItem.Text = "New Web Browser";
            this.newWebBrowserToolStripMenuItem.Click += new System.EventHandler(this.NewWebBrowserToolStripMenuItem_Click);
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
            this.logVeiwerToolStripMenuItem,
            this.liveConsoleToolStripMenuItem,
            this.exportLogsToolStripMenuItem1});
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.consoleToolStripMenuItem.Text = "Console";
            // 
            // logVeiwerToolStripMenuItem
            // 
            this.logVeiwerToolStripMenuItem.Name = "logVeiwerToolStripMenuItem";
            this.logVeiwerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.logVeiwerToolStripMenuItem.Text = "Log Veiwer";
            this.logVeiwerToolStripMenuItem.Click += new System.EventHandler(this.LogViewerToolStripMenuItem_Click);
            // 
            // liveConsoleToolStripMenuItem
            // 
            this.liveConsoleToolStripMenuItem.Name = "liveConsoleToolStripMenuItem";
            this.liveConsoleToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.liveConsoleToolStripMenuItem.Text = "Live Console";
            this.liveConsoleToolStripMenuItem.Click += new System.EventHandler(this.LogViewerOnlyConsoleToolStripMenuItem_Click);
            // 
            // exportLogsToolStripMenuItem1
            // 
            this.exportLogsToolStripMenuItem1.Name = "exportLogsToolStripMenuItem1";
            this.exportLogsToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.exportLogsToolStripMenuItem1.Text = "Export Logs";
            this.exportLogsToolStripMenuItem1.Click += new System.EventHandler(this.ExportLogsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.ConfigurationToolStripMenuItem_Click);
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
            this.toolStripSplitButton1.Image = global::DuoEditor.Properties.Resources.Logo2;
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
            // tabForms
            // 
            this.tabForms.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabForms.Location = new System.Drawing.Point(0, 46);
            this.tabForms.Name = "tabForms";
            this.tabForms.SelectedIndex = 0;
            this.tabForms.Size = new System.Drawing.Size(1844, 23);
            this.tabForms.TabIndex = 4;
            this.tabForms.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabForms.DoubleClick += new System.EventHandler(this.TabForms_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem1,
            this.newJavaScriptEditorToolStripMenuItem1,
            this.clearLogsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 70);
            this.contextMenuStrip1.Text = "DuoEditor Parent";
            // 
            // newWindowToolStripMenuItem1
            // 
            this.newWindowToolStripMenuItem1.Name = "newWindowToolStripMenuItem1";
            this.newWindowToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.newWindowToolStripMenuItem1.Text = "New HTML Editor";
            this.newWindowToolStripMenuItem1.Click += new System.EventHandler(this.newWindowToolStripMenuItem_Click);
            // 
            // newJavaScriptEditorToolStripMenuItem1
            // 
            this.newJavaScriptEditorToolStripMenuItem1.Name = "newJavaScriptEditorToolStripMenuItem1";
            this.newJavaScriptEditorToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.newJavaScriptEditorToolStripMenuItem1.Text = "New JavaScript Editor";
            // 
            // clearLogsToolStripMenuItem
            // 
            this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.clearLogsToolStripMenuItem.Text = "Export Logs";
            this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.ExportLogsToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.ContextMenuStrip = this.contextMenuStrip2;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView1.Location = new System.Drawing.Point(1693, 69);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(151, 787);
            this.treeView1.TabIndex = 6;
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseDoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1690, 69);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 787);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // consoleControl1
            // 
            this.consoleControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.consoleControl1.IsInputEnabled = true;
            this.consoleControl1.Location = new System.Drawing.Point(0, 742);
            this.consoleControl1.Name = "consoleControl1";
            this.consoleControl1.SendKeyboardCommandsToProcess = false;
            this.consoleControl1.ShowDiagnostics = false;
            this.consoleControl1.Size = new System.Drawing.Size(1690, 114);
            this.consoleControl1.TabIndex = 9;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 732);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1690, 10);
            this.splitter2.TabIndex = 10;
            this.splitter2.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 710);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1690, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel1.Image = global::DuoEditor.Properties.Resources.Logo2;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(61, 17);
            this.toolStripStatusLabel1.Text = "  Ready";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1844, 856);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.consoleControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.tabForms);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DuoEditor Parent";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem endToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logVeiwerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLogsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportFilesToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabForms;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newJavaScriptEditorToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem newJavaScriptEditorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newWebBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private ConsoleControl.ConsoleControl consoleControl1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ToolStripMenuItem liveConsoleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}




using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using DuoLogger;
namespace DuoEditor
{

    public partial class MainForm : RibbonForm
    {
        public static string WORKINGFILENAME()
        {
            if (Settings.StartDirectory == "www")
            {
                return Path.Combine(Directory.GetCurrentDirectory() + "\\www\\");
            }
            else
            {
                return Settings.StartDirectory;
            }
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int sW_HIDE = 0;
        private const int sW_SHOW = 5;
        static Loading_Screen loadingscreen = new Loading_Screen();
        public MainForm()
        {
    
            InitializeComponent();
            FormClosing += form_FormClosing;
        }
    

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {   // User clicked 'X' button
                var x = MessageBox.Show("Do you really want to exit?", "DuoEditor " + PublicFuncs.APPVERSION, MessageBoxButtons.YesNo);

                if (x == DialogResult.Yes)
                {
                    CefSharp.Cef.Shutdown();
                    Logger.CleanLogs(); 
                    
                    consoleControl1.StopProcess();
                    Application.Exit();
                    try
                    {
                        Environment.Exit(0);
                    }
                    catch (Exception)
                    {
                    }
                }
                e.Cancel = true;             // Disable Form closing
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                foreach (MainBrowserForm childForm in this.MdiChildren.OfType<MainBrowserForm>())
                {
                    //Check for its corresponding MDI child form
                    if (childForm.TabPag.Equals(tabForms.SelectedTab))
                    {
                        //Activate the MDI child form
                        childForm.Select();

                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                foreach (Forms.MainJSEditor childForm in this.MdiChildren.OfType<Forms.MainJSEditor>())
                {
                    //Check for its corresponding MDI child form
                    if (childForm.TabPag.Equals(tabForms.SelectedTab))
                    {
                        //Activate the MDI child form
                        childForm.Select();
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                foreach (MainChildForm childForm in this.MdiChildren.OfType<MainChildForm>())
                {
                    //Check for its corresponding MDI child form
                    if (childForm.TabPag.Equals(tabForms.SelectedTab))
                    {
                        //Activate the MDI child form
                        childForm.Select();
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            DuoLogger.Logger.ProccesLogs();
            ribbonButton48.Click += RibbonButton48_Click;
            Update();
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            LoadUserPrefrences();
            this.Icon = new Icon(this.Icon, new Size(16, 16));

            consoleControl1.StartProcess("DuoServer.exe", "");

            ListDirectory(treeView1, WORKINGFILENAME());
            WindowCount1++;
            Form childForm = new Form();
            MainChildForm newMDIChild = new MainChildForm(null);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this; 
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + WindowCount1;
            //Add a Tabpage and enables it
            TabPage tp = new TabPage();
            tp.Parent = tabForms;
            tp.Text = newMDIChild.Text;
            tp.Show();

            //child Form will now hold a reference value to a tabpage
            newMDIChild.TabPag = tp;


            //Activate the newly created Tabpage
            tabForms.SelectedTab = tp;
            this.Activate();
           
          
        }

        private void RibbonButton48_Click(object sender, EventArgs e)
        {
            if (ribbon1.Minimized)
            {
                ribbon1.Minimized = false;
                ribbonButton48.Checked = ribbon1.Minimized;
                DuoDatabase.WRITE.CreateData("0","\\Configuration\\", "\\RibbonAutoHide");
                Update();
            }
            else
            {
                ribbon1.Minimized = true;
                ribbonButton48.Checked = ribbon1.Minimized;
                DuoDatabase.WRITE.CreateData("1", "\\Configuration\\", "\\RibbonAutoHide");
                Update();
            }
        }

        private void LoadUserPrefrences()
        {
            #region RibbonVisibility
            try
            {
                var AutoHide = DuoDatabase.READ.ReadData("\\Configuration\\", "\\RibbonAutoHide");
                if (AutoHide == "0")
                {
                    ribbon1.Minimized = false;
                    ribbonButton48.Checked = ribbon1.Minimized; Update();
                }
                else
                {
                    ribbon1.Minimized = true;
                    ribbonButton48.Checked = ribbon1.Minimized; Update();
                }
            } catch (Exception) { }
            #endregion
            #region ThemeStyle
            try
            {
                var ThemeStyle = DuoDatabase.READ.ReadData("\\Configuration\\", "\\ThemeStyle");
                if (ThemeStyle == "2013")
                {
                    ribbon1.OrbStyle = RibbonOrbStyle.Office_2013; Update(); ribbon1.OrbImage = null;
                }
                else if (ThemeStyle == "2010")
                {
                    ribbon1.OrbStyle = RibbonOrbStyle.Office_2010; Update();
                    ribbon1.OrbImage = null;
                }
                else if (ThemeStyle == "2007")
                {
                    ribbon1.OrbStyle = RibbonOrbStyle.Office_2007; Update();
                    ribbon1.OrbImage = Properties.Resources.Webp_net_resizeimage__4_;
                }
            } 
          
            catch (Exception) { }
            #endregion
            #region TreeViewVisibilty
            try
            {
                var TreeViewVisibility = DuoDatabase.READ.ReadData("\\Configuration\\", "\\TreeViewVisibility");
                if (TreeViewVisibility == "0")
                {
                    treeView1.Visible = false; splitter1.Visible = treeView1.Visible;
                    ribbonButton37.Text = "Show TreeView";
                }
                else
                {
                    treeView1.Visible = true; splitter1.Visible = treeView1.Visible;
                    ribbonButton37.Text = "Hide TreeView";
                }
            }
            catch (Exception) { }
            #endregion
            #region ConsoleVisibility
            try
            {
                var ConsoleVisibility = DuoDatabase.READ.ReadData("\\Configuration\\", "\\ConsoleVisibility");
                if (ConsoleVisibility == "0")
                {
                    consoleControl1.Visible = false;
                    ribbonButton25.LargeImage = Properties.Resources.icons8_eye_filled_50;
                    splitter2.Visible = consoleControl1.Visible;
                    ribbonButton25.Text = "Show Console";
                }
                else
                {
                    consoleControl1.Visible = true;
                    ribbonButton25.LargeImage = Properties.Resources.icons8_hide_filled_50;
                    splitter2.Visible = consoleControl1.Visible;
                    ribbonButton25.Text = "Hide Console";
                }

            }
            catch (Exception) { }
            #endregion
            #region ThemeColor
            try
            {
                var RibbonColorCode = DuoDatabase.READ.ReadData("\\Configuration\\", "\\ThemeColor");
                if (RibbonColorCode == "1")
                {
                    ribbon1.ThemeColor = RibbonTheme.JellyBelly;
                    this.Update();
                }
                else if (RibbonColorCode == "2")
                {
                    ribbon1.ThemeColor = RibbonTheme.Blue;
                    this.Update();
                }
                else if (RibbonColorCode == "3")
                {
                    ribbon1.ThemeColor = RibbonTheme.Green;
                    this.Update();
                }
                else if (RibbonColorCode == "4")
                {
                    ribbon1.ThemeColor = RibbonTheme.Purple;
                    this.Update();
                }
                else if (RibbonColorCode == "5")
                {
                    ribbon1.ThemeColor = RibbonTheme.Blue_2010;
                    this.Update();
                }
                else if (RibbonColorCode == "6")
                {
                    ribbon1.ThemeColor = RibbonTheme.Halloween;
                    this.Update();
                }
                else if (RibbonColorCode == "7")
                {
                    ribbon1.ThemeColor = RibbonTheme.Black;
                    this.Update();
                }
                else if (RibbonColorCode == "8")
                {
                    ribbon1.ThemeColor = RibbonTheme.Normal;
                    this.Update();
                }
            }
            catch (Exception) { } 
            #endregion
        }

        ImageList myImageList = new ImageList();
        Icon icon = Properties.Resources.FileIcon;
        private void ListDirectory(TreeView treeView, string Path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(Path);
            var _ = treeView.Nodes.Add(this.CreateDirectoryNode(rootDirectoryInfo));
            MyImageList.Images.Add(Icon1);
            treeView.ImageList = MyImageList;
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {

            MyImageList.Images.Clear();
            var directorynode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var _directorynode = directorynode.Nodes.Add(CreateDirectoryNode(directory));
                string filename = directory.FullName;


            }
            foreach (var file in directoryInfo.GetFiles())
            {
                string filename = file.FullName;
                Icon _icon = System.Drawing.Icon.ExtractAssociatedIcon(filename);
                directorynode.Nodes.Add(new TreeNode(file.Name));

            }

            return directorynode;
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void tileVeritcalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
        }

        private void fileSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowCount1++;
            Form childForm = new Form();
            MainChildForm newMDIChild = new MainChildForm(null);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + WindowCount1;
            //Add a Tabpage and enables it
            TabPage tp = new TabPage();
            tp.Parent = tabForms;
            tp.Text = newMDIChild.Text;
            tp.Show();

            //child Form will now hold a reference value to a tabpage
            newMDIChild.TabPag = tp;


            //Activate the newly created Tabpage
            tabForms.SelectedTab = tp;
        }

        private void closeAllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm != Parent)
                    {
                        frm.Close();
                    }
                }
            }
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" FastColoredTextbox by: Pavel Torgashov\n Zen Barcode Rendering Framework By: dementeddevil", "Credits To Lovely people", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            string fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Exports\\" + count.ToString() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond);

            try
            {
                System.IO.Directory.CreateDirectory(fileName);
                string DSsourcePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\DuoServer\\DuoServer.exe");
                string DSdestinationPath = System.IO.Path.Combine(fileName + "\\DuoServer.exe");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(DSsourcePath, DSdestinationPath, UIOption.AllDialogs);
                string sourcePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Settings.StartDirectory\\");
                // Choose a destination for the copied files.
                string destinationPath = System.IO.Path.Combine(fileName + "\\" + Settings.StartDirectory + "\\");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourcePath, destinationPath, UIOption.AllDialogs);
                count++;
                MessageBox.Show("Done! Copied To\n " + fileName + " \n Sucsessfully", "Coping Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception i) { Logger.LogEx(i); }

        }

        private void newServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Dir = Interaction.InputBox("Enter The Location you want to host", "Host A Server", Settings.StartDirectory, -1, -1);
            try
            {
                PublicFuncs.Host(Dir, Convert.ToInt32(Interaction.InputBox("Enter The Host Port", "Host A Server", "8080", -1, -1)));
            }
            catch (Exception i) { Logger.LogEx(i); }
        }

        private void ShowHideServerToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }
        private void EndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.CleanLogs();
        }

        private void LogViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart Runner = new ThreadStart(Run);
            Thread RunnerThread = new Thread(Run);
            RunnerThread.SetApartmentState(ApartmentState.STA);
            RunnerThread.Start();
            void Run()
            {
                DuoEditor.Forms.SubForms.LogVeiwer frm = new Forms.SubForms.LogVeiwer();
                frm.ShowDialog();
            }
        }
        private void LogViewerOnlyConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart Runner = new ThreadStart(Run);
            Thread RunnerThread = new Thread(Run);
            RunnerThread.SetApartmentState(ApartmentState.STA);
            RunnerThread.Start();
            void Run()
            {
                DuoEditor.Forms.SubForms.LogViewer2 frm = new Forms.SubForms.LogViewer2();
                frm.ShowDialog();
            }
        }



        private void TabForms_DoubleClick(object sender, EventArgs e)
        {
            string NewTabname = Interaction.InputBox("Enter a new name for your tab please:\nNote: Changing this wont effect any outcome.", "Change The Tab's name.", tabForms.SelectedTab.Text);
            if (!(NewTabname == ""))
            {
                tabForms.SelectedTab.Text = NewTabname;
                this.ActiveMdiChild.Text = NewTabname;
            }
        }

        private void NewJavaScriptEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FILENAME = null;
            try
            {
                FILENAME = DuoDatabase.READ.ReadData("\\LastOpened\\", "MainJSEditor");
            }
            catch (Exception)
            {
            }
            JSWindowCount1++;
            Form childForm = new Form();
            Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(FILENAME);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount1;
            //Add a Tabpage and enables it
            TabPage tp = new TabPage();
            tp.Parent = tabForms;
            tp.Text = newMDIChild.Text;
            tp.Show();

            //child Form will now hold a reference value to a tabpage
            newMDIChild.TabPag = tp;


            //Activate the newly created Tabpage
            tabForms.SelectedTab = tp;
        }

        private void TreeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string TreeNodeNameAndParents = treeView1.SelectedNode.FullPath;
                if (System.IO.Path.GetExtension(TreeNodeNameAndParents) == ".js")
                {
                    JSWindowCount1++;
                    Form childForm = new Form();
                    Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(TreeNodeNameAndParents);
                    // Set the Parent Form of the Child window.  
                    newMDIChild.MdiParent = this;
                    // Display the new form.  
                    newMDIChild.Show();
                    newMDIChild.TabCtrl = tabForms;
                    newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount1;
                    //Add a Tabpage and enables it
                    TabPage tp = new TabPage();
                    tp.Parent = tabForms;
                    tp.Text = newMDIChild.Text;
                    tp.Show();

                    //child Form will now hold a reference value to a tabpage
                    newMDIChild.TabPag = tp;


                    //Activate the newly created Tabpage
                    tabForms.SelectedTab = tp;
                }
                else if (System.IO.Path.GetExtension(TreeNodeNameAndParents) == ".html")
                {
                    WindowCount1++;
                    Form childForm = new Form();
                    MainChildForm newMDIChild = new MainChildForm(TreeNodeNameAndParents);
                    // Set the Parent Form of the Child window.  
                    newMDIChild.MdiParent = this;
                    // Display the new form.  
                    newMDIChild.Show();
                    newMDIChild.TabCtrl = tabForms;
                    newMDIChild.Text = newMDIChild.Text + " " + WindowCount1;
                    //Add a Tabpage and enables it
                    TabPage tp = new TabPage();
                    tp.Parent = tabForms;
                    tp.Text = newMDIChild.Text;
                    tp.Show();

                    //child Form will now hold a reference value to a tabpage
                    newMDIChild.TabPag = tp;


                    //Activate the newly created Tabpage
                    tabForms.SelectedTab = tp;
                }
            }
            catch (Exception) { }
        }
        int BrowserCount = 0;

        public int WindowCount1 { get; set; } = 0;
        public int JSWindowCount1 { get; set; } = 0;

        public static int SW_HIDE => sW_HIDE;

        public static int SW_SHOW => sW_SHOW;

        public ImageList MyImageList { get => myImageList; set => myImageList = value; }
        public Icon Icon1 { get => icon; set => icon = value; }
        public int BrowserCount1 { get => BrowserCount; set => BrowserCount = value; }

        private void NewWebBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowserCount1++;
            Form childForm = new Form();
            MainBrowserForm newMDIChild = new MainBrowserForm(null);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + BrowserCount1;
           
            //Add a Tabpage and enables it
            TabPage tp = new TabPage();
            tp.Parent = tabForms;
            tp.Text = newMDIChild.Text;
            tp.Show();

            //child Form will now hold a reference value to a tabpage
            newMDIChild.TabPag = tp;


            //Activate the newly created Tabpage
            tabForms.SelectedTab = tp;
        }

        private void LogSyncer_Tick(object sender, EventArgs e)
        {
        }

        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.SubForms.OptionDialogs.MainConfigs mainConfigs = new Forms.SubForms.OptionDialogs.MainConfigs();
            mainConfigs.Show();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListDirectory(treeView1, WORKINGFILENAME());
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop,false);
            foreach (string Filename in files)
            {
                try
                {
                    if (System.IO.Path.GetExtension(Filename) == ".js")
                    {
                        JSWindowCount1++;
                        Form childForm = new Form();
                        Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount1;
                        //Add a Tabpage and enables it
                        TabPage tp = new TabPage();
                        tp.Parent = tabForms;
                        tp.Text = newMDIChild.Text;
                        tp.Show();

                        //child Form will now hold a reference value to a tabpage
                        newMDIChild.TabPag = tp;


                        //Activate the newly created Tabpage
                        tabForms.SelectedTab = tp;
                    }
                    else if (System.IO.Path.GetExtension(Filename) == ".html")
                    {
                        WindowCount1++;
                        Form childForm = new Form();
                        MainChildForm newMDIChild = new MainChildForm(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + WindowCount1;
                        //Add a Tabpage and enables it
                        TabPage tp = new TabPage();
                        tp.Parent = tabForms;
                        tp.Text = newMDIChild.Text;
                        tp.Show();

                        //child Form will now hold a reference value to a tabpage
                        newMDIChild.TabPag = tp;


                        //Activate the newly created Tabpage
                        tabForms.SelectedTab = tp;
                    }
                }
                catch (Exception) { }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {

        e.Effect = DragDropEffects.All;
        }

        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string Filename in files)
            {
                try
                {
                    if (System.IO.Path.GetExtension(Filename) == ".js")
                    {
                        JSWindowCount1++;
                        Form childForm = new Form();
                        Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount1;
                        //Add a Tabpage and enables it
                        TabPage tp = new TabPage();
                        tp.Parent = tabForms;
                        tp.Text = newMDIChild.Text;
                        tp.Show();

                        //child Form will now hold a reference value to a tabpage
                        newMDIChild.TabPag = tp;


                        //Activate the newly created Tabpage
                        tabForms.SelectedTab = tp;
                    }
                    else if (System.IO.Path.GetExtension(Filename) == ".html")
                    {
                        WindowCount1++;
                        Form childForm = new Form();
                        MainChildForm newMDIChild = new MainChildForm(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + WindowCount1;
                        //Add a Tabpage and enables it
                        TabPage tp = new TabPage();
                        tp.Parent = tabForms;
                        tp.Text = newMDIChild.Text;
                        tp.Show();

                        //child Form will now hold a reference value to a tabpage
                        newMDIChild.TabPag = tp;


                        //Activate the newly created Tabpage
                        tabForms.SelectedTab = tp;
                    }
                }
                catch (Exception) { }
            }
        }

        private void DebugLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.SubForms.LogViewers.Logs_Debugger x = new Forms.SubForms.LogViewers.Logs_Debugger();
            x.Show();
        }

        private void RibbonButton21_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.WindowState = FormWindowState.Minimized;
        }

        private void RibbonButton22_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
        }

        private void RibbonButton23_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.WindowState = FormWindowState.Normal;
        }

        private void RibbonButton24_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }

        private void RibbonButton25_Click(object sender, EventArgs e)
        {
            if (consoleControl1.Visible)
            {
                consoleControl1.Visible = false; splitter2.Visible = consoleControl1.Visible;
                ribbonButton25.LargeImage = Properties.Resources.icons8_eye_filled_50;
                ribbonButton25.Text = "Show Console";
                DuoDatabase.WRITE.CreateData("0","\\Configuration\\","\\ConsoleVisibility");
            }
            else
            {
                consoleControl1.Visible = true; splitter2.Visible = consoleControl1.Visible;
                ribbonButton25.LargeImage = Properties.Resources.icons8_hide_filled_50;
                ribbonButton25.Text = "Hide Console";
                DuoDatabase.WRITE.CreateData("1", "\\Configuration\\", "\\ConsoleVisibility");
            }
        }

        private void MenuStrip1_ItemRemoved(object sender, ToolStripItemEventArgs e)
        {
          
                menuStrip1.Visible = false;
            
        }

        private void MenuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            menuStrip1.Visible = true;
        }

        private void Ribbon1_Click(object sender, EventArgs e)
        {

        }

        private void RibbonButton29_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.JellyBelly;
            this.Update();
            DuoDatabase.WRITE.CreateData("1","\\Configuration\\","\\ThemeColor");
        }

        private void RibbonButton30_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Blue;
            this.Update();
            DuoDatabase.WRITE.CreateData("2", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton31_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Green;
            this.Update();
            DuoDatabase.WRITE.CreateData("3", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton32_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Purple;
            this.Update();
            DuoDatabase.WRITE.CreateData("4", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton33_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Blue_2010;
            this.Update();
            DuoDatabase.WRITE.CreateData("5", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton34_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Halloween;
            this.Update();
            DuoDatabase.WRITE.CreateData("6", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton35_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Black;
            this.Update();
            DuoDatabase.WRITE.CreateData("7", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton36_Click(object sender, EventArgs e)
        {
            ribbon1.ThemeColor = RibbonTheme.Normal;
            this.Update();
            DuoDatabase.WRITE.CreateData("8", "\\Configuration\\", "\\ThemeColor");
        }

        private void RibbonButton37_Click(object sender, EventArgs e)
        {
            if (treeView1.Visible)
            {
                treeView1.Visible = false;
                splitter1.Visible = treeView1.Visible;
                ribbonButton37.Text = "Show TreeView";
                DuoDatabase.WRITE.CreateData("0", "\\Configuration\\", "\\TreeViewVisibility");
            }
            else
            {
                treeView1.Visible = true;
                splitter1.Visible = treeView1.Visible;
                ribbonButton37.Text = "Hide TreeView";
                DuoDatabase.WRITE.CreateData("1", "\\Configuration\\", "\\TreeViewVisibility");
            }

        }

        private void RibbonButton38_Click(object sender, EventArgs e)
        {
            ribbon1.OrbStyle = RibbonOrbStyle.Office_2013; Update(); ribbon1.OrbImage = null;
            DuoDatabase.WRITE.CreateData("2013", "\\Configuration\\", "\\ThemeStyle");
        }

        private void RibbonButton39_Click(object sender, EventArgs e)
        {
            ribbon1.OrbStyle = RibbonOrbStyle.Office_2010; Update(); ribbon1.OrbImage = null;
            DuoDatabase.WRITE.CreateData("2010", "\\Configuration\\", "\\ThemeStyle");
        }

        private void RibbonButton40_Click(object sender, EventArgs e)
        {
            ribbon1.OrbStyle = RibbonOrbStyle.Office_2007;
            Update();
            ribbon1.OrbImage = Properties.Resources.Webp_net_resizeimage__4_;
            DuoDatabase.WRITE.CreateData("2007", "\\Configuration\\", "\\ThemeStyle");
        }

        private void RibbonButton41_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void RibbonButton42_Click(object sender, EventArgs e)
        {
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void RibbonButton72_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Layout(object sender, LayoutEventArgs e)
        {
    
        }
    }
    
}

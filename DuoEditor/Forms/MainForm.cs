using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using DuoLogger;
namespace DuoEditor
{

    public partial class MainForm : Form
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
        int WindowCount = 0;

        int JSWindowCount = 0;
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
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
                    Environment.Exit(1);
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
           
            this.Activate(); 
            consoleControl1.StartProcess("DuoServer.exe", "");
        
                ListDirectory(treeView1, WORKINGFILENAME());
                WindowCount++;
                Form childForm = new Form();
                MainChildForm newMDIChild = new MainChildForm(null);
                // Set the Parent Form of the Child window.  
                newMDIChild.MdiParent = this;
                // Display the new form.  
                newMDIChild.Show();
                newMDIChild.TabCtrl = tabForms;
                newMDIChild.Text = newMDIChild.Text + " " + WindowCount;
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
        ImageList myImageList = new ImageList();
        Icon icon = DuoEditor.Properties.Resources.FileIcon;
        private void ListDirectory(TreeView treeView, string Path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(Path);
            var _ = treeView.Nodes.Add(this.CreateDirectoryNode(rootDirectoryInfo));
            myImageList.Images.Add(icon);
            treeView.ImageList = myImageList;
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {

            myImageList.Images.Clear();
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
            string File1 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Settings.StartDirectory + "\\");
            string File2 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Settings.StartDirectory + "\\uploaded_images");
            if (!System.IO.Directory.Exists(File1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Log("\n" + "Creating File" + File1 + "\n");
                Console.ResetColor();

                System.IO.Directory.CreateDirectory(File1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Log("\n" + " Main Form : File: " + File1 + " already exists." + "\n");
                Console.ResetColor();

            }
            if (!System.IO.Directory.Exists(File2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Log("\n" + "Main Form : Creating File:" + File2 + "\n");
                Console.ResetColor();

                System.IO.Directory.CreateDirectory(File2);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Log("\n" + "Main Form : File : " + File2 + " already exists." + "\n");
                Console.ResetColor();
                return;
            }
        }
        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowCount++;
            Form childForm = new Form();
            MainChildForm newMDIChild = new MainChildForm(null);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + WindowCount;
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
            Environment.Exit(1);
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
            JSWindowCount++;
            Form childForm = new Form();
            Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(FILENAME);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount;
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
                    JSWindowCount++;
                    Form childForm = new Form();
                    Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(TreeNodeNameAndParents);
                    // Set the Parent Form of the Child window.  
                    newMDIChild.MdiParent = this;
                    // Display the new form.  
                    newMDIChild.Show();
                    newMDIChild.TabCtrl = tabForms;
                    newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount;
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
                    WindowCount++;
                    Form childForm = new Form();
                    MainChildForm newMDIChild = new MainChildForm(TreeNodeNameAndParents);
                    // Set the Parent Form of the Child window.  
                    newMDIChild.MdiParent = this;
                    // Display the new form.  
                    newMDIChild.Show();
                    newMDIChild.TabCtrl = tabForms;
                    newMDIChild.Text = newMDIChild.Text + " " + WindowCount;
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

        private void NewWebBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowCount++;
            Form childForm = new Form();
            MainBrowserForm newMDIChild = new MainBrowserForm(null);
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.TabCtrl = tabForms;
            newMDIChild.Text = newMDIChild.Text + " " + WindowCount;
           
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
                        JSWindowCount++;
                        Form childForm = new Form();
                        Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount;
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
                        WindowCount++;
                        Form childForm = new Form();
                        MainChildForm newMDIChild = new MainChildForm(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + WindowCount;
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
                        JSWindowCount++;
                        Form childForm = new Form();
                        Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + JSWindowCount;
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
                        WindowCount++;
                        Form childForm = new Form();
                        MainChildForm newMDIChild = new MainChildForm(Path.Combine(Filename));
                        // Set the Parent Form of the Child window.  
                        newMDIChild.MdiParent = this;
                        // Display the new form.  
                        newMDIChild.Show();
                        newMDIChild.TabCtrl = tabForms;
                        newMDIChild.Text = newMDIChild.Text + " " + WindowCount;
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
    }
    
}

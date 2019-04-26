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
namespace DuoEditor
{
     
    public partial class MainForm : Form
    {

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
                var x = MessageBox.Show("Do you really want to exit?","DuoEditor 1.3",MessageBoxButtons.YesNo);
                if (x == DialogResult.Yes)
                {
                    Environment.Exit(1);
                } e.Cancel = true;             // Disable Form closing
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

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
         
            WindowCount++;
            Form childForm = new Form();
            MainChildForm newMDIChild = new MainChildForm();
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
           string File1 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) +"\\www\\") ;
            string File2 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\www\\uploaded_images");
            if (!System.IO.Directory.Exists(File1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
           Logger.Log("\n"+"Creating File" + File1 + "\n");
                Console.ResetColor();
              
                System.IO.Directory.CreateDirectory(File1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
           Logger.Log("\n"+" Main Form : File: "+ File1 + " already exists."+"\n");
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
           Logger.Log("\n" + "Main Form : File : " +File2+ " already exists."+"\n");
                Console.ResetColor();
                return;
            }
        }
        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowCount++;
            Form childForm = new Form();
            MainChildForm newMDIChild = new MainChildForm();
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
            MessageBox.Show(" FastColoredTextbox by: Pavel Torgashov\n Zen Barcode Rendering Framework By: dementeddevil", "Credits To Lovely people",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                    string wwwsourcePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\www\\"); 
                    // Choose a destination for the copied files.
                    string wwwdestinationPath = System.IO.Path.Combine( fileName+ "\\www\\");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(wwwsourcePath, wwwdestinationPath, UIOption.AllDialogs);
                count++;
                MessageBox.Show("Done! Copied To\n " + fileName +" \n Sucsessfully", "Coping Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                catch (Exception i) { Logger.LogEx(i); }
            
        }

        private void newServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Dir = Interaction.InputBox("Enter The Location you want to host", "Host A Server", "www", -1, -1);
            try
            {
                PublicFuncs.Host(Dir, Convert.ToInt32(Interaction.InputBox("Enter The Host Port", "Host A Server", "8080", -1, -1)));
            } catch (Exception i) { Logger.LogEx(i); }
        }
        
        private void ShowHideServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (PublicFuncs.shwn == false) { PublicFuncs.shwn = true; MainServer.ShowConsole(); } else { PublicFuncs.shwn = false; MainServer.HideConsole(); }
           
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
            void Run(){
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
            JSWindowCount++;
            Form childForm = new Form();
            Forms.MainJSEditor newMDIChild = new Forms.MainJSEditor();
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
    }

}

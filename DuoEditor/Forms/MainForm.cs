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
namespace DuoEditor
{
     
    public partial class MainForm : Form
    {
     
   
        public MainForm()
        {
            InitializeComponent();
         
        }

  
        private void MainForm_Load(object sender, EventArgs e)
        {
              Form EditorchildForm = new Form();
            MainChildForm newMDIChild = new MainChildForm();
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
     
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        

        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void windowsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void wipeTheProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
           string File1 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) +"\\www\\") ;
            string File2 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\www\\uploaded_images");
            if (!System.IO.Directory.Exists(File1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n"+"Creating File" + File1 + "\n");
                Console.ResetColor();
              
                System.IO.Directory.CreateDirectory(File1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n"+" Main Form : File: \"{0}\" already exists."+"\n", File1);
                Console.ResetColor();
               
            }
            if (!System.IO.Directory.Exists(File2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + "Main Form : Creating File:" + File2 + "\n");
                Console.ResetColor();

                System.IO.Directory.CreateDirectory(File2);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + "Main Form : File : " +File2+ " already exists."+"\n");
                Console.ResetColor();
                return;
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Form();
            MainChildForm newMDIChild = new MainChildForm();
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
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
                    string wwwsourcePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\www\\"); ;
                    // Choose a destination for the copied files.
                    string wwwdestinationPath = System.IO.Path.Combine( fileName+ "\\www\\");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(wwwsourcePath, wwwdestinationPath, UIOption.AllDialogs);
                count++;
                MessageBox.Show("Done! Copied To\n " + fileName +" \n Sucsessfully", "Coping Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                catch (Exception i) { MessageBox.Show("Something Gone Bad here are Some Info: "+i,"Opps Something went wrong",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            
        }

        private void newServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Dir = Interaction.InputBox("Enter The Location you want to host", "Host A Server", "www", -1, -1);
            try
            {
                PublicVars.Host(Dir, Convert.ToInt32(Interaction.InputBox("Enter The Host Port", "Host A Server", "8080", -1, -1)));
            } catch (Exception i) { MessageBox.Show("Opps Somthing Bad Happend Here is info: " + i, "Opps Somthing Bad Happend", MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }
    }
}

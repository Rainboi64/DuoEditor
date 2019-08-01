using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Diagnostics;

namespace OpenY_Studio
{
    public partial class Form1 : RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        FastColoredTextBoxNS.AutocompleteMenu popupMenu;
     
        private void Form1_Load(object sender, EventArgs e)
        {
            consoleControl1.StartProcess("OpenY Prompt.exe", "");
           popupMenu = new FastColoredTextBoxNS.AutocompleteMenu(fastColoredTextBox1);
            popupMenu.MinFragmentLength = 2;
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
            popupMenu.AllowTabKey = true;

            LoadDirectory("E:\\Trash\\");
            
            var OpenYWords = new List<string>();
            OpenYWords.Add("<String></String>");
            OpenYWords.Add("<Int32></Int32>");
            OpenYWords.Add("<Screen></Screen>");
            OpenYWords.Add("<InputLine>");
            OpenYWords.Add("<InputChar>");
            OpenYWords.Add("<ClearScreen>");
            OpenYWords.Add("<Halt></Halt>");
            fastColoredTextBox1.Language = Language.XML;
            //set words as autocomplete source
            popupMenu.Items.SetAutocompleteItems(OpenYWords);
            //size of popupmenu
            popupMenu.Items.MaximumSize = new System.Drawing.Size(200, 300);
            popupMenu.Items.Width = 200;
        }
        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value  
          
            TreeNode tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }
        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories  
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories  
            foreach (string subdirectory in subdirectoryEntries)
            {

                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);

            }
        }
        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");

            // Loop through them to see files  
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;

            }
        }

        private void FastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
                
            

        }

        private void FastColoredTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //forced show (MinFragmentLength will be ignored)
            if(e.KeyChar == '<')
            popupMenu.Show(true);
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Cut();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Paste();
        }

        private void ToolStripSeparator1_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Paste();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Undo();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Redo();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.SelectAll();
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.ShowFindDialog();
        }

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.ShowReplaceDialog();
        }

        private void FoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.CollapseBlock(fastColoredTextBox1.Selection.Start.iLine,
                  fastColoredTextBox1.Selection.End.iLine);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Copy();
        }

        private void RibbonButton21_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.BookmarkLine(fastColoredTextBox1.Selection.Start.iLine);
        }

        private void RibbonButton22_Click(object sender, EventArgs e)
        {
           
        }

        private void RibbonButton12_Click(object sender, EventArgs e)
        {
            consoleControl2.InternalRichTextBox.Clear();
            Console.SetOut(new ControlWriter(consoleControl2.InternalRichTextBox));
            OpenY_Compiler.Compiler compiler = new OpenY_Compiler.Compiler();
            compiler.Lexxer(fastColoredTextBox1.Text);
        }

        private void RibbonButton27_Click(object sender, EventArgs e)
        {
   
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do You Want To Save Your Work?","Saving Your Work",MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void RibbonButton20_Click(object sender, EventArgs e)
        {
            
        }

        private void RibbonButton32_Click(object sender, EventArgs e)
        {
            Hide();
            Show();
        }

        private void RibbonButton14_Click(object sender, EventArgs e)
        {
            StreamWriter txtoutput = new StreamWriter("temp");
            txtoutput.Write(fastColoredTextBox1.Text);
            txtoutput.Close();
            Process.Start("PowerShell","-NoExit -Command ./OpenY.exe temp");
        }
    }
}

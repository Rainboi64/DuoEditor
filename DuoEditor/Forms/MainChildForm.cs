#region References
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using Microsoft.VisualBasic;
#endregion
namespace DuoEditor
{
    /// <summary>
    /// This Is The Child form Containing All of the Editor Stuff
    /// </summary>
    public partial class MainChildForm : Form
    {
        public MainChildForm()
        {
            InitializeComponent();


        }
        #region Vars
        string Filename;
        string SafeFilename;
#endregion

        private void MainChildForm_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// This is the prelaunch setup
            /// </summary>
            ///This Code below loads the highlighting config to the HTMLCodeTextBox1
            HTMLCodeTextBox1.DescriptionFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Files/htmlDesc.xml");
            ///This Code below Adds The Dummy Text To The HTMLCodeTextBox1
            HTMLCodeTextBox1.Text = "<! DuoEditor Version Alpha 1.1 \n Credits: Main Tasks Then Credits>";
            ///This Code below Assigns The DocumentMap1 To The HTMLCodeTextBox1
            documentMap1.Target = HTMLCodeTextBox1;
            ///This Code below Disables WebBrowsers Script Errors
            NormalVeiwWB.ScriptErrorsSuppressed = true;
            LiveBrowserWB.ScriptErrorsSuppressed = true;
            ///This Code below Add Sets The Text Of URLTextBox1
            URLTextBox1.Text = "http://" + PublicVars.CleanIp;
            ///This Code below Reports Status to the Console
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n" + " Form Report: A Child Form was launched" + "\n");
            Console.ResetColor();
        }


        #region New, Save, Open, Exit
        /// <summary>
        /// This is the region where the New, Save, Open, Exit buttons are implemented
        /// </summary>

        /// 
        /// Controls Closing the Window
        /// 
        private void closeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hides This Window
            this.Hide();
        }

        ///
        /// Controls Saving And Running
        /// 
        private void compileRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Saves The File And Runs it
            try
            {
                if (Filename == null)
                {
                    string savefile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\www\\" + (Interaction.InputBox("Choose file name", "Save", "File", -1, -1) + ".html"));
                    StreamWriter txtoutput = new StreamWriter(savefile);
                    txtoutput.Write(HTMLCodeTextBox1.Text);
                    txtoutput.Close();
                    Filename = savefile;
                    SafeFilename = "/" + Path.GetFileName(savefile);
                    URLTextBox1.Text = "http://" + PublicVars.CleanIp + SafeFilename;
                }
                else
                {
                    StreamWriter txtoutput = new StreamWriter(Filename);
                    txtoutput.Write(HTMLCodeTextBox1.Text);
                    txtoutput.Close();
                    SafeFilename = "/" + Path.GetFileName(Filename);
                    URLTextBox1.Text = "http://" + PublicVars.CleanIp + SafeFilename;
                }
                this.NormalVeiwWB.Navigate(URLTextBox1.Text);
            }
            catch (Exception i) { }
        }

        ///
        /// Controls Making Things New
        ///
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Wipes all parameters to look new
            URLTextBox1.Text = "http://" + PublicVars.CleanIp;
            SafeFilename = null;
            HTMLCodeTextBox1.Clear();
            Filename = null;
        }

        ///
        /// Controls Opening Files
        ///
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Files
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Open File";
            openfile.Filter = "HTML|*.html|All Files|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                HTMLCodeTextBox1.Clear();
                using (StreamReader sr = new StreamReader(openfile.FileName))
                {
                    HTMLCodeTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                }
                SafeFilename = openfile.SafeFileName;
                URLTextBox1.Text = "http://" + PublicVars.CleanIp + "/" + SafeFilename;
                this.NormalVeiwWB.Navigate(URLTextBox1.Text);

            }
        }

        ///
        /// Controls Saving
        ///
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Title = "Save file as..";
                savefile.Filter = "HTML|*.html|All Files|*.*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                    txtoutput.Write(HTMLCodeTextBox1.Text);
                    txtoutput.Close();
                }
                Filename = savefile.FileName;
                SafeFilename = "/" + Path.GetFileName(savefile.FileName);
                URLTextBox1.Text = "http://" + PublicVars.CleanIp + SafeFilename;

            }
            catch (Exception i) { }
        }

        /// Could add Some Changes Later
        #endregion
        #region Browser Commands
        /// <summary>
        /// This is The Region Where The Browser Controls Code is located
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///  For Normal WebBrowser
        /// //Refreshes WebBrowser
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Refresh();
        }
        //Stops WebBrowser
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Stop();
  
        }
        //Go back WebBrowser
        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.GoBack();
        }
        //Go Forward WebBrowser
        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.GoForward();
        }
        //Updates WebBrowser
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Update();
        }
        //Prints WebBrowser
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.ShowPrintDialog();
        }
        /// This is for the Live Browser
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.Refresh();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.GoBack();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.GoForward();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.Stop();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.Update();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.ShowPrintDialog();
        }
        #endregion
        #region RTB Commands
        /// <summary>
        /// This is the Region where The FastTextBox Controls Are:
        /// Credits To FastTextBox Maker: Pavel Torgashov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foldToolStripMenuItem_Click(object sender, EventArgs e)
        {

            HTMLCodeTextBox1.CollapseBlock(HTMLCodeTextBox1.Selection.Start.iLine,
              HTMLCodeTextBox1.Selection.End.iLine);

        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Redo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Paste();
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.ShowReplaceDialog();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Zoom = HTMLCodeTextBox1.Zoom + 10;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLCodeTextBox1.Zoom = HTMLCodeTextBox1.Zoom - 10;
        }
        #endregion
        #region Insterters 
        /// <summary>
        /// Here is the region where HTML Inserters are Used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.ParagraphInserter Hinsert = new DuoEditor.Forms.SubForms.ParagraphInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {

                    HTMLCodeTextBox1.InsertText(Hinsert.ReturnParagraph);
                }
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.HyperLinkInserter Hinsert = new DuoEditor.Forms.SubForms.HyperLinkInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {

                    HTMLCodeTextBox1.InsertText(Hinsert.ReturnHyperLink);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.ImageInserter Hinsert = new DuoEditor.Forms.SubForms.ImageInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {

                    HTMLCodeTextBox1.InsertText(Hinsert.ReturnImage);
                }
            }
        }
        private void headerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.HeaderInserter Hinsert = new DuoEditor.Forms.SubForms.HeaderInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {
                    HTMLCodeTextBox1.InsertText(Hinsert.ReturnHeader);
              
                }
            }
        }

        private void paragraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.ParagraphInserter Hinsert = new DuoEditor.Forms.SubForms.ParagraphInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {
                    HTMLCodeTextBox1.InsertText(Hinsert.ReturnParagraph);
              
                }
            }

        }

        private void hyperLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.HyperLinkInserter Hinsert = new DuoEditor.Forms.SubForms.HyperLinkInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {
                    HTMLCodeTextBox1.InsertText( Hinsert.ReturnHyperLink);
                 
                }
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DuoEditor.Forms.SubForms.ImageInserter Hinsert = new DuoEditor.Forms.SubForms.ImageInserter())
            {
                var result = Hinsert.ShowDialog();
                if (Hinsert.DialogResult == DialogResult.OK)
                {
                    HTMLCodeTextBox1.InsertText(Hinsert.ReturnImage);
          
                }
            }
        }
        #endregion
        #region Misc
        private void richTextBox31_TextChanged(object sender, TextChangedEventArgs e)
        {
            LiveBrowserWB.DocumentText = HTMLCodeTextBox1.Text;
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            PublicVars.ip = URLTextBox1.Text;
            DuoEditor.Forms.SubForms.BarCodeForm1 bc1 = new Forms.SubForms.BarCodeForm1();
            bc1.ShowDialog();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            PublicVars.ip = URLTextBox1.Text;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PublicVars.ip = URLTextBox1.Text;
        }
        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LiveBrowserWB.DocumentText = HTMLCodeTextBox1.Text;


            }
            catch (Exception) { }
        }





        private void Test_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PublicVars.ip);
        }

        #endregion
    }
}

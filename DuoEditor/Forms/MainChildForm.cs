#region References
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using DuoLogger;
using FastColoredTextBoxNS;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CefSharp;
using CefSharp.WinForms;
#endregion
namespace DuoEditor
{
    /// <summary>
    /// This Is The Child form Containing All of the Editor Stuff
    /// </summary>
    public partial class MainChildForm : Form
    {
        #region Vars
        string Filename;
        string SafeFilename;
        #endregion
        ChromiumWebBrowser LiveBrowserWB;
        public void InitializeLiveChromium()
        {

            //   CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            // Cef.Initialize(settings);
            // Create a browser component
            LiveBrowserWB = new ChromiumWebBrowser(Settings.StartIP);
            // Add it to the form and fill it to the form window.
            this.tabPageLive.Controls.Add(LiveBrowserWB);

            LiveBrowserWB.Dock = DockStyle.Fill;
        }
        ChromiumWebBrowser NormalVeiwWB;
        public void InitializeChromium()
        {

            // Create a browser component
            NormalVeiwWB = new ChromiumWebBrowser(Settings.StartIP);
            // Add it to the form and fill it to the form window.
            this.tabPageNView.Controls.Add(NormalVeiwWB);
      
            NormalVeiwWB.Dock = DockStyle.Fill;
            NormalVeiwWB.StatusMessage += NormalVeiwWB_StatusMessage;
            NormalVeiwWB.FrameLoadEnd += NormalVeiwWB_FrameLoadEnd;
       
        }

        private void NormalVeiwWB_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.HttpStatusCode != 200 && e.HttpStatusCode != 0)
            {
                try
                {
                    StatusLabel1.Text = "Error Code: " + e.HttpStatusCode;
                    StatusLabel1.Image = Properties.Resources.Red_Icon;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    StatusLabel1.Text = "Ready..";
                    StatusLabel1.Image = Properties.Resources.Green_Icon;
                }
                catch (Exception)
                {

                }
            }
        }

        private void NormalVeiwWB_StatusMessage(object sender, StatusMessageEventArgs e)
        {

            if (e.Browser.IsLoading)
            {
                StatusLabel1.Text = "Loading...";
                StatusLabel1.Image = Properties.Resources.Red_Icon;
            }
    
        }

        private TabControl tabCtrl;
        private TabPage tabPag;
        public TabPage TabPag
        {
            get
            {
                return tabPag;
            }
            set
            {
                tabPag = value;
            }
        }
        private void ActiveMdiChild_FormClosed(object sender,
                                    FormClosedEventArgs e)
        {

            try
            {
                DuoDatabase.WRITE.CreateData(Filename, "\\LastOpened\\", "MainChildForm");
            }
            catch (Exception)
            {
            }
            this.tabPag.Dispose();

            
        }
        public TabControl TabCtrl
        {
            set
            {
                tabCtrl = value;
            }
        }
      public static string ReceivedFileName;
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        public MainChildForm(string filename)
        {
            InitializeComponent();
            InitializeLiveChromium();
            InitializeChromium();
            this.tabPageNView.Controls.Add(this.menuStrip2);
            this.tabPageLive.Controls.Add(this.menuStrip4);
            this.tabPageNView.Controls.Add(this.toolStrip2);
            if (filename != null)
            {
                ReceivedFileName = Path.Combine(Path.GetDirectoryName(Settings.StartDirectory) + "\\" + filename);
            }
        }
   

        private void MainChildForm_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(this.Icon, new Size(16, 16));
            this.WindowState = FormWindowState.Maximized;
            try
            {
                foreach (string file in Directory.GetDirectories(Path.Combine(Directory.GetCurrentDirectory() + "\\Plugins\\MainHTMLEditor\\Inserters"))) CreateTool(file);

            }
            catch (Exception)
            {


            }            
            /// <summary>
            /// This is the prelaunch setup
            /// </summary>
            timer1.Start();
            ///This Code below loads the highlighting config to the HTMLCodeTextBox1
            HTMLCodeTextBox1.DescriptionFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Files/htmlDesc.xml");
            ///This Code below Adds The Dummy Text To The HTMLCodeTextBox1
            HTMLCodeTextBox1.Text = "<! DuoEditor Version Alpha "+PublicFuncs.APPVERSION+" \n Credits: Main Tasks Then Credits>";
            ///This Code below Assigns The DocumentMap1 To The HTMLCodeTextBox1
            documentMap1.Target = HTMLCodeTextBox1;
            ///This Code below Add Sets The Text Of URLTextBox1
            URLTextBox1.Text = "http://" + PublicFuncs.CleanIp;
            ///This Code below Reports Status to the Console
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n" + " Form Report: A Child Form was launched" + "\n");
            Console.ResetColor();
            this.Icon = new Icon(this.Icon, new Size(16, 16));

            if (ReceivedFileName != null)
                {
                    Thread.Sleep(500);
                    using (StreamReader sr = new StreamReader(ReceivedFileName))
                    {
                        HTMLCodeTextBox1.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                    Filename = ReceivedFileName;
                SafeFilename = Path.GetFullPath(Filename).Replace(Settings.StartDirectory,"").Replace("\\","/") ;
                    URLTextBox1.Text = "http://" + PublicFuncs.CleanIp + SafeFilename;
                    this.NormalVeiwWB.Load(URLTextBox1.Text);
                }
      
        }

        private void CreateTool(string file)
        {
            ToolStripButton toolStripButton = new ToolStripButton(Path.GetFileName(file));
            toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton.Tag = Path.GetFileName(file);
           toolStripButton.Image = Image.FromFile(Path.Combine(file + "\\icon"));
            toolStripButton.ImageAlign = ContentAlignment.MiddleCenter;
            toolStripButton.ImageScaling = ToolStripItemImageScaling.SizeToFit;
            toolStripButton.Click += ToolStripButton_Click;
            toolStrip1.Items.Add(toolStripButton);
        }
        private const int SW_MAXIMIZE = 3;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            string filename = (Path.Combine(Directory.GetCurrentDirectory()+ "\\Plugins\\MainHTMLEditor\\Inserters\\" + sender.ToString()+("\\"+sender.ToString()+".exe")));
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory() + "\\Plugins\\MainHTMLEditor\\Inserters\\" + sender.ToString());
            process.StartInfo.FileName = filename;
            process.Start();
            process.WaitForExit();
  
            int trials = 0;
            Thread.Sleep(500);
            while (true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory() + "\\Plugins\\MainHTMLEditor\\Inserters\\" + sender.ToString() + "\\Return")))
                    {
                        HTMLCodeTextBox1.InsertText(sr.ReadToEnd());
                        sr.Close();

                        File.Delete(Path.Combine(Directory.GetCurrentDirectory() + "\\Plugins\\MainHTMLEditor\\Inserters\\" + sender.ToString() + "\\Return"));
                        break;
                    }

                }
                catch (Exception exp)
                {
                    if (exp.Message.Contains("file")|| exp.Message.Contains("File"))
                    {
                        if (trials == 25)
                        {
                          var result =  MessageBox.Show("Couldn't Get Result after 25 trials, You can try again or, Abort The Plugin","Response Not Found",MessageBoxButtons.AbortRetryIgnore,MessageBoxIcon.Error);
                            if (result == DialogResult.Abort)
                            {
                                process.Close();
                                break;
                            }
                            else if (result == DialogResult.Retry)
                            {
                                try
                                {
                                    using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory() + "\\Plugins\\MainHTMLEditor\\Inserters\\" + sender.ToString() + "\\Return")))
                                    {
                                        HTMLCodeTextBox1.InsertText(sr.ReadToEnd());
                                        sr.Close();

                                        File.Delete(Path.Combine(Directory.GetCurrentDirectory() + "\\Plugins\\MainHTMLEditor\\Inserters\\" + sender.ToString() + "\\Return"));
                                        break;
                                    }


                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Failed");
                                    break;
                                }

                            }
                            else if (result == DialogResult.Cancel)
                            {
                                break;
                            }
                            else { break; }
                        }
                        trials++;
                    }
                } 
            }
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
                    string safefileName = Interaction.InputBox("Choose file name", "Save", "File", -1, -1);
                    string savefile = System.IO.Path.Combine(Settings.StartDirectory+"\\" + safefileName + ".html");
                    if (!(safefileName == "")) { 
                    StreamWriter txtoutput = new StreamWriter(savefile);
                    txtoutput.Write(HTMLCodeTextBox1.Text);
                    txtoutput.Close();
                    Filename = savefile;
                    SafeFilename = "/" + Path.GetFileName(savefile);
                    URLTextBox1.Text = "http://" + PublicFuncs.CleanIp + SafeFilename;
                }
            }
                else
                {
                    StreamWriter txtoutput = new StreamWriter(Filename);
                    txtoutput.Write(HTMLCodeTextBox1.Text);
                    txtoutput.Close();
                    SafeFilename = "/" + Path.GetFileName(Filename);
                    URLTextBox1.Text = "http://" + PublicFuncs.CleanIp + SafeFilename;
                }
                this.NormalVeiwWB.Load(URLTextBox1.Text);
            }
            catch (Exception i) { Logger.LogEx(i); }
        }

        ///
        /// Controls Making Things New
        ///
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Wipes all parameters to look new
            URLTextBox1.Text = "http://" + PublicFuncs.CleanIp;
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
                URLTextBox1.Text = "http://" + PublicFuncs.CleanIp + "/" + SafeFilename;
                NormalVeiwWB.Load(URLTextBox1.Text);

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
                URLTextBox1.Text = "http://" + PublicFuncs.CleanIp + SafeFilename;

            }
            catch (Exception i) { Logger.LogEx(i); }
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
            NormalVeiwWB.Reload();
            try
            {
                using (StreamReader sr = new StreamReader(Filename))
                {
                    HTMLCodeTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        //Stops WebBrowser
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Stop();
  
        }
        //Go back WebBrowser
        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Back();
        }
        //Go Forward WebBrowser
        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Forward();
        }
        //Updates WebBrowser
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Update();
        }
        //Prints WebBrowser
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormalVeiwWB.Print();
        }
        /// This is for the Live Browser
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.LoadHtml( HTMLCodeTextBox1.Text);
            LiveBrowserWB.Reload();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.Back();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            LiveBrowserWB.Forward();
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
            LiveBrowserWB.Print();
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
            LiveBrowserWB.LoadHtml( HTMLCodeTextBox1.Text);
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
            LiveBrowserWB.LoadHtml ( HTMLCodeTextBox1.Text);
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
            LiveBrowserWB.LoadHtml( HTMLCodeTextBox1.Text);
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
            LiveBrowserWB.LoadHtml ( HTMLCodeTextBox1.Text);
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
            LiveBrowserWB.LoadHtml( HTMLCodeTextBox1.Text);
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
            LiveBrowserWB.LoadHtml( HTMLCodeTextBox1.Text);
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
            LiveBrowserWB.LoadHtml(HTMLCodeTextBox1.Text);
        }
        #endregion
        #region Misc
        private void richTextBox31_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            PublicFuncs.ip = URLTextBox1.Text;
            DuoEditor.Forms.SubForms.BarCodeForm1 bc1 = new Forms.SubForms.BarCodeForm1();
            bc1.ShowDialog();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            PublicFuncs.ip = URLTextBox1.Text;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PublicFuncs.ip = URLTextBox1.Text;
        }
        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LiveBrowserWB.Text = HTMLCodeTextBox1.Text;


            }
            catch (Exception i) { Logger.LogEx(i); }
        }

        private void MDIChild_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        { 
            //Destroy the corresponding Tabpage when closing MDI child form
            this.tabPag.Dispose();

            //If no Tabpage left
            if (!tabCtrl.HasChildren)
            {
                tabCtrl.Visible = false;
            }
        }



        private void Test_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PublicFuncs.ip);
        }

        #endregion

        private void ClToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.CleanLogs();
        }

        private void MainChildForm_Activated(object sender, EventArgs e)
        {
            if ((splitContainer1.Panel1Collapsed == false))
            {
                menuStrip1.Visible = false;
                URLTextBox1.Font = new Font(URLTextBox1.Font.FontFamily, 27, FontStyle.Regular);
            }
        }
        private void MainChildForm_Deactivate(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed == false)
            {
                splitContainer1.Panel1.Visible = true;
                menuStrip1.Visible = true;
                URLTextBox1.Font = new Font(URLTextBox1.Font.FontFamily, 8.25f, FontStyle.Regular);
            }
        }
        private void HTMLCodeTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                LiveBrowserWB.LoadHtml ( HTMLCodeTextBox1.Text);
            }
            catch (Exception) { }
            }
        private void MainChildForm_Click(object sender, EventArgs e)
        {
  
        }

        private void URLTextBox1_DoubleClick(object sender, EventArgs e)
        {
 
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        bool URLhidden = true;
        private void HideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (URLhidden == false)
            {
                splitContainer1.Panel1Collapsed = URLhidden;
                URLhidden = true;
            }
            else
            {
                splitContainer1.Panel1Collapsed = URLhidden;
                URLhidden = false;
            }
        }
        bool Toolbarhidden = true;
        private void HideShowToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Toolbarhidden == false)
            {
                splitContainer3.Panel2Collapsed = Toolbarhidden;
                Toolbarhidden = true;
            }
            else
            {
                splitContainer3.Panel2Collapsed = Toolbarhidden;
                Toolbarhidden = false;
            }
        }
        bool PreviewTabhidden = true;
        private void HideShowPreviewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PreviewTabhidden == false)
            {
                timer1.Start();
                splitContainer2.Panel1Collapsed = PreviewTabhidden;
                PreviewTabhidden = true;
            }
            else
            {
                timer1.Stop();
                splitContainer2.Panel1Collapsed = PreviewTabhidden;
                PreviewTabhidden = false;
            }
        }

        private void CloseFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HideWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void HTMLCodeTextBox1_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            timer1.Start();
        }

        private void HTMLCodeTextBox1_TextChanging(object sender, TextChangingEventArgs e)
        {
            timer1.Stop();
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint wMsg, int wParam, uint lParam);

#pragma warning disable CS0414 // The field 'MainChildForm.EM_LINEINDEX' is assigned but its value is never used
        private static uint EM_LINEINDEX = 0xbb;
#pragma warning restore CS0414 // The field 'MainChildForm.EM_LINEINDEX' is assigned but its value is never used

        private void URLTextBox1_SizeChanged(object sender, EventArgs e)
        {
            SizeLabelFont(URLTextBox1);
        }
        // Copy this text into the Label using
        // the biggest font that will fit.
        private void SizeLabelFont(TextBox lbl)
        {
            // Only bother if there's text.
            string txt = lbl.Text;
            if (txt.Length > 0)
            {
                int best_size = 100;

                // See how much room we have, allowing a bit
                // for the Label's internal margin.
                int wid = lbl.DisplayRectangle.Width - 3;
                int hgt = lbl.DisplayRectangle.Height - 3;

                // Make a Graphics object to measure the text.
                using (Graphics gr = lbl.CreateGraphics())
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        using (Font test_font =
                            new Font(lbl.Font.FontFamily, i))
                        {
                            // See how much space the text would
                            // need, specifying a maximum width.
                            SizeF text_size =
                                gr.MeasureString(txt, test_font);
                            if ((text_size.Width > wid) ||
                                (text_size.Height > hgt))
                            {
                                best_size = i - 1;
                                break;
                            }
                        }
                    }
                }

                // Use that font size.
                try
                {
                    lbl.Font = new Font(lbl.Font.FontFamily, best_size);
                }
                catch (Exception)
                {

                }
            }
        }

        private void ToolStripButton3_Click_1(object sender, EventArgs e)
        {
            NormalVeiwWB.Reload();
        }

        private void ToolStripButton2_Click_1(object sender, EventArgs e)
        {
            NormalVeiwWB.Stop();
        }

        private void ToolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                NormalVeiwWB.ShowDevTools();
            }
            catch (Exception)
            {

            }
        }
    }
}

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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
namespace DuoEditor.Forms
{
    public partial class MainJSEditor : Form
    {
        Process DebugProcces;
        Process NodeProcess;
        Process CMDProcess;
        string RequsterPath = System.IO.Path.Combine(Application.StartupPath + "\\Requester.exe");
        string CMDPath = System.IO.Path.Combine(Application.StartupPath + "\\Requester.exe");
        private TabControl tabCtrl;
        private TabPage tabPag;
        bool CreatedFirstPanel = false;
        bool CreatedSecondPanel = false;
        string SafeFileName = string.Empty;
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
                CMDProcess.Kill();
                NodeProcess.Kill();
                DebugProcces.Kill();
                this.tabPag.Dispose();
            }
            catch (Exception)
            {
                if (!tabCtrl.HasChildren)
                {
                    tabCtrl.Visible = false;
                }
            }
            //If no Tabpage left
            if (!tabCtrl.HasChildren)
            {
                tabCtrl.Visible = false;
            }
        }
        public TabControl TabCtrl
        {
            set
            {
                tabCtrl = value;
            }
        }
        public MainJSEditor(string filename)
        {
            InitializeComponent();
            if (filename != null)
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    fastColoredTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                }
                SafeFileName = filename;
            }
        }
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private void MainJSEditor_Activated(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = true;
        }

        private void TabPage1_Paint(object sender, PaintEventArgs e)
        {
            if (CreatedFirstPanel == false)
            {
                CreatedFirstPanel = true;
                NodeProcess = Process.Start(new ProcessStartInfo()
                {
                    FileName =RequsterPath,
                    Arguments = ("node"),
                    WindowStyle = ProcessWindowStyle.Minimized
                });
                Thread.Sleep(500);
                SetParent(NodeProcess.MainWindowHandle, splitContainer6.Panel1.Handle);
                ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
            }
        }

        private void MainJSEditor_Load(object sender, EventArgs e)
        {
            fastColoredTextBox1.DescriptionFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Files/JSDesc.xml");
        }

        private void MainJSEditor_Deactivate(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
        }

        private void SplitContainer6_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWNORMAL);
            ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWNORMAL);
                ShowWindowAsync(DebugProcces.MainWindowHandle, SW_SHOWNORMAL);
            }
            catch (Exception) { }
            Thread.Sleep(100);
            try
            {
                ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
            ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
                ShowWindowAsync(DebugProcces.MainWindowHandle, SW_SHOWMAXIMIZED);
            }
            catch(Exception) { }
        }

        private void SplitContainer6_Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (CreatedSecondPanel == false)
            {
                CreatedSecondPanel = true;
                CMDProcess = Process.Start(new ProcessStartInfo()
                {
                    FileName = CMDPath,
                    Arguments = ("cmd"),
                    WindowStyle = ProcessWindowStyle.Minimized
                });
                Thread.Sleep(500);
                SetParent(CMDProcess.MainWindowHandle, splitContainer6.Panel2.Handle);
                ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
            }
        }



        private void StartNODEJSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "JavaScript File|*.js|All Files|*.*";
            savefile.InitialDirectory = "www\\";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(fastColoredTextBox1.Text);
                txtoutput.Close();
                SafeFileName = savefile.FileName;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SafeFileName == "")
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Title = "Save file as..";
                savefile.Filter = "JavaScript File|*.js|All Files|*.*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                    txtoutput.Write((fastColoredTextBox1.Text));
                    txtoutput.Close();
                    SafeFileName = savefile.FileName;
                }
                
            }
            else
            {
                StreamWriter txtoutput = new StreamWriter(SafeFileName);
                txtoutput.Write(fastColoredTextBox1.Text);
                txtoutput.Close();
            }
        }

        private void SaveAndRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SafeFileName == "")
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Title = "Save file as..";
                savefile.Filter = "JavaScript File|*.js|All Files|*.*";
                savefile.InitialDirectory = "www\\";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                    txtoutput.Write((fastColoredTextBox1.Text));
                    txtoutput.Close();
                    SafeFileName = savefile.FileName;
                }
               
            }
            else
            {
                StreamWriter txtoutput = new StreamWriter(SafeFileName);
                txtoutput.Write(fastColoredTextBox1.Text);
                txtoutput.Close();
            }
            DebugProcces = Process.Start(new ProcessStartInfo()
            {
                FileName = "Requester",
                Arguments = "node " + SafeFileName + " " + "1200",
                WindowStyle = ProcessWindowStyle.Minimized
            });
            Thread.Sleep(1000);
            SetParent(DebugProcces.MainWindowHandle, splitContainer6.Panel1.Handle);
            ShowWindowAsync(DebugProcces.MainWindowHandle, SW_SHOWMAXIMIZED);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Open File";
            openfile.Filter = "JavaScript File|*.js|All Files|*.*";
            openfile.InitialDirectory = "www\\";

            if (openfile.ShowDialog() == DialogResult.OK)
            {
                fastColoredTextBox1.Clear();
                using (StreamReader sr = new StreamReader(openfile.FileName))
                {
                    fastColoredTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                }
                SafeFileName = openfile.SafeFileName;
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeFileName = null;
            fastColoredTextBox1.Clear();
        }

        private void SdaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void foldToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fastColoredTextBox1.CollapseBlock(fastColoredTextBox1.Selection.Start.iLine,
              fastColoredTextBox1.Selection.End.iLine);

        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Redo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Paste();
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.ShowReplaceDialog();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Zoom = fastColoredTextBox1.Zoom + 10;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Zoom = fastColoredTextBox1.Zoom - 10;
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Print();
        }

        private void PrintPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void EnableNodeJSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enableNodeJSToolStripMenuItem.Checked == true)
            {
                splitContainer1.Panel2Collapsed = true;
                enableNodeJSToolStripMenuItem.Checked = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
                enableNodeJSToolStripMenuItem.Checked = true;
            
            }
        }

        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWNORMAL);
                ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWNORMAL);
                ShowWindowAsync(DebugProcces.MainWindowHandle, SW_SHOWNORMAL);
            }
            catch (Exception) { }
            Thread.Sleep(100);
            try
            {
                ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
                ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
                ShowWindowAsync(DebugProcces.MainWindowHandle, SW_SHOWMAXIMIZED);
            }
            catch (Exception) { }
        }
    }
}

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
        string RequsterPath = System.IO.Path.Combine(Application.StartupPath + "\\NodeRequester.exe");
        string CMDPath = System.IO.Path.Combine(Application.StartupPath + "\\CMDRequester.exe");
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
            CMDProcess.Kill();
            NodeProcess.Kill();
            DebugProcces.Kill();
            this.tabPag.Dispose();

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
        public MainJSEditor()
        {
            InitializeComponent();
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
            ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWNORMAL);
            ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWNORMAL);
            try
            {
                ShowWindowAsync(DebugProcces.MainWindowHandle, SW_SHOWNORMAL);
            }
            catch (Exception) { }
            Thread.Sleep(100);
            ShowWindowAsync(NodeProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
            ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
            try
            {
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
                    WindowStyle = ProcessWindowStyle.Minimized
                });
                Thread.Sleep(1000);
                SetParent(CMDProcess.MainWindowHandle, splitContainer6.Panel2.Handle);
                ShowWindowAsync(CMDProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
            }
        }



        private void StartNODEJSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "JavaScript File|*.js|All Files|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(fastColoredTextBox1.Text);
                txtoutput.Close();
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
            System.IO.StreamWriter _txtoutput = new System.IO.StreamWriter("tempbat.bat");
            _txtoutput.Write("@echo off" +
                "\n cd www" +
                "\n title Node Debug (Ported) " +
                "\n node " + SafeFileName +
                "\n echo End Of File Reached.." +
                "\n pause");
            _txtoutput.Close();
            DebugProcces = Process.Start(new ProcessStartInfo()
            {
                FileName = "tempbat.bat",
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
            openfile.Filter = "HTML|*.html|All Files|*.*";
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
    }
}

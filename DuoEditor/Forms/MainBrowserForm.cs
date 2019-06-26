using System;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
namespace DuoEditor
{
    public partial class MainBrowserForm : Form
    {
        private TabControl tabCtrl;
        private TabPage tabPag;
        public static string URL = null;
        public MainBrowserForm(string _URL)
        {
            try
            {
                URL = DuoDatabase.READ.ReadData("\\LastOpened\\", "LastOpenedUrl");
            }
            catch (Exception)
            {
                if (_URL != null)
                {
                    URL = _URL;
                }
            }
           
            InitializeComponent();
            InitializeChromium();
        }
        public TabControl TabCtrl
        {
            set
            {
                tabCtrl = value;
            }
        }
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
        private void MainBrowserForm_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(this.Icon, new Size(16, 16));
            if (URL != null)
            {
                chromeBrowser.Load(URL);
            }
            else
            {
                chromeBrowser.Load(PublicFuncs.CleanIp);
            }
        }

        ChromiumWebBrowser chromeBrowser;
        public void InitializeChromium()
        {


            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(PublicFuncs.CleanIp);
            // Add it to the form and fill it to the form window.
            this.panel1.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.AddressChanged += ChromeBrowser_AddressChanged;
            chromeBrowser.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
            chromeBrowser.StatusMessage += ChromeBrowser_StatusMessage;
            chromeBrowser.Load(PublicFuncs.CleanIp);
        }

        private void ChromeBrowser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            if (e.Browser.IsLoading)
            {
                toolStripStatusLabel1.Text = "Loading...";
                toolStripStatusLabel1.Image = Properties.Resources.Red_Icon;
            }
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.HttpStatusCode != 200 && e.HttpStatusCode != 0)
            {
                toolStripStatusLabel1.Text = "Error Code: " + e.HttpStatusCode;
                toolStripStatusLabel1.Image = Properties.Resources.Red_Icon;
            }
            else
            {
                toolStripStatusLabel1.Text = "Ready..";
                toolStripStatusLabel1.Image = Properties.Resources.Green_Icon;
            }
        }
        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
        private void ChromeBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {

            SetText(e.Address);
        }

        private void ActiveMdiChild_FormClosed(object sender,
                                   FormClosedEventArgs e)
        {
            this.tabPag.Dispose();
            //If no Tabpage left
            if (!tabCtrl.HasChildren)
            {
                tabCtrl.Visible = false;
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.Reload();
        }
        private void BackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.Back();
        }
        private void ForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.Forward();
        }
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.Stop();
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
       
            try
            {
                if (e.KeyCode == Keys.Enter)
                { 
                System.Media.SystemSounds.Question.Play(); e.SuppressKeyPress = true; chromeBrowser.Load(textBox1.Text);
                }
            }
            catch { }
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            chromeBrowser.ShowDevTools();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

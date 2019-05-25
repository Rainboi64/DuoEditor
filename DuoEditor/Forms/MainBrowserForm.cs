using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (URL != null)
            {
                chromeBrowser.Load(URL);
            }
            else
            {
                textBox1.Text = PublicFuncs.ip;
                chromeBrowser.Load(textBox1.Text);
            }
        }

        ChromiumWebBrowser chromeBrowser;
        public void InitializeChromium()
        {
              
     //   CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
           // Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(Settings.StartIP);
            // Add it to the form and fill it to the form window.
            this.splitContainer1.Panel2.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.IsBrowserInitializedChanged += ChromeBrowser_IsBrowserInitializedChanged;
            chromeBrowser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;
            chromeBrowser.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
          //  textBox1.Text = e.Url;
        }

        private void ChromeBrowser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
        
        }

        private void ChromeBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
        
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

        private void GoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                chromeBrowser.Load(textBox1.Text);
            }
            catch (Exception)
            {

            }
        }
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromeBrowser.Refresh();
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

        private void MainBrowserForm_Activated(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = true;
        }

        private void MainBrowserForm_Deactivate(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                webBrowser1.Navigate(URL);
            }
            else
            {
                textBox1.Text = PublicFuncs.ip;
                webBrowser1.Navigate(textBox1.Text);
            }
        }
        private void ActiveMdiChild_FormClosed(object sender,
                                   FormClosedEventArgs e)
        {
           
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
                webBrowser1.Navigate(textBox1.Text);
            }
            catch (Exception)
            {

            }
        }
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }
        private void BackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
        private void ForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
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

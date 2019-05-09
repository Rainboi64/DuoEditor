using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DuoLogger;
namespace DuoEditor.Forms.SubForms
{
    public partial class LogViewer2 : Form
    {
        public LogViewer2()
        {
            InitializeComponent();
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            Logger.DoSaveLogs();
            using (StreamReader sr = new StreamReader("Logs.DSLF"))
            {
                fastColoredTextBox1.Clear();
                fastColoredTextBox1.Text = fastColoredTextBox1.Text + DuoEditor.Encryption.Decrypt( sr.ReadToEnd());
                sr.Close();
                fastColoredTextBox1.GoEnd();
            }
        }

        private void LogViewer2_Load(object sender, EventArgs e)
        {
            SyncTimer.Start();
            fastColoredTextBox1.DescriptionFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Files/logDesc.xml");
        }

        private void ExportLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.CleanLogs();
        }

        private void StartLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncTimer.Start();
            fastColoredTextBox1.Enabled = false;
        }

        private void StopLoggingInspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncTimer.Stop();
            fastColoredTextBox1.Enabled = true;
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.DoSaveLogs();
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "DuoServer Log File|*.DSLF|All Files|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(DuoEditor.Encryption.Encrypt(fastColoredTextBox1.Text));
                txtoutput.Close();
            }
        }

        private void DecryptSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "Text File|*.txt|All Files|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(fastColoredTextBox1.Text);
                txtoutput.Close();
            }
        }
    }
}

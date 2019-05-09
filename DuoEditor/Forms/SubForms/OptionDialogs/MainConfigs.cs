using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuoDatabase;
using System.Diagnostics;
using System.IO;
namespace DuoEditor.Forms.SubForms.OptionDialogs
{
    public partial class MainConfigs : Form
    {
        public MainConfigs()
        {
            InitializeComponent();
        }
        private void ListDirectory(TreeView treeView, string Path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(Path);
            var _ = treeView.Nodes.Add(this.CreateDirectoryNode(rootDirectoryInfo));
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directorynode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var _directorynode = directorynode.Nodes.Add(CreateDirectoryNode(directory));
                string filename = directory.FullName;


            }
            foreach (var file in directoryInfo.GetFiles())
            {
                string filename = file.FullName;
                Icon _icon = System.Drawing.Icon.ExtractAssociatedIcon(filename);
                directorynode.Nodes.Add(new TreeNode(file.Name));

            }

            return directorynode;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MainConfigs_Load(object sender, EventArgs e)
        {
            tbDefaultStartDirectory.Text = Settings.StartDirectory;
            tbDefaultStartip.Text = Settings.StartIP;
            tbDefaultStartPort.Text = Settings.StartPort;
            ListDirectory(treeView1,"Logs");
                string eventLogName = "DuoEditor";

                EventLog eventLog = new EventLog();
                eventLog.Log = eventLogName;

                foreach (EventLogEntry log in eventLog.Entries)
                {
                listBox1.Items.Add(log.Message);
                }
            
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            tbDefaultStartDirectory.Text = "www";
            tbDefaultStartip.Text = "localhost";
            tbDefaultStartPort.Text = "8080";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
          FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
           var Result = openFileDialog.ShowDialog();
            if (Result == DialogResult.OK)
            {
                tbDefaultStartDirectory.Text = openFileDialog.SelectedPath;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DuoDatabase.WRITE.CreateData(tbDefaultStartip.Text,"//Configuration//", "StartIP");
            DuoDatabase.WRITE.CreateData(tbDefaultStartDirectory.Text, "//Configuration//", "StartDirectory");
            DuoDatabase.WRITE.CreateData(tbDefaultStartPort.Text, "//Configuration//", "StartPort");
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?","Exit?",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

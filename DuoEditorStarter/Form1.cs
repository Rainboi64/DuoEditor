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
using System.IO;
using System.Diagnostics;
namespace DuoEditorStarter
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MetroTabPage1_Click(object sender, EventArgs e)
        {

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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("Logs.DSLF"))
            {
                StreamWriter txtoutput = new StreamWriter("Logs.DSLF");
                txtoutput.Write("");
                txtoutput.Close();
            }
            webBrowser1.ScriptErrorsSuppressed = true;
            ListDirectory(treeView1, "Logs");
            try
            {
                string eventLogName = "DuoEditor";

                EventLog eventLog = new EventLog
                {
                    Log = eventLogName
                };
                foreach (EventLogEntry log in eventLog.Entries)
                {
                    metroListView2.Items.Add(log.Message);
                }
            }
            catch (Exception)
            {
            }

           
            metroTabControl1.ImageList = imageList1;
            metroListView1.LargeImageList = imageList1;
            if (System.IO.Directory.Exists("Database//projects") )
            {
          
                metroListView1.Items.Clear();
                foreach (string directory in System.IO.Directory.GetDirectories("Database\\projects"))
                {


                    metroListView1.Items.Add(System.IO.Path.GetFileName(directory), 0);

                }
            }
        }

        private void MetroTabPage2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void MetroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thread.Sleep(10);
            this.Show();
            this.Activate();
        }

        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void MetroLabel8_Click(object sender, EventArgs e)
        {

        }

        private void MetroToggle8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MetroToggle9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MetroLabel16_Click(object sender, EventArgs e)
        {

        }

        private void MetroLabel17_Click(object sender, EventArgs e)
        {

        }

        private void MetroTile5_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage3;
        }

        private void MetroTile1_Click(object sender, EventArgs e)
        {
            NewProjects newProjects = new NewProjects();
            newProjects.ShowDialog();
            if (newProjects.DialogResult == DialogResult.OK)
            {
                this.Hide();
                Process.Start("DuoEditor.exe");
            }
        }

        private void MetroTile2_Click(object sender, EventArgs e)
        {
            OpenProject newProjects = new OpenProject();
            newProjects.ShowDialog();
            if (newProjects.DialogResult == DialogResult.OK)
            {
                this.Hide();
                Process.Start("DuoEditor.exe");
            }
        }

        private void MetroListView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string filename = metroListView1.SelectedItems[0].Text;
             string path =   DuoDatabase.READ.ReadData("\\Projects\\" + filename +"\\","\\Path");
             string port =    DuoDatabase.READ.ReadData("\\Projects\\" + filename + "\\", "\\Port");
                DuoDatabase.WRITE.CreateData(path, "\\Configuration\\", "StartDirectory");
                DuoDatabase.WRITE.CreateData(port, "\\Configuration\\", "StartPort");
                Process.Start("DuoEditor.exe");
                this.Close();
            } catch { }
        }
    }
}

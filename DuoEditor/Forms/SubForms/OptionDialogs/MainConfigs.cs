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

        }
    }
}

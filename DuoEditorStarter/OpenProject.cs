using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace DuoEditorStarter
{
    public partial class OpenProject : MetroFramework.Forms.MetroForm
    {
        public OpenProject()
        {
            InitializeComponent();
        }

        private void OpenProject_Load(object sender, EventArgs e)
        {
            metroTextBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            var result = browserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                metroTextBox4.Text = browserDialog.SelectedPath;
            }
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            if (metroTextBox2.Text == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
            {
                if (!System.IO.Directory.Exists(metroTextBox2.Text + "\\DuoEditor\\Projects\\"))
                {
                    System.IO.Directory.CreateDirectory(metroTextBox2.Text + "\\DuoEditor\\");
                    System.IO.Directory.CreateDirectory(metroTextBox2.Text + "\\DuoEditor\\Projects\\");
                }
                System.IO.Directory.CreateDirectory(metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text);
                foreach (string filename in System.IO.Directory.GetDirectories(metroTextBox4.Text))
                {
                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(filename, metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text, UIOption.AllDialogs);
                    }
                    catch { }
                    }
                foreach (string filename in System.IO.Directory.GetFiles(metroTextBox4.Text))
                {
                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(filename, metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text, UIOption.AllDialogs);
                    }
                    catch { }
                }
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory() + ("\\Database\\Projects\\" + metroTextBox3.Text)));
                DuoDatabase.WRITE.CreateData(metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text, "\\Projects\\" + metroTextBox3.Text, "\\Path");
                DuoDatabase.WRITE.CreateData(metroTextBox1.Text, "\\Projects\\" + metroTextBox3.Text, "\\Port");
                DuoDatabase.WRITE.CreateData(DuoDatabase.READ.ReadData("\\Projects\\" + metroTextBox3.Text + "\\", "\\Path"), "\\Configuration\\", "StartDirectory");
                DuoDatabase.WRITE.CreateData(DuoDatabase.READ.ReadData("\\Projects\\" + metroTextBox3.Text + "\\", "\\Port"), "\\Configuration\\", "StartPort");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                foreach (string filename in System.IO.Directory.GetFiles(metroTextBox4.Text))
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(filename, metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text, UIOption.AllDialogs);
                }
                DuoDatabase.WRITE.CreateData(metroTextBox2.Text +"\\"+metroTextBox3.Text, "\\Projects\\" + metroTextBox3.Text, "\\Path");
                DuoDatabase.WRITE.CreateData(metroTextBox1.Text, "\\Projects\\" + metroTextBox3.Text, "\\Port");
                DuoDatabase.WRITE.CreateData(DuoDatabase.READ.ReadData("\\Projects\\" + metroTextBox3.Text + "\\", "\\Path"), "\\Configuration\\", "StartDirectory");
                DuoDatabase.WRITE.CreateData(DuoDatabase.READ.ReadData("\\Projects\\" + metroTextBox3.Text + "\\", "\\Port"), "\\Configuration\\", "StartPort");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            var result = browserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                metroTextBox2.Text = browserDialog.SelectedPath;
            }
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

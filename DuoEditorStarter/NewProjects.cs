using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuoEditorStarter
{
    public partial class NewProjects : MetroFramework.Forms.MetroForm
    {
        public NewProjects()
        {
            InitializeComponent();
        }

        private void NewProjects_Load(object sender, EventArgs e)
        {
           metroTextBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(metroTextBox2.Text + "\\DuoEditor\\Projects\\"))
            {
                System.IO.Directory.CreateDirectory(metroTextBox2.Text + "\\DuoEditor\\");
                System.IO.Directory.CreateDirectory(metroTextBox2.Text + "\\DuoEditor\\Projects\\");
            }
            System.IO.Directory.CreateDirectory(metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text);
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory()+("\\Database\\Projects\\"+ metroTextBox3.Text)));
            DuoDatabase.WRITE.CreateData(metroTextBox2.Text + "\\DuoEditor\\Projects\\" + metroTextBox3.Text, "\\Projects\\" +metroTextBox3.Text,"\\Path");
            DuoDatabase.WRITE.CreateData(metroTextBox1.Text, "\\Projects\\" + metroTextBox3.Text, "\\Port");
            DuoDatabase.WRITE.CreateData(DuoDatabase.READ.ReadData("\\Projects\\"+ metroTextBox3.Text + "\\", "\\Path"),"\\Configuration\\","StartDirectory");
            DuoDatabase.WRITE.CreateData(DuoDatabase.READ.ReadData("\\Projects\\" + metroTextBox3.Text + "\\", "\\Port"), "\\Configuration\\", "StartPort");
            this.DialogResult = DialogResult.OK;
            this.Close();
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
    }
}

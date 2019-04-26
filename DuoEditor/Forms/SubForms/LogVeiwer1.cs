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
namespace DuoEditor.Forms.SubForms
{
    public partial class LogVeiwer : Form
    {
        public LogVeiwer()
        {
            InitializeComponent();
        }

        private void SplitContainer5_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

      
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Open File";
            openfile.Filter = "DuoServer Log File|*.DSLF|All Files|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                HTMLCodeTextBox1.Clear();
                using (StreamReader sr = new StreamReader(openfile.FileName))
                {
                    HTMLCodeTextBox1.Text = DuoEditor.Encryption.Decrypt(sr.ReadToEnd());
                    sr.Close();
                }
                

            
        }
    }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "DuoServer Log File|*.DSLF|All Files|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(DuoEditor.Encryption.Encrypt(HTMLCodeTextBox1.Text));
                txtoutput.Close();
            }
        }

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LogVeiwer_Load(object sender, EventArgs e)
        {

            HTMLCodeTextBox1.DescriptionFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Files/logDesc.xml");
        }

        private void DecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "Text File|*.txt|All Files|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(HTMLCodeTextBox1.Text);
                txtoutput.Close();
            }
        }
    }
}

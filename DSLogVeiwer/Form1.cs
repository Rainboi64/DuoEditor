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
using System.Runtime.InteropServices;
using Microsoft.Win32;
namespace DuoEditor
{
    public partial class DSLogVeiwer : Form
    {
    public static string _input;
        public DSLogVeiwer(string input)
        {
            InitializeComponent()
                ;
            _input = input;
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
                    HTMLCodeTextBox1.Text = global::DuoEditor.Encryption.Decrypt(sr.ReadToEnd());
                    sr.Close();
                }


            }
        }

        private void ExctractToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void DSLogVeiwer_Load(object sender, EventArgs e)
        {
            if (_input != "")
            {
                using (StreamReader sr = new StreamReader(_input))
                {
                    HTMLCodeTextBox1.Text = global::DuoEditor.Encryption.Decrypt(sr.ReadToEnd());
                    sr.Close();
                }
            }
            HTMLCodeTextBox1.DescriptionFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\logDesc.xml");
            
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as..";
            savefile.Filter = "Text File|*.txt|All Files|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(global::DuoEditor.Encryption.Encrypt(HTMLCodeTextBox1.Text));
                txtoutput.Close();
            }
        }
    }
    }

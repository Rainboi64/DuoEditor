using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuoEditor.Forms.SubForms
{
    public partial class ImageInserter : Form
    {
        string FilenameSafe;
        public string ReturnImage { get; set; }
        public ImageInserter()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnImage = "<img src=\"" + textBox1.Text + "\" alt = \""+ richTextBox1.Text + "\"" + "width=\""+numericUpDown1.Value+"\"" + "height=\""+numericUpDown2.Value +  "\"> ";
            this.DialogResult = DialogResult.OK;
        }
        bool Checked = true;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
   
                
            }
            
        

        private void ImageInserter_Load(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

       Logger.Log("\n" + " Form Report: Image Inserter was launched" + "\n");

            Console.ResetColor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                string Rawfilename = ofd.FileName;
                string RawFilenameSafe = ofd.SafeFileName;
                string DestinationFilename = "\\www\\uploaded_images\\";
                if (System.IO.File.Exists(Rawfilename) & System.IO.Directory.Exists(DestinationFilename))
                {
                    FilenameSafe = (System.IO.Path.Combine(DestinationFilename + RawFilenameSafe));
                    System.IO.File.Copy(Rawfilename, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + FilenameSafe), true);
                    textBox1.Text = "\\uploaded_images\\" + RawFilenameSafe;
                }
                else
                { Logger.Log("Couldnt Copy Missing file"); Console.Beep(12000, 50); Console.Beep(12000, 50); Console.Beep(12000, 50); Console.Beep(12000, 50); }
            }
            
            catch(Exception i) { Logger.LogEx(i); }
            }

        private void radioButton1_Click(object sender, EventArgs e)
        {
         
        }

        private void button4_SizeChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Checked == true)
            {
                Checked = false;
                textBox1.Enabled = false;
                button1.Enabled = true;
            }
            else
            {

                Checked = true;
                textBox1.Enabled = true;
                button1.Enabled = false;
            }

        }
    }
}

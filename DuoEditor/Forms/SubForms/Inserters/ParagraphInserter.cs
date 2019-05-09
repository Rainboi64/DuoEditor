using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuoLogger;
namespace DuoEditor.Forms.SubForms
{
    public partial class ParagraphInserter : Form
    {
        public string ReturnParagraph { get; set; }
        public ParagraphInserter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnParagraph = "<p>" + richTextBox1.Text + "</p>";
            this.DialogResult = DialogResult.OK;
        }

        private void ParagraphInserter_Load(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

       Logger.Log("\n" + " Form Report: Paragraph Inserter was launched" + "\n");

            Console.ResetColor();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

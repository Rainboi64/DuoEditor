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
    public partial class HyperLinkInserter : Form
    {
        public string ReturnHyperLink { get; set; }
        public HyperLinkInserter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnHyperLink = "<a href=\"" + textBox1.Text + "\">" + richTextBox1.Text + "</a>";
            this.DialogResult = DialogResult.OK;
        }

        private void HyperLinkInserter_Load(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

       Logger.Log("\n" + " Form Report: HyperLink Inserter was launched" + "\n");

            Console.ResetColor();
        }
    }
}

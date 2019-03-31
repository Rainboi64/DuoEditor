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
    public partial class HeaderInserter : Form
    {
        public string ReturnHeader { get; set; }
        public HeaderInserter()
        {

            InitializeComponent();

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReturnHeader = "<h" + numericUpDown1.Value + ">" + richTextBox1.Text + "</h" + numericUpDown1.Value + ">" + "\n";
            this.DialogResult = DialogResult.OK;
            this.Close();
        
}

        private void HInserter_Load(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("\n" + " Form Report: Header Inserter was launched" + "\n");

            Console.ResetColor();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuoEditor.Forms.SubForms.LogViewers
{
    public partial class Logs_Debugger : Form
    {
        public Logs_Debugger()
        {
            InitializeComponent();
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Syncer_Tick(object sender, EventArgs e)
        {

            try
            {
                charsinmem.Text = DuoLogger.Logger.GetLogs.Length.ToString();
            }
            catch (Exception)
            {

                
            }
            try
            {
                sizoflogsmem.Text = (DuoLogger.Logger.GetLogs.Length * 8).ToString();
            }
            catch (Exception)
            {

               
            }
            try
            {

                fastColoredTextBox1.Text = DuoLogger.Logger.GetLogs;

            }
            catch (Exception)
            {

             
            }
            try
            {
                fastColoredTextBox2.Text = DuoLogger.Logger.ViewLogfile();

            }
            catch (Exception)
            {

           
            }
        }

        private void Logs_Debugger_Load(object sender, EventArgs e)
        {
            Syncer.Start();
        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DuoLogger.Logger.ClearLogs();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DuoLogger.Logger.ClearLogs();
        }
    }
}

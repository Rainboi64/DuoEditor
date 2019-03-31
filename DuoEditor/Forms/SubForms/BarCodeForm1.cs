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
    public partial class BarCodeForm1 : Form
    {
        public BarCodeForm1()
        {
            InitializeComponent();
        }

        private void BarCodeForm1_Load(object sender, EventArgs e)
        {
                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pictureBox1.Image = qrcode.Draw(DuoEditor.PublicVars.ip, 500);
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PublicVars.ip);
        }
    }
}

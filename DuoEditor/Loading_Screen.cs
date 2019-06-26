using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuoEditor
{
    public partial class Loading_Screen : Form
    {
        public Loading_Screen()
        {
            InitializeComponent();
           // this.TransparencyKey = this.BackColor;
        }

        private void Loading_Screen_Load(object sender, EventArgs e)
        {
            // Create points that define polygon.
            //Get the middle of the panel
            var x_0 = Width / 2;
            var y_0 = Height / 2 ;

            var shape = new PointF[6];

            var r = 200; //70 px radius 

            //Create 6 points
            for (int a = 0; a < 6; a++)
            {
                shape[a] = new PointF(
                    x_0 + r * (float)Math.Cos(a * 60 * Math.PI / 180f),
                    y_0 + r * (float)Math.Sin(a * 60 * Math.PI / 180f));
            }

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(shape);
            Region region = new Region(path);
            this.Region = region;
        }
    }
}

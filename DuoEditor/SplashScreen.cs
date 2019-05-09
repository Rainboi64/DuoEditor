using System;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace DuoEditor
{
    public partial class SplashScreen : MetroFramework.Forms.MetroForm
    {
        public SplashScreen()
        {
            InitializeComponent();
           
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            metroLabel1.Text = ("Version : " + PublicFuncs.APPVERSION);
            Thread formstarter = new Thread(formstarterworker);
            formstarter.SetApartmentState(ApartmentState.STA);
            formstarter.Start();

        }
        public void formstarterworker()
        {
   
            if (!File.Exists("Logs.DSLF"))
            {
                StreamWriter txtoutput = new StreamWriter("Logs.DSLF");
                txtoutput.Write("");
                txtoutput.Close();
            }
            if (!File.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\"));
            }
            if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month)))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\"));
            }
            if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.DayOfWeek + "\\")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\"));
            }
            Thread.Sleep(5000);
            DuoLogger.Logger.ProccesLogs();
            this.Invoke(new MethodInvoker(delegate {
                this.Hide();
            }));
            Application.Run(new MainForm());
        }

        private void SplashScreen_BackgroundImageChanged(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {

        }
   
        private void Timer1_Tick(object sender, EventArgs e)
        {
          
     
        }
    }
}

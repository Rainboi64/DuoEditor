using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
namespace DuoEditor
{

    class MainServer
    {
        static void OnProcessExit(object sender, EventArgs e)
        {
            Logger.CleanLogs();
        }

        public static void SplashWork()
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
        
            Logger.ProccesLogs();
            Thread.Sleep(3000);
        }
        public static void Splash()
        {

            SplashScreen.SplashForm frm = new SplashScreen.SplashForm();
            frm.AppName = "";
            frm.BackgroundImage = Properties.Resources.SplashLogo;
            frm.BackgroundImageLayout = ImageLayout.Center;
            frm.Icon = Properties.Resources.SplashIcon;
            frm.ShowInTaskbar = false;
            Thread.Sleep(20);
            Application.Run(frm);


        }
        public static void MainFormStarter()
        {
            Application.Run(new MainForm());
        }
        public static void FormStarter()
        {
            Console.ResetColor();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            ThreadStart FormStart_ = new ThreadStart(Splash);
            Thread FormStarterThread = new Thread(FormStart_);
            FormStarterThread.SetApartmentState(ApartmentState.STA);
            ThreadStart FormStart1_ = new ThreadStart(MainFormStarter);
            Thread FormStarterThread1 = new Thread(FormStart1_);
            FormStarterThread1.SetApartmentState(ApartmentState.STA);
            FormStarterThread.Start();
            SplashWork();
            FormStarterThread.Abort();
            FormStarterThread1.Start();
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            if (!File.Exists("//Database//Configuration//StartDirectory.ddb"))
            {
                DuoDatabase.WRITE.CreateData("www", "Configuration//", "StartDirectory");
            }
            if (!File.Exists("//Database//Configuration//StartPort.ddb"))
            {
                DuoDatabase.WRITE.CreateData("8080", "Configuration//", "StartPort");
            }
            if (!File.Exists("//Database//Configuration//StartIP.ddb"))
            {
                DuoDatabase.WRITE.CreateData("localhost", "Configuration//", "StartIP");
            }
            PublicFuncs.CleanIp =Settings.StartIP+":" + Settings.StartPort;
            PublicFuncs.ip = Settings.StartIP;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            FormStarter();

        }

    }
}


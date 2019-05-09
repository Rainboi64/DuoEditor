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
using DuoLogger;
namespace DuoEditor
{
    class MainStart
    {
        static void OnProcessExit(object sender, EventArgs e)
        {
           
            Logger.CleanLogs();
        }
        public static void FormStarter()
        {
           SplashScreen frm = new SplashScreen();
            Application.Run(frm);
        }

        static void Main(string[] args)
        {
            DuoLogger.Logger.ProccesLogs();
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory() + "//Database//Configuration//StartDirectory.ddb")))
            {
                DuoDatabase.WRITE.CreateData("www", "Configuration//", "StartDirectory");
            }
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory() + "//Database//Configuration//StartPort.ddb")))
            {
                DuoDatabase.WRITE.CreateData("8080", "Configuration//", "StartPort");
            }
            if (!File.Exists(Path.Combine( Directory.GetCurrentDirectory() + "//Database//Configuration//StartIP.ddb")))
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


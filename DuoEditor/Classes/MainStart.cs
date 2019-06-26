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
        [STAThread]
        static void OnProcessExit(object sender, EventArgs e)
        {
           
            Logger.CleanLogs();
        }
        public static void FormStarter()
        {
           MainForm frm = new MainForm();
            Application.Run(frm);
        }

        static void Main(string[] args)
        {
          
            Thread main = new Thread(() => FormStarter());
            main.SetApartmentState(ApartmentState.STA);
            main.Start();
        }

    }
}


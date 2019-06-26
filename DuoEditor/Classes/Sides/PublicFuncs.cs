using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace DuoEditor
{

    public class PublicFuncs
    {
        #region Defines
        public static string APPVERSION = "0.2";
        public static string ip { get; set; }
        public static string CleanIp = Settings.StartIP +':'+ Settings.StartPort;
        public static string port { get; set; }
        public static void Host(string Dir, int Port) => HostWorker(Dir,Port);

        public static bool shwn = false;
        #endregion

        #region Workers

        private static void HostWorker(string Dir_,int Port_)
        {
            DuoServerLib.Serve.HTTPServer hTTPServer = new DuoServerLib.Serve.HTTPServer(Dir_, Port_);
        }
        
      
        #endregion
    }


}

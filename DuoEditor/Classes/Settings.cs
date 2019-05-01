using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuoDatabase;
namespace DuoEditor
{
    class Settings
    {
        

        public static readonly string EnableEventViewerLogs;
        public static readonly string EnableServerLogs;
        public static readonly string LogsEncryptionKey;
        public static readonly string EnableFormLogs;
        public static readonly string HideServer;
        #region StartIP
        public static string StartIP => StartIPProvider();
        private static string GottenStartIp = null;
        private static string StartIPProvider()
        {
            if (GottenStartIp == null)
            {
                return StartIPGetter();
            }
            return GottenStartIp;
        }
        private static string StartIPGetter()
        {
          return READ.ReadData("//Configuration//","StartIP");
        }
        #endregion
        #region StartPort
        public static string StartPort => StartPortProvider();
        private static string GottenStartPort = null;
        private static string StartPortProvider()
        {
            if (GottenStartPort == null)
            {
                return StartPortGetter();
            }
            return GottenStartPort;
        }
        private static string StartPortGetter()
        {
            return READ.ReadData("//Configuration//", "StartPort");
        }
        #endregion
        #region StartDirectory
        public static string StartDirectory => StartDirectoryProvider();
        private static string GottenStartDirectory = null;
        private static string StartDirectoryProvider()
        {
            if (GottenStartDirectory == null)
            {
                return StartDirectoryGetter();
            }
            return GottenStartPort;
        }
        private static string StartDirectoryGetter()
        {
            return READ.ReadData("//Configuration//", "StartDirectory");
        }
        #endregion
        #region EnableLogs
        public static string EnableLogs => EnableLogsProvider();
        private static string GottenEnableLogs = null;
        private static string EnableLogsProvider()
        {
            if (GottenEnableLogs == null)
            {
                return EnableLogsGetter();
            }
            return GottenEnableLogs;
        }
        private static string EnableLogsGetter()
        {
            return READ.ReadData("//Configurations//", "EnableLogs");
        }
        #endregion

    }
}

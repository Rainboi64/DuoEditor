using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace DuoEditor
{
    class Logger
    {
        public static void Log(string logdetail) => LogWorker(logdetail);
        public static void ProccesLogs() => ProccesLogsWorker();
        public static string GetLogs = LogBunk;
        public static void ClearLogs() => ClearLogsWorker();
        public static void CleanLogs() => CleanLogsWorker();
        public static string ViewLogfile() => ViewLogfileWorker();
        public static void LogEx(Exception LogDetail) => LogExWorker(LogDetail);
        public static void DoSaveLogs() => DoSaveLogsWorker();

        private static string LogBunk;
        private static void LogExWorker(Exception LogDetail_)
        {
            try
            {
                string LogDetail = Convert.ToString(LogDetail_);
                Log(LogDetail);
                using (EventLog eventLog = new EventLog("DuoEditor"))
                {
                    eventLog.Source = "DuoEditor";
                    eventLog.WriteEntry(LogDetail, EventLogEntryType.Error, 101, 1);
                }
            }
            catch (Exception i)
            {
                LogEx(i);
                throw;
            }
        }
        private static string ViewLogfileWorker()
        {
            string logfilecontents = string.Empty;
            using (StreamReader sr = new StreamReader("Logs.DSLF"))
            {
                logfilecontents = DuoEditor.Encryption.Decrypt( sr.ReadToEnd());
                sr.Close();
            }
            return logfilecontents;
        }
        private static void LogWorker(string LogDetail)
        {
            Console.WriteLine(LogDetail);
            
            try
            {
                LogBunk = LogBunk + LogDetail + Environment.NewLine;
            }
            catch (Exception) { throw; }
        }
        private static void ClearLogsWorker()
        {
            try
            {
                LogBunk = null;
            }
            catch (Exception i)
            {
                LogEx(i);
                throw;
            }
        }
        static String cLog = String.Empty;
      private static void CleanLogsWorker()
        {

            try
            {
                string FileLocation = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + DateTime.Now.Ticks + ".DSLF");
                Log("Did a Log export at " + DateTime.Now + " Called " + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + DateTime.Now.Ticks + ".DSLF");
                StreamWriter txt = new StreamWriter(FileLocation);
                txt.Write(DuoEditor.Encryption.Encrypt(cLog));
                txt.Close();
                StreamWriter txt1 = new StreamWriter("Logs.DSLF");
                txt1.Write("");
                txt1.Close();
            }
            catch (Exception i)
            {

                LogEx(i);
                throw;
            }
        }
        private static void DoSaveLogsWorker()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Logs.DSLF"))
                {
               cLog = DuoEditor.Encryption.Decrypt( sr.ReadToEnd());
                    sr.Close();
                }
                StreamWriter txtoutput = new StreamWriter("Logs.DSLF");
                txtoutput.Write(DuoEditor.Encryption.Encrypt(cLog + LogBunk));
                txtoutput.Close();
                ClearLogs();
            }
            catch (Exception i)
            {
                LogEx(i);
            }
        }
        private static void ProccesLogsWorker()
        {
            try
            {

                if (cLog.Length >= 1000)
                {
                    CleanLogs();
                }
                Thread childThread = new Thread(childref);
                childThread.Start();

                void childref()
                {
                    while (true)
                    {
                        Thread.Sleep(30000);

                        DoSaveLogs();
                    }
                }

            }
            catch (Exception i)
            {
                LogEx(i);
                throw;
            }
        }

    }
}

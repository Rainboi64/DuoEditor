using System;
using System.Linq;
using BetterConsoleTables;
using System.Net;
using SimpleTCP;
using System.IO;
using DuoDatabase;
using System.Threading;

namespace DuoHost
{
    class Program
    {
        public static string IP;
        public static DateTime dateTime;
        public static string Project_Name()
        {
            return READ.ReadData("\\Configurations\\", "\\Project_Name");
        }
        public static string Auth_Code()
        {
            return READ.ReadData("\\Configurations\\", "\\Auth_Code");
        }
        public static string Port()
        {
            return READ.ReadData("\\Configurations\\", "\\Port");
        }
        public static string Website_Directory()
        {
            return READ.ReadData("\\Configurations\\", "\\Website_Directory");
        }
     public static SimpleTcpServer server = new SimpleTcpServer();


        static void Main()
        {
            #region Setup
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Database\\"))
            {
                const string asciiartitle = @"
 
                    ___           ___           ___           ___           ___                   
     _____         /\  \         /\  \         /\  \         /\  \         /\__\                  
    /::\  \        \:\  \       /::\  \        \:\  \       /::\  \       /:/ _/_         ___     
   /:/\:\  \        \:\  \     /:/\:\  \        \:\  \     /:/\:\  \     /:/ /\  \       /\__\    
  /:/  \:\__\   ___  \:\  \   /:/  \:\  \   ___ /::\  \   /:/  \:\  \   /:/ /::\  \     /:/  /    
 /:/__/ \:|__| /\  \  \:\__\ /:/__/ \:\__\ /\  /:/\:\__\ /:/__/ \:\__\ /:/_/:/\:\__\   /:/__/     
 \:\  \ /:/  / \:\  \ /:/  / \:\  \ /:/  / \:\/:/  \/__/ \:\  \ /:/  / \:\/:/ /:/  /  /::\  \     
  \:\  /:/  /   \:\  /:/  /   \:\  /:/  /   \::/__/       \:\  /:/  /   \::/ /:/  /  /:/\:\  \    
   \:\/:/  /     \:\/:/  /     \:\/:/  /     \:\  \        \:\/:/  /     \/_/:/  /   \/__\:\  \   
    \::/  /       \::/  /       \::/  /       \:\__\        \::/  /        /:/  /         \:\__\  
     \/__/         \/__/         \/__/         \/__/         \/__/         \/__/           \/__/









";
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(asciiartitle);
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Start The Setup.....");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + "\\Database"));
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\Stats"));
                Console.WriteLine("Created: " + Path.Combine(Directory.GetCurrentDirectory() + "\\Database"));
                Console.WriteLine("Created: " + Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\Stats"));
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\"+"\\Configurations"));
                Console.WriteLine("Created: " + Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\Configurations"));
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose a Project Name: ");
                string input_project_name = Console.ReadLine();
                WRITE.CreateData(input_project_name, "\\Configurations\\", "\\Project_Name");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Project_Name.ddb");
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose an Authentication Code: ");
                string input_auth_code = Console.ReadLine();
                WRITE.CreateData(input_auth_code, "\\Configurations\\", "\\Auth_Code");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Auth_Code.ddb");
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose an Suitable Port: ");
                string input_port = Console.ReadLine();
                WRITE.CreateData(input_port, "\\Configurations\\", "\\Port");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Port.ddb");
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose an Suitable Website Directory: ");
                string input_Website_Directory = Console.ReadLine();
                WRITE.CreateData(input_Website_Directory, "\\Configurations\\", "\\Website_Directory");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Website_Directory.ddb");
                Console.ResetColor();
                Console.Clear();
              }
            #endregion
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
           dateTime = DateTime.Now;
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                IP = addr.ToString() ;
            }
            server.Delimiter = 0x13;
            server.StringEncoder = System.Text.ASCIIEncoding.ASCII;
            server.DataReceived += Server_DataReceived;
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.DelimiterDataReceived += Server_DelimiterDataReceived;
            DuoLogger.Logger.ProccesLogs();
            server.Start(Convert.ToInt32(Port()));
            DuoDatabase.WRITE.CreateData(Convert.ToString(server.ConnectedClientsCount), "\\Stats\\", "\\Connected_Count");
            DuoDatabase.WRITE.CreateData("No Users Connected", "\\Stats\\", "\\ConnectedIPS");
            DuoDatabase.WRITE.CreateData("Online", "\\Stats\\","\\ServerStatus");
            DuoLogger.Logger.Log("Server Started Ip: "+IP+":"+Port()+" At "+dateTime);
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            DuoDatabase.WRITE.CreateData("Offline", "\\Stats\\", "\\ServerStatus");
        }
        #region Client Events
        private static void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            try
            {
           
               string nIPBunk = IPBunk.Replace(IPAddress.Parse(((IPEndPoint)e.Client.LocalEndPoint).Address.ToString()) + Environment.NewLine, string.Empty);
                IPBunk = nIPBunk;
                DuoDatabase.WRITE.CreateData(IPBunk, "\\Stats\\", "\\ConnectedIPS");
                DuoDatabase.WRITE.CreateData(Convert.ToString(server.ConnectedClientsCount), "\\Stats\\", "\\Connected_Count");
                DuoLogger.Logger.Log("Client Disconnected With IP : " + IPAddress.Parse(((IPEndPoint)e.Client.LocalEndPoint).Address.ToString()) + ":" + ((IPEndPoint)e.Client.LocalEndPoint).Port.ToString() + " At " + DateTime.Now);
            }
            catch (Exception)
            {
            }
        }
        private static string IPBunk = string.Empty;
        private static void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            try
            {
               IPBunk = IPBunk + IPAddress.Parse(((IPEndPoint)e.Client.LocalEndPoint).Address.ToString()) + Environment.NewLine;
                DuoDatabase.WRITE.CreateData(IPBunk, "\\Stats\\", "\\ConnectedIPS");
                DuoDatabase.WRITE.CreateData(Convert.ToString(server.ConnectedClientsCount), "\\Stats\\", "\\Connected_Count");
                DuoLogger.Logger.Log("Client Connected With IP : " + IPAddress.Parse(((IPEndPoint)e.Client.LocalEndPoint).Address.ToString()) + ":" + ((IPEndPoint)e.Client.LocalEndPoint).Port.ToString() + " At " + DateTime.Now);

            }
            catch (Exception)
            {

            }
        }
        #endregion
        private static void Server_DelimiterDataReceived(object sender, Message e)
        {
           
            Console.WriteLine("Requsted: " + e.MessageString);

            if (e.MessageString == (Auth_Code()))
            {
               e.ReplyLine(Project_Name());
            }
        }

        private static void Server_DataReceived(object sender, Message e)
        {
     

        }
    }
}

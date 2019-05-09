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
        public static Table Statstable = new Table("Start Time", "Uptime", "Port", "Ip","Connected Users Count","Auth Key");
        public static Table Userstable = new Table("Log :","Ip :","Port :", "Log Time :");
        public static string Project_Name()
        {
            return READ.ReadData("Configurations", "Project_Name");
        }
        public static string Auth_Code()
        {
            return READ.ReadData("Configurations", "Auth_Code");
        }
        public static string Port()
        {
            return READ.ReadData("Configurations", "Port");
        }
        public static string Website_Directory()
        {
            return READ.ReadData("Configurations", "Website_Directory");
        }
     public static SimpleTcpServer server = new SimpleTcpServer();

        public static void Stats_Syncer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Server Stats :");
                Statstable.Rows.Clear();
                Statstable.AddRow(dateTime, DateTime.UtcNow - dateTime, Port(), IP, server.ConnectedClientsCount, Auth_Code());
                Console.WriteLine(Statstable.ToString());
                Console.WriteLine("Server Log :"); Console.WriteLine(Userstable.ToString()); 
                Thread.Sleep(2000);
            }
        }
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
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\"));
                Console.WriteLine("Created: " + Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\"));
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\"+"\\Configurations\\"));
                Console.WriteLine("Created: " + Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\Configurations\\"));
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose a Project Name: ");
                string input_project_name = Console.ReadLine();
                WRITE.CreateData(input_project_name, "Configurations", "Project_Name");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Project_Name.ddb");
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose an Authentication Code: ");
                string input_auth_code = Console.ReadLine();
                WRITE.CreateData(input_auth_code, "Configurations", "Auth_Code");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Auth_Code.ddb");
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose an Suitable Port: ");
                string input_port = Console.ReadLine();
                WRITE.CreateData(input_port, "Configurations", "Port");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Port.ddb");
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Choose an Suitable Website Directory: ");
                string input_Website_Directory = Console.ReadLine();
                WRITE.CreateData(input_Website_Directory, "Configurations", "Website_Directory");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Created Database Record: Database\\Configurations\\Website_Directory.ddb");
                Console.ResetColor();
                Console.Clear();
              }
            #endregion
            dateTime = DateTime.Now;
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                IP = addr.ToString() ;
            }
            Thread SyncStatsThread = new Thread(new ThreadStart(Stats_Syncer));
            Statstable.Config = TableConfiguration.Unicode();
            Userstable.Config = TableConfiguration.Unicode();
            server.Delimiter = 0x01;
            server.StringEncoder = System.Text.ASCIIEncoding.ASCII;
            server.DataReceived += Server_DataReceived;
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.DelimiterDataReceived += Server_DelimiterDataReceived;
            server.Start(Convert.ToInt32(Port()));
            Userstable.AddRow("Server Started", IP, Port(), dateTime);
            SyncStatsThread.Start();
            while (true)
            {
                Console.ReadLine();
            }
        }
        #region Client Events
        private static void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            Userstable.AddRow("Client Disconnected", IPAddress.Parse(((IPEndPoint)e.Client.LocalEndPoint).Address.ToString()), ((IPEndPoint)e.Client.LocalEndPoint).Port.ToString(), DateTime.Now);
        }

        private static void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            Userstable.AddRow("Client Connected",IPAddress.Parse(((IPEndPoint)e.Client.LocalEndPoint).Address.ToString()), ((IPEndPoint)e.Client.LocalEndPoint).Port.ToString(),DateTime.Now);
          
        }
        #endregion
        private static void Server_DelimiterDataReceived(object sender, Message e)
        {
           
            Console.WriteLine("Requsted: " + e.MessageString);
            if (e.MessageString == Auth_Code())
            {
                e.Reply(Project_Name());
            }
      
        }

        private static void Server_DataReceived(object sender, Message e)
        {
           
        }
    }
}

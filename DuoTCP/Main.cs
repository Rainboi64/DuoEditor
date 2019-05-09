using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using SimpleTCP;
namespace DuoTCP
{
   public class Client
    {
        
    }
    public class Server
    {
        SimpleTcpServer server = new SimpleTcpServer();
        public void Start(int port)
        {
            server.Start(port);
        }
        public bool ServerStartStatus()
        {
            return server.IsStarted;
        }
        public string ConnectedIps()
        {
            return Convert.ToString(server.GetListeningIPs());
        }

    }
}

using System;
using DuoDatabase;
using SimpleTCP;

namespace TeamController
{
   
    public class TeamControl
    {
        public static string Setup(string IP, int Port, string AuthCode,TimeSpan TimeoutSpan) 
        {
            var client = new SimpleTcpClient().Connect(IP, Port);
            client.StringEncoder = System.Text.ASCIIEncoding.ASCII;
          string ReturnString =  client.WriteLineAndGetReply(AuthCode,TimeoutSpan).MessageString;
            WRITE.CreateData(ReturnString, "\\Projects\\" + ReturnString + "\\Configurations\\", "\\Project_Name");
            client.Disconnect();
            return ReturnString;
        }
    }
}

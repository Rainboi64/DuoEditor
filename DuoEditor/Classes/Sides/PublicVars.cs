namespace DuoEditor
{

    public class PublicVars
    {
        public static string ip { get; set; }
        public static string CleanIp { get; set; }
        public static void Host(string Dir, int Port) { MainServer.SimpleHTTPServer MS = new MainServer.SimpleHTTPServer(Dir,Port);}
    }
}

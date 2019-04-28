﻿using System;
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
namespace DuoEditor
{

    class MainServer
    {
        
        [DllImport("kernel32.dll")]
   static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
       const int SW_SHOW = 5;
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("Shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        static void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                File.Delete("tempbat.bat");
            }
            catch (Exception) { }
            Logger.CleanLogs();
        }
        public static bool IsAssociated()
        {
            return (Registry.CurrentUser.OpenSubKey("Software\\Classes\\.DSLF", false) == null);
        }
        public static void Associate()
        {
            RegistryKey FileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.DSLF");
            RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\DSLogViewer.exe");
            RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.DSLF");
            FileReg.CreateSubKey("DefaultIcon").SetValue("", System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\DSFLIcon.ico"));
            FileReg.CreateSubKey("PreceivedType").SetValue("", "Text");
            AppReg.CreateSubKey("shell\\Open\\command").SetValue("", "\"" + Application.ExecutablePath + "\"%1");
            AppReg.CreateSubKey("shell\\View\\command").SetValue("", "\"" + Application.ExecutablePath + "\"%1");
            AppReg.CreateSubKey("DefaultIcon").SetValue("", System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\DSFLIcon.ico"));
            AppAssoc.CreateSubKey("UserChoice").SetValue("Progid", "Applications\\DSLogViewer");
            SHChangeNotify(0x000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
        public static void SplashWork()
        {

            if (!File.Exists("Logs.DSLF"))
            {
                StreamWriter txtoutput = new StreamWriter("Logs.DSLF");
                txtoutput.Write("");
                txtoutput.Close();
            }
            if (!File.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\"));
            }
            if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month)))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month+"\\"));
            }
            if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month+"\\"+DateTime.Now.DayOfWeek+"\\")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\"+ DateTime.Now.Day +"_"+ DateTime.Now.DayOfWeek + "\\"));
            }
            if (IsAssociated())
            {
                Associate();
            }

            Logger.ProccesLogs();
            Thread.Sleep(3000);
        }
        public static void Splash()
        {
            
            SplashScreen.SplashForm frm = new SplashScreen.SplashForm();
            frm.AppName = "";
            frm.BackgroundImage = Properties.Resources.SplashLogo;
            frm.BackgroundImageLayout = ImageLayout.Center;
            frm.Icon = Properties.Resources.SplashIcon;
            frm.ShowInTaskbar = false;
            Thread.Sleep(20);
            Application.Run(frm);
       

        }
        public static void MainFormStarter()
        {
            Application.Run(new MainForm());
        }
        public static void FormStarter()
        {
            Console.ResetColor();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            ThreadStart FormStart_ = new ThreadStart(Splash);
            Thread FormStarterThread = new Thread(FormStart_);
            FormStarterThread.SetApartmentState(ApartmentState.STA);
            ThreadStart FormStart1_ = new ThreadStart(MainFormStarter);
            Thread FormStarterThread1 = new Thread(FormStart1_);
            FormStarterThread1.SetApartmentState(ApartmentState.STA);
            FormStarterThread.Start();
            SplashWork();
            FormStarterThread.Abort();
            FormStarterThread1.Start();
            Console.ResetColor();
        }
     


        public static void ShowConsole()
        {
            var handle = GetConsoleWindow();
                ShowWindow(handle, SW_SHOW);
        }
        public static void HideConsole()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }

     
        static void Main(string[] args)
        {
            
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            try
            {
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
                if (args.Contains("/h"))
                {
                    ShowWindow(handle, SW_SHOW);

                }
                FormStarter();
                Console.Title = "DuoServer Alpha "+PublicFuncs.APPVERSION+" DE Edition";
            }
            catch (Exception i)
            {
                Logger.LogEx(i);
            }
            string ip;
            string GetIP()
            {
                
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                IPAddress[] addr = ipEntry.AddressList;
                String GetIpResult = addr[addr.Length - 1].ToString();

                if ((args.Length >= 1)) 
              
                {
                    try
                    {
                       return args[3];
                    }
                    catch (Exception i)
                    {
                        
                   Logger.Log(" DuoServer Alpha "+PublicFuncs.APPVERSION+" DE Edition" +
                       " \n Looks like there was an error proccesing your arguments " +
                       "\n This is some help..........." +
                       "\n The First space is used to specify the file that is wanted to be hosted" +
                       "\n The Second space is used to specify the port of the server" +
                       "\n The Third space is to speicify the ip address" +
                       "\n /h to stop the editor from launching." +
                       "\n Here is the error you`ve made");
                        Logger.LogEx(i);
                    }
                }
                return GetIpResult;
            }
            try
            {
                if (!(args.Length >= 1))
                {
                    try
                    {
                        string s = "www";

                        Console.Clear();
                        SimpleHTTPServer myServer = new SimpleHTTPServer(s);

                        PublicFuncs.CleanIp = (GetIP() + ":" + myServer.Port.ToString());
                   Logger.Log("Server.Started On: " + GetIP() + ":" + myServer.Port.ToString() + "\n");
                        ip = GetIP() + ":" + myServer.Port.ToString();
                    }
                    catch (Exception i) { Logger.LogEx(i); }
                }
                else
                {
                    Console.Clear();
                    string path = args[0].ToString();
                    int port = Convert.ToInt32(args[1].ToString());
                    SimpleHTTPServer myServer = new SimpleHTTPServer(path, port);
                    PublicFuncs.CleanIp = (GetIP() + ":" + myServer.Port.ToString());
               Logger.Log("Server.Started On: " + GetIP() + ":" + myServer.Port.ToString() + "\n");
                    ip = GetIP() + ":" + myServer.Port.ToString();


                }
            }
            catch (Exception i) {
             
                Logger.Log(" DuoServer Alpha "+PublicFuncs.APPVERSION+" DE Edition" +
  " \n Looks like there was an error proccesing your arguments " +
  "\n This is some help..........." +
  "\n The First space is used to specify the file that is wanted to be hosted" +
  "\n The Second space is used to specify the port of the server" +
  "\n The Third space is to speicify the ip address" +
  "\n /h to stop the editor from launching." +
  "\n Here is the error you`ve made" 
);
                Logger.LogEx(i);
            };
        } 
        public readonly string GetIP = PublicFuncs.CleanIp;
        public class SimpleHTTPServer
        {

            private readonly string[] _indexFiles = {
        "index.html",
        "index.htm",
        "default.html",
        "default.htm",
        "index.php"
    };

            private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        #region extension to MIME type list
        {".asf", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".avi", "video/x-msvideo"},
        {".bin", "application/octet-stream"},
        {".cco", "application/x-cocoa"},
        {".crt", "application/x-x509-ca-cert"},
        {".css", "text/css"},
        {".deb", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dll", "application/octet-stream"},
        {".dmg", "application/octet-stream"},
        {".ear", "application/java-archive"},
        {".eot", "application/octet-stream"},
        {".exe", "application/octet-stream"},
        {".flv", "video/x-flv"},
        {".gif", "image/gif"},
        {".hqx", "application/mac-binhex40"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".ico", "image/x-icon"},
        {".img", "application/octet-stream"},
        {".iso", "application/octet-stream"},
        {".jar", "application/java-archive"},
        {".jardiff", "application/x-java-archive-diff"},
        {".jng", "image/x-jng"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".mml", "text/mathml"},
        {".mng", "video/x-mng"},
        {".mov", "video/quicktime"},
        {".mp3", "audio/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpg", "video/mpeg"},
        {".msi", "application/octet-stream"},
        {".msm", "application/octet-stream"},
        {".msp", "application/octet-stream"},
        {".pdb", "application/x-pilot"},
        {".pdf", "application/pdf"},
        {".pem", "application/x-x509-ca-cert"},
        {".pl", "application/x-perl"},
        {".pm", "application/x-perl"},
        {".png", "image/png"},
        {".prc", "application/x-pilot"},
        {".ra", "audio/x-realaudio"},
        {".rar", "application/x-rar-compressed"},
        {".rpm", "application/x-redhat-package-manager"},
        {".rss", "text/xml"},
        {".run", "application/x-makeself"},
        {".sea", "application/x-sea"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".swf", "application/x-shockwave-flash"},
        {".tcl", "application/x-tcl"},
        {".tk", "application/x-tcl"},
        {".txt", "text/plain"},
        {".war", "application/java-archive"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wmv", "video/x-ms-wmv"},
        {".xml", "text/xml"},
        {".xpi", "application/x-xpinstall"},
        {".zip", "application/zip"},
        #endregion
    };
            private Thread _serverThread;
            private string _rootDirectory;
            private HttpListener _listener;
            private int _port;

            public int Port
            {
                get { return _port; }
                private set { }
            }

            /// <summary>
            /// Construct server with given port.
            /// </summary>
            /// <param name="path">Directory path to serve.</param>
            /// <param name="port">Port of the server.</param>
            public SimpleHTTPServer(string path, int port)
            {
                this.Initialize(path, port);
            }

            /// <summary>
            /// Construct server with suitable port.
            /// </summary>
            /// <param name="path">Directory path to serve.</param>
            public SimpleHTTPServer(string path)
            {
                //get an empty port
                TcpListener l = new TcpListener(IPAddress.Loopback, 0);

                l.Start();
                int port = ((IPEndPoint)l.LocalEndpoint).Port;
                l.Stop();
                this.Initialize(path, port);
            }

            /// <summary>
            /// Stop server and dispose all functions.
            /// </summary>
            public void Stop()
            {
                _serverThread.Abort();
                _listener.Stop();
            }

            private void Listen()
            {
                _listener = new HttpListener();
                _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
                _listener.Start();
           Logger.Log(Port+ " : Listener Started at " + DateTime.Now);
                while (true)
                {
                    try
                    {
                        HttpListenerContext context = _listener.GetContext();
                        Process(context);
                    }
                    catch (Exception i)
                    {
                        Logger.Log(Port + " : Listener Error Catched: ");
                        Logger.LogEx(i);
                        _listener.Stop();
                        _listener.Start();
                    }
                }
            }

            private void Process(HttpListenerContext context)
            {
                string filename = context.Request.Url.AbsolutePath;
           Logger.Log(Port + " : Requsted: " + filename);
                filename = filename.Substring(1);

                if (string.IsNullOrEmpty(filename))
                {
               Logger.Log( _port+ " : Error: Filename.IsNullOrEmpty at " + DateTime.Now);
                    foreach (string indexFile in _indexFiles)
                    {
                        if (File.Exists(Path.Combine(_rootDirectory, indexFile)))
                        {
                            filename = indexFile;
                            break;
                        }
                    }
                }

                filename = Path.Combine(_rootDirectory, filename);

                if (File.Exists(filename))
                {
                    try
                    {
                        Stream input = new FileStream(filename, FileMode.Open);

                        //Adding permanent http response headers
                        string mime;
                        context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                        context.Response.ContentLength64 = input.Length;
                        context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                        context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                        byte[] buffer = new byte[1024 * 16];
                        int nbytes;
                        while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                            context.Response.OutputStream.Write(buffer, 0, nbytes);
                        input.Close();

                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.OutputStream.Flush();
                   Logger.Log(_port + " : Info: Request Served + Flushed at " + DateTime.Now);
                    }
                    catch (Exception i)
                    {
                        Logger.LogEx(i);
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                   Logger.Log(_port + " : Warning: Internal Server Error Code: " +i+ " " + DateTime.Now);
                    }

                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
               Logger.Log(_port + " : Warning: Internal Server Error Code: Path not found at " + DateTime.Now);
                }

                context.Response.OutputStream.Close();
           Logger.Log(_port + " : Info: Stream.Closed\n");

            }

            private void Initialize(string path, int port)
            {
                Stopwatch sw = Stopwatch.StartNew();
                ThreadStart childref = new ThreadStart(CallToChildThread);
           Logger.Log("Loading DuoServer Alpha "+PublicFuncs.APPVERSION+" DE Edition\n");
                this._rootDirectory = path;
                this._port = port;
           Logger.Log(_port + " : Server Starting with parameters: " + _port + " " + _rootDirectory);
                _serverThread = new Thread(this.Listen);
                _serverThread.Start();
           Logger.Log(_port + " : Server Thread Started Succsesfully");
                Thread childThread = new Thread(childref);
                Thread insertThread = new Thread(w84insert);
                childThread.Start();
                insertThread.Start();
                void w84insert()
                {
                    while (true)
                    {
                        while (Console.ReadKey(true).Key == ConsoleKey.Insert) 
                        {
                            HideConsole();
                            PublicFuncs.shwn = false;
                        }
                    }
                }
                void CallToChildThread()
                {
               Logger.Log(_port + " : Input Thread Started");
                    while (1 == 1)
                    {
           
                        string _input()
                        {
                           return Console.ReadLine();
                        }
                        string input = _input();
                        if (input == "clear")
                        {
                            Console.Clear();
                        }
                        else if (input == "ip")
                        {
                            Logger.Log(PublicFuncs.CleanIp);
                        }
                        else if (input == "stop")
                        {
                            Environment.Exit(1);
                        }
                        else if (input == "test")
                        {
                            System.Diagnostics.Process.Start(@"http://" + PublicFuncs.CleanIp);
                        }
                        else if (input == "starttime")
                        {
                            Logger.Log(Convert.ToString(sw.Elapsed));
                        }
                        else if (input == "new")
                        {
                            ThreadStart FormStart_ = new ThreadStart(FormStarter);
                            Thread FormStarterThread = new Thread(FormStart_);
                            FormStarterThread.SetApartmentState(ApartmentState.STA);
                            FormStarterThread.Start();
                        }
                        else if (input == "nhost")
                        {
                            Console.Write("Enter a Directory Name: ");
                                string dir = Console.ReadLine();
                            Console.Write("->Enter a port: ");
                            int _port = Convert.ToInt32(Console.ReadLine());
                            PublicFuncs.Host(dir,_port);
                        }
                        else if (input == "cleanlogbunk")
                        {
                            Logger.ClearLogs();
                            Logger.Log("Cleaned Log Bunk");
                        }
                        else if (input == "export")
                        {
                            Logger.CleanLogs();
                        }
                        else if (input == "viewlogbunk")
                        {
                            Console.Write(Logger.GetLogs);
                        }
                        else if (input == "hide")
                        {
                            HideConsole();
                            PublicFuncs.shwn = false;
                        }
                        else if (input == "viewlogfile")
                        {
                            Console.WriteLine(Logger.ViewLogfile());
                        }
                        else
                        {
                            Logger.Log("Not a Command. Check case sensitivity");
                        } 
                    }
                }
            }
            public string Ip = PublicFuncs.CleanIp;
        }

    }

}


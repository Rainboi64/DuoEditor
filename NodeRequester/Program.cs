using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
namespace NodeRequester
{
    class Program
    {

        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }

        private const int GWL_STYLE = -16;              //hex constant for style changing
        private const int WS_BORDER = 0x00800000;       //window with border
        private const int WS_CAPTION = 0x00C00000;      //window with a title bar
        private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        private const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox
        public static string WINDOW_NAME = "Requester";
       
        //Requester
        static void Main(string[] args)
        {
            WINDOW_NAME = args[0];
            Console.Title = WINDOW_NAME;
            makeBorderless();
            try
            {
                Thread.Sleep(Convert.ToInt32(args[2]));
            }
            catch (Exception)
            {
            }            // This is the code for the base process
            Process myProcess = new Process();
            // Start a new instance of this program but specify the 'spawned' version.
            try
            {

                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(args[0], args[1]);
                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo = myProcessStartInfo;
                myProcess.Start();
                StreamReader myStreamReader = myProcess.StandardOutput;
                // Read the standard output of the spawned process.
                while (myStreamReader.EndOfStream == false)
                {
                    string myString = myStreamReader.ReadLine();
                    Console.WriteLine(myString);
                }

                myProcess.WaitForExit();
                myProcess.Close();
                

            }
            catch (IndexOutOfRangeException)
            {
                if (args[0] == "node")
                {
                    Console.WriteLine("NodeJS Prompt:"); 
                }
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(args[0],"");
                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo = myProcessStartInfo;
                myProcess.Start();
                StreamReader myStreamReader = myProcess.StandardOutput;
                // Read the standard output of the spawned process.
                while (myStreamReader.EndOfStream == false)
                {
                    string myString = myStreamReader.ReadLine();
                    Console.WriteLine(myString);
                }

                myProcess.WaitForExit();
                myProcess.Close();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("..END OF EXECUTION..");
            Console.WriteLine("Press any key to continue......");
            Console.ReadKey();
        }
        static void makeBorderless()
        {
            // Get the handle of self
            IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            RECT rect;
            // Get the rectangle of self (Size)
            GetWindowRect(window, out rect);
            // Get the handle of the desktop
            IntPtr HWND_DESKTOP = GetDesktopWindow();
            // Attempt to get the location of self compared to desktop
            MapWindowPoints(HWND_DESKTOP, window, ref rect, 2);
            // update self
            SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
            // rect.left rect.top should work but they're returning negative values for me. I probably messed up
            SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);
            DrawMenuBar(window);
        }

    }
}

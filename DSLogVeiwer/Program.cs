using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
namespace DSLogVeiwer
{
    static class Program
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!(IsAssociated()))
            {
                Associate();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {

                Application.Run(new DSLogVeiwer(""));
            }
            else
            {
                Application.Run(new DSLogVeiwer(args[0]));
            }
        }
        public static bool IsAssociated()
        {
            return (Registry.CurrentUser.OpenSubKey("Software\\Classes\\.DSLF", false) == null);
        }
        public static void Associate()
        {
            RegistryKey FileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.DSLF");
            RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\DSLogViewer.exe");
            RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.txge");
            FileReg.CreateSubKey("DefaultIcon").SetValue("", System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\DSFLIcon"));
            FileReg.CreateSubKey("PreceivedType").SetValue("", "Text");
            AppReg.CreateSubKey("shell\\Open\\command").SetValue("", "\"" + Application.ExecutablePath + "\"%1");
            AppReg.CreateSubKey("shell\\View\\command").SetValue("", "\"" + Application.ExecutablePath + "\"%1");
            AppReg.CreateSubKey("DefaultIcon").SetValue("", System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Files\\DSFLIcon"));
            AppAssoc.CreateSubKey("UserChoice").SetValue("Progid", "Applications\\DSLogViewer");
            SHChangeNotify(0x000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
    }
}

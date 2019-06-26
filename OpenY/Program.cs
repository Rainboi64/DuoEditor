using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenY
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenY_Compiler.Compiler compiler = new OpenY_Compiler.Compiler();
            compiler.Lexxer(System.IO.File.ReadAllText("temp"));
        }

      
    }
}

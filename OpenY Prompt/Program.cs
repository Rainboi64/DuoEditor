using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenY_Prompt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OpenY Prompt : ");
            OpenY_Compiler.Compiler compiler = new OpenY_Compiler.Compiler();

            while (true)
            {
                var input = Console.ReadLine();
                compiler.Lexxer(input); 
            }
        }
    }
}

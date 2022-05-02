using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Tools
{
    public class PrintLine
    {
        public static void Print()
        {
            string line = new('-', 60);
            Console.WriteLine(line);
        }
    }
}

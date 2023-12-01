using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures
{
    interface IWriteLine
    {
        public void WriteLine()
        {
            Console.WriteLine("Wow C# 8!");
        }

        public int GetVal()
        {
            return 10;
        }
    }

    public class WL : IWriteLine
    {
        public int GetVal()
        {
            return 20;
        }

        public void WriteLine()
        {
            Console.WriteLine("Wow C# 8! new features");
        }
    }
}

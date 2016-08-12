using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppConfigTester.VmWare;

namespace AppConfigTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = VmWareServiceConfigLoader.LoadConfig();
            new object();
        }
    }
}

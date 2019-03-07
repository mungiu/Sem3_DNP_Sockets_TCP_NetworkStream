using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConnectLib.Class1 serverLib = new ServerConnectLib.Class1;

            serverLib.test();

        }
    }
}

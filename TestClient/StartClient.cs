using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class StartClient
    {
        private static NetworkConnectLib.Client clientLib;
        private static string serverIP;
        private static int port = 5015;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting client...");
            serverIP = "192.254.84.153";
        }
    }
}

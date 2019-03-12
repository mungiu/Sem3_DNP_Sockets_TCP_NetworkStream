using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestServer
{
    class StartServer
    {
        private static NetworkConnectLib.Server serverLib;
        private static IPAddress localIP;
        private static int port = 5015;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server...");
            localIP = GetLocalIPAddress();
            serverLib = new NetworkConnectLib.Server(localIP, port);

            //Console.WriteLine("Waiting for someone t oconnect ...");
            //Task.Run(() => serverLib.getTCPNetworkStreamAsync()).Wait();
            

            Console.WriteLine("Input message to send...");
            while (true)
            {
                string clientMessage = Console.ReadLine();

                if (clientMessage != null)
                {
                    Task.Run(() => serverLib.WriteNetworkStreamAsync(clientMessage));
                    clientMessage = null;
                }
            }

        }

        public static IPAddress GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}

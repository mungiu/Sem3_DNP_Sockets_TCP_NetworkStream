using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestServer2
{
    class Program
    {
        private static ServerConnectLib.Server serverLib;
        private static IPAddress localIP;
        private static int port = 5016;
        private static byte[] buffer;

        static void Main(string[] args)
        {
            localIP = GetLocalIPAddress();

            Console.WriteLine("Starting client...");
            serverLib = new ServerConnectLib.Server(localIP, port);
            Task.Run(() => serverLib.getTCPNetworkStreamAsync()).Wait();

            // NOTE: No while loop
            Console.WriteLine("Input message to send...");
            string clientMessage = Console.ReadLine();

            if (clientMessage != null)
            {
                Task.Run(() => serverLib.WriteToNetworkStreamAsync(serverLib.NetworkStream, clientMessage));
                clientMessage = null;
            }

            Task.Run(() => ReceiveMessages());
        }

        public async static void ReceiveMessages()
        {
            buffer = new byte[100];
            await serverLib.ReadFromNetworkStreamAsync(serverLib.NetworkStream, buffer);

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

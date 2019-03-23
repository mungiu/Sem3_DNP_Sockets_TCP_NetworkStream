using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TestClient
{
    class StartClient
    {
        private static NetworkConnectLib.Client clientLib;
        private static string serverIP = "169.254.84.153";
        private static int port = 5538;
        private static string sendMessage;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting client...");
            clientLib = new NetworkConnectLib.Client(serverIP, port);

            while (true)
            {
                sendMessage = Console.ReadLine();
                Task.Run(() => clientLib.WriteNetworkStreamAsync(sendMessage));
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

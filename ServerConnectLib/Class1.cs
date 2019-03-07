using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerConnectLib
{
    public class Class1
    {
        // getting the IP address
        public IPAddress getIpOfDomain(string domainName)
        {
            return new IPAddress(long.Parse(Dns.GetHostAddresses("google.com").ToString()));
        }

        public async void startTCPListen(IPAddress localIP, int port)
        {
            /// NOTE: using System.Net.Sockets;
            TcpListener tcpListener = new TcpListener(localIP, port);
            tcpListener.Start();
            Console.WriteLine("TCP listen started...");

            TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
        }
    }
}

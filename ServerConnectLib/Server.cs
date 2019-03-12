using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkConnectLib
{
    public class Server
    {
        public NetworkStream NetworkStream { get; set; }
        private IPAddress LocalIP { get; set; }
        private int Port { get; set; }

        public TcpListener TcpListener { get; set; }
        public TcpClient TcpClient { get; set; }
        private static byte[] writeBuffer;
        private static byte[] readBuffer;

        public Server(IPAddress localIP, int port)
        {
            LocalIP = localIP;
            Port = port;

            TcpListener = new TcpListener(LocalIP, Port);
            TcpListener.Start();

            Console.WriteLine("Waiting for someone to connect ...");
            Task.Run(() => getTCPNetworkStreamAsync()).Wait();
            Console.WriteLine("Network stream etablished...");
            Task.Run(() => ReadNetworkStreamAsync());

            Console.WriteLine("TCP listen started...");
        }

        public IPAddress getIpOfDomain(string domainName)
        {
            return new IPAddress(long.Parse(Dns.GetHostAddresses("google.com").ToString()));
        }

        public async Task getTCPNetworkStreamAsync()
        {
            ///An await expression does not block the thread on which it is executing. 
            ///Instead, it causes the compiler to sign up the rest of the async method 
            ///as a continuation on the awaited task. Control then returns to the caller 
            ///of the async method. When the task completes, it invokes its continuation, 
            ///and execution of the async method resumes where it left off.
            ///
            ///An await expression can occur only in the body of its enclosing method, 
            ///lambda expression, or anonymous method, which must be marked with an async 
            ///modifier.The term await serves as a keyword only in that context. 
            ///Elsewhere, it is interpreted as an identifier.Within the method, lambda 
            ///expression, or anonymous method, an await expression cannot occur in the 
            ///body of a synchronous function, in a query expression, in the block of a 
            ///lock statement, or in an unsafe context.
            TcpClient tcpClient = await TcpListener.AcceptTcpClientAsync();

            NetworkStream = TcpClient.GetStream();
            Console.WriteLine("TCP connection established and NetworkStream found");
        }

        /// <summary>
        /// Run this method in a separate Task/thread
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WriteNetworkStreamAsync(string message)
        {
            if (NetworkStream == null)
                await getTCPNetworkStreamAsync();

            writeBuffer = new byte[1024];
            writeBuffer = Encoding.ASCII.GetBytes(message);
            await NetworkStream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
        }

        public async Task ReadNetworkStreamAsync()
        {
            while (true)
            {
                if (NetworkStream == null)
                    await getTCPNetworkStreamAsync();

                if (NetworkStream.DataAvailable)
                {
                    readBuffer = new byte[1024];
                    int byteCount = await NetworkStream.ReadAsync(readBuffer, 0, readBuffer.Length);
                    Console.WriteLine($"Received: {Encoding.ASCII.GetString(readBuffer, 0, byteCount)}");
                }

            }
        }
    }
}
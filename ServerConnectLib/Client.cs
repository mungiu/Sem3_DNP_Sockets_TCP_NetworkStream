using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkConnectLib
{
    public class Client
    {
        NetworkStream networkStream;
        TcpClient client;

        public Client()
        {
            try
            {
                client = new TcpClient(serverIp, port);
            }
            catch (SocketException)
            {
                Console.WriteLine("Unable to connect to server");
                return;
            }

            networkStream = client.GetStream();
        }

        public void Start(string serverIp, int port)
        {
            byte[] data = new byte[1024];
            string message, stringData;

            Console.WriteLine("Connected to server");

            while (true)
            {
                message = Console.ReadLine();
                Task.Run(() => WriteNetworkStreamAsync(message));

                data = new byte[1024];
                int recv = networkStream.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
        }

        public async void ReadNetworkStreamAsync()
        {

        }

        public async void WriteNetworkStreamAsync(string message)
        {
            await networkStream.WriteAsync(Encoding.ASCII.GetBytes(message), 0, message.Length);
            networkStream.Flush();
        }
    }
}

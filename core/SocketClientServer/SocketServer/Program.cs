using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SocketServer
{
    class Program
    {
        private static IDictionary<Client, Socket> _clients = new Dictionary<Client, Socket>();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveBufferSize = 1024;
            Console.WriteLine("Bind EndPoint");
            // await socket.ConnectAsync(IPAddress.Parse("192.168.1.39"), 1234);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 1234));
            Console.WriteLine("Start listening");
            socket.Listen(10);

            while (true)
            {
                Console.WriteLine("Waiting for a new client");
                var acceptedSocket = await socket.AcceptAsync();

                await new TaskFactory().StartNew(async (clientSocket) =>
                {
                    await new Program().WorkWithSocket(clientSocket as Socket);
                }, acceptedSocket);
            }
        }

        private async Task WorkWithSocket(Socket socket)
        {
            Client client = null;
            Console.WriteLine("Start work with client");

            while (true)
            {
                var byteArray = new byte[1024];
                var memoryBuffer = new Memory<byte>(byteArray);
                var cancellationToken = new CancellationTokenSource().Token;

                Console.WriteLine("Start receiving");
                var result = await socket.ReceiveAsync(memoryBuffer, SocketFlags.None, cancellationToken);
                KeyValuePair<Client, Socket> currentSocket = new KeyValuePair<Client, Socket>();

                foreach (var sct in _clients)
                {
                    if (sct.Value == socket)
                    {
                        currentSocket = new KeyValuePair<Client, Socket>(sct.Key, sct.Value);
                        break;
                    }
                }

                var text = Encoding.UTF32.GetString(byteArray);

                ReadOnlyMemory<byte> buffer = null;

                if (string.IsNullOrEmpty(currentSocket.Key?.Nick))
                {
                    try
                    {
                        client = JsonConvert.DeserializeObject<Client>(text);
                        _clients.Add(client, socket);

                        byteArray = Encoding.UTF32.GetBytes("You are connected"); ;
                        buffer = new ReadOnlyMemory<byte>(byteArray);
                        await socket.SendAsync(buffer, SocketFlags.None);

                        foreach (var activeClient in _clients)
                        {
                            if (activeClient.Key.Nick != client.Nick)
                            {
                                byteArray = Encoding.UTF32.GetBytes($"Client '{client.Nick}' connectecd to chat"); ;
                                buffer = new ReadOnlyMemory<byte>(byteArray);
                                await activeClient.Value.SendAsync(buffer, SocketFlags.None);
                            }
                        }
                    }
                    catch
                    {
                        byteArray = Encoding.UTF32.GetBytes("Should setup name first");
                        buffer = new ReadOnlyMemory<byte>(byteArray);

                        await socket.SendAsync(buffer, SocketFlags.None);
                    }

                    continue;
                }

                Console.WriteLine($"Message: {text}");

                byteArray = Encoding.UTF32.GetBytes($"[{currentSocket.Key.Nick}]: {text}"); ;
                buffer = new ReadOnlyMemory<byte>(byteArray);

                foreach (var activeClient in _clients)
                {
                    if (activeClient.Key.Nick != currentSocket.Key.Nick)
                    {
                        await activeClient.Value.SendAsync(buffer, SocketFlags.None);
                    }
                }
            }
        }
    }

    class Client
    {
        public string Nick { get; set; }
    }
}

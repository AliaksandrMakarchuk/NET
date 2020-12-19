using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SocketClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            await socket.ConnectAsync(new IPEndPoint(IPAddress.Loopback, 1234));

            Console.WriteLine("Connected");
            Console.Write("Input nick: ");
            var nick = Console.ReadLine();

            var byteArray = Encoding.UTF32.GetBytes(JsonConvert.SerializeObject(new Client { Nick = nick }));
            var buffer = new ReadOnlyMemory<byte>(byteArray);
            await socket.SendAsync(buffer, SocketFlags.None);

            Console.WriteLine("Start chat");

            await new TaskFactory().StartNew(async (clientSocket) =>
            {
                await ProcessConnection(clientSocket as Socket);
            }, socket);

            while (true)
            {
                Console.Write("Input message: ");
                var message = Console.ReadLine();

                byteArray = Encoding.UTF32.GetBytes(message);
                buffer = new ReadOnlyMemory<byte>(byteArray);

                await socket.SendAsync(buffer, SocketFlags.None);
                Console.WriteLine("Message sent");
            }
        }

        private static async Task ProcessConnection(Socket socket)
        {
            while (true)
            {
                var byteArray = new byte[1024];
                var memoryBuffer = new Memory<byte>(byteArray);
                var cancellationToken = new CancellationTokenSource().Token;

                var result = await socket.ReceiveAsync(memoryBuffer, SocketFlags.None, cancellationToken);
                var text = Encoding.UTF32.GetString(byteArray);

                Console.WriteLine($"{Environment.NewLine}[SERVER] {text}");
            }
        }
    }

    class Client
    {
        public string Nick { get; set; }
    }
}

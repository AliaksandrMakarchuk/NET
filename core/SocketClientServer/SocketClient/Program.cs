using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocketClient
{
    class Program
    {
        private static Guid _clientId;
        private static byte[] _buffer = new byte[1024];

        static async Task Main(string[] args)
        {
            Console.Write("Input nick: ");
            var nick = Console.ReadLine();

            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            await socket.ConnectAsync(new IPEndPoint(IPAddress.Loopback, 1234));

            Console.WriteLine("Connected");

            // establising connection
            var result = socket.BeginReceive(_buffer, 0, 1024, SocketFlags.None, new Program().ReceivedCallback, new object());
            while (!result.AsyncWaitHandle.WaitOne()) { await Task.Delay(1000); }
            _buffer = new byte[1024];
            _buffer = Encoding.UTF32.GetBytes($"{_clientId}: {nick}");
            Console.WriteLine("DEBUG. Send nick");
            result = socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new Program().SendCallback, new object());
            // result = socket.BeginSend(_buffer)
            // ------------------------

            // var byteArray = Encoding.UTF32.GetBytes(JsonConvert.SerializeObject(new Client { Nick = nick }));
            // var buffer = new ReadOnlyMemory<byte>(byteArray);
            // await socket.SendAsync(buffer, SocketFlags.None);

            // Console.WriteLine("Start chat");

            // await new TaskFactory().StartNew(async (clientSocket) =>
            // {
            //     await ProcessConnection(clientSocket as Socket);
            // }, socket);

            while (!result.AsyncWaitHandle.WaitOne())
            {
                // Console.Write("Input message: ");
                // var message = Console.ReadLine();

                // byteArray = Encoding.UTF32.GetBytes(message);
                // buffer = new ReadOnlyMemory<byte>(byteArray);

                // await socket.SendAsync(buffer, SocketFlags.None);
                // Console.WriteLine("Message sent");
            }

            Console.WriteLine("Completed");
        }

        private void ReceivedCallback(IAsyncResult result)
        {
            Thread.Sleep(3000);
            var received = Encoding.UTF32.GetString(_buffer);
            var regex = new Regex("Ping: ([0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12})");
            var match = regex.Match(received);
            if (match.Success)
            {
                var stringGuid = match.Groups[1].Value;
                if (!Guid.TryParse(stringGuid, out _clientId))
                {
                    Console.WriteLine($"DEBUG. Could not convert received guid: {stringGuid}");
                }
            }
            else
            {
                Console.WriteLine($"DEBUG. Not matched: {received}");
            }
        }

        private void SendCallback(IAsyncResult result) { }

        // private static async Task ProcessConnection(Socket socket)
        // {
        //     while (true)
        //     {
        //         var byteArray = new byte[1024];
        //         var memoryBuffer = new Memory<byte>(byteArray);
        //         var cancellationToken = new CancellationTokenSource().Token;

        //         var result = await socket.ReceiveAsync(memoryBuffer, SocketFlags.None, cancellationToken);
        //         var text = Encoding.UTF32.GetString(byteArray);

        //         Console.WriteLine($"{Environment.NewLine}[SERVER] {text}");
        //     }
        // }
    }

    class Client
    {
        public string Nick { get; set; }
    }
}
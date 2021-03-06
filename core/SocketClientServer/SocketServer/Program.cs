﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketServer.Models;

namespace SocketServer
{
    class Program
    {
        private byte[] _buffer = new byte[1024];

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Server v1.0 ===");

            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveBufferSize = 1024;
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 1234));
            socket.Listen(10);
            Console.WriteLine("Started");

            while (true)
            {
                var acceptedSocket = await socket.AcceptAsync();

                await new TaskFactory().StartNew(async(clientSocket) =>
                {
                    await WorkWithSocket(clientSocket as Socket);
                }, acceptedSocket);
            }
        }

        private static async Task WorkWithSocket(Socket socket)
        {
            Console.WriteLine("New client");
            var clientId = ClientManager.Instance.AddClient();
            Console.WriteLine($"DEBUG. ClientId: {clientId}");

            var buffer = Encoding.UTF32.GetBytes($"Ping: {clientId}");

            // establish connection
            // var args = new SocketAsyncEventArgs();
            // args.SetBuffer(buffer);
            // args.Completed += OnSendCompleted;
            // var res = socket.SendAsync(args);

            Console.WriteLine($"DEBUG. Send {buffer.Length} bytes");
            var res = socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            res = socket.BeginReceive(_buffer, 0, 1024, SocketFlags.None, ReceiveCallback, new StateObject{WorkSocket = socket});
            while (!res.AsyncWaitHandle.WaitOne()) { await Task.Delay(1000); }

            if (!ClientManager.Instance.IsConnected(clientId))
            {
                Console.WriteLine("Connection refused");
            }

            Console.WriteLine("Run completed");
        }

        private void SendCallback(IAsyncResult result)
        {
            var socket = (Socket) result.AsyncState;
            var res = socket.EndSend(result);
            Console.WriteLine($"Was send {res} bytes");
            Console.WriteLine($"Sync: {result.CompletedSynchronously}");
            Console.WriteLine($"State: {result.AsyncState?.GetType()}");
            Console.WriteLine("Send complete");
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            var stateObject = (StateObject) result.AsyncState;
            var socket = stateObject.WorkSocket;

            var read = socket.EndReceive(result);

            Thread.Sleep(3000);
            var received = Encoding.UTF32.GetString(_buffer);
            Console.WriteLine($"DEBUG. Received: {received}");
            var regex = new Regex("([0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}): ([a-zA-Z]*)");
            var match = regex.Match(received);
            if (match.Success)
            {
                var stringGuid = match.Groups[1].Value;
                Guid guid;
                if (!Guid.TryParse(stringGuid, out guid)) { return; }
                var nick = match.Groups[3].Value;
                ClientManager.Instance.SetClientNick(guid, nick);
            }
            else
            {
                Console.WriteLine($"DEBUG. Not matched: {received}");
            }
        }
    }
}
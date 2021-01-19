using System;
using System.Collections.Generic;
using SocketServer.Models;

namespace SocketServer
{
    public class ClientManager
    {
        private object _locker;
        private static ClientManager _instance;

        public static ClientManager Instance
        {
            get
            {
                return _instance ?? (_instance = new ClientManager());
            }
        }

        private IDictionary<Guid, Client> _clients;

        private ClientManager()
        {
            _locker = new object();
            _clients = new Dictionary<Guid, Client>();
        }

        public Guid AddClient()
        {
            Guid clientId;

            lock(_locker)
            {
                clientId = Guid.NewGuid();
                _clients.Add(clientId, new Client());
            }

            return clientId;
        }

        public bool IsConnected(Guid clientId)
        {
            return _clients[clientId].IsConnected;
        }

        public void SetClientNick(Guid clientId, string nick)
        {
            _clients[clientId].Nick = nick;
            _clients[clientId].IsConnected = true;
        }
    }
}
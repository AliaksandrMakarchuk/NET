using System;
using System.Collections.Generic;
using SocketServer.Models;

namespace SocketServer
{
    public class ClientManager
    {
        private IDictionary<Guid, Client> _clients;

        public ClientManager()
        {
            _clients = new Dictionary<Guid, Client>();
        }

        public Guid AddClient()
        {
            var clientId = Guid.NewGuid();

            _clients.Add(clientId, new Client());

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
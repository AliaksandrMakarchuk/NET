using System.Net.Sockets;
using System.Text;

namespace SocketServer.Models
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
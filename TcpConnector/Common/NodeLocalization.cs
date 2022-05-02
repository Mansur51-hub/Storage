using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.Tools
{
    public class NodeLocalization
    {
        public NodeLocalization(string ip, int port)
        {
            Ip = ip ?? throw new ArgumentNullException(nameof(ip));
            Port = port;
        }

        public string Ip { get; }
        public int Port { get; }
    }
}

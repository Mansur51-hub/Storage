using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using TcpConnector.tools;
using TcpConnector.Tools;

namespace TcpConnector.QueryConstructor
{
    public class CreatePodQuery : IQuery
    {
        private readonly Packet _packet;
        private readonly NodeLocalization _nodeLocalization;

        public CreatePodQuery(string partialPath, string name, byte[] data, NodeLocalization nodeLocalization)
        {
            byte queryType = (byte)QueryType.AddFile;
            var headers = new List<HeaderStructure> {
                new HeaderStructure(partialPath.Length, TcpConverter.GetBytes(partialPath)),
                new HeaderStructure(name.Length, TcpConverter.GetBytes(name)),
            };
            
            _packet = new Packet(queryType, headers, data);
            _nodeLocalization = nodeLocalization ?? throw new ArgumentNullException(nameof(nodeLocalization));
        }

        public void Execute()
        {
            byte[] data = _packet.GetDataToSend();

            TcpSender.SendData(data, _nodeLocalization).Close();
        }

    }
}

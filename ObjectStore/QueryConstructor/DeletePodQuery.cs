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
    public class DeletePodQuery : IQuery
    {
        private readonly Packet _packet;
        private readonly NodeLocalization _nodeLocalization;

        public DeletePodQuery(string partialPath, string name, NodeLocalization nodeLoclization)
        {
            byte queryType = (byte)QueryType.RemoveFile;
            var headers = new List<HeaderStructure> {
                new HeaderStructure(partialPath.Length, TcpConverter.GetBytes(partialPath)),
                new HeaderStructure(name.Length, TcpConverter.GetBytes(name))
            };
            
            _packet = new Packet(queryType, headers, Array.Empty<byte>());
            _nodeLocalization = nodeLoclization ?? throw new ArgumentNullException(nameof(nodeLoclization));
        }

        public void Execute()
        {
            byte[] data = _packet.GetDataToSend();

            TcpSender.SendData(data, _nodeLocalization).Close();
        }
    }
}

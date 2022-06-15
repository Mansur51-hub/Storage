using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using TcpConnector.QueryConstructor;
using TcpConnector.tools;
using TcpConnector.Tools;

namespace ObjectStorage.QueryConstructor
{
    public class GetPodQuery : IQuery
    {
        private readonly Packet _packet;
        private readonly NodeLocalization _nodeLocalization;
        private byte[] _recievedData = Array.Empty<byte>();

        public GetPodQuery(string partialPath, string name, NodeLocalization nodeLoclization, NodeLocalization masterLocalization)
        {
            byte queryType = (byte)QueryType.GetFile;
            var headers = new List<HeaderStructure> {
                new HeaderStructure(partialPath.Length, TcpConverter.GetBytes(partialPath)),
                new HeaderStructure(name.Length, TcpConverter.GetBytes(name)),
                new HeaderStructure(masterLocalization.Ip.Length, TcpConverter.GetBytes(masterLocalization.Ip)),
                new HeaderStructure(masterLocalization.Port.ToString().Length, TcpConverter.GetBytes(masterLocalization.Port.ToString()))
            };

            _packet = new Packet(queryType, headers, Array.Empty<byte>());
            _nodeLocalization = nodeLoclization ?? throw new ArgumentNullException(nameof(nodeLoclization));
        }

        public void Execute()
        {
            var client = TcpSender.SendData(_packet.GetDataToSend(), _nodeLocalization);

            _recievedData = TcpSender.RecieveData(client);

            client.Close();
        }

        public byte[] RecieveData()
        {
            return _recievedData;
        }

    }
}

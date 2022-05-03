using TcpConnector.Common;
using TcpConnector.tools;
using TcpConnector.Tools;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using ObjectStorage.Handlers;
using ObjectStorage.MasterNode;

namespace TcpConnector.QueryConstructor
{
    public class CleanNodeQuery : IQuery
    {
        private readonly Packet _packet;
        private readonly NodeLocalization _nodeLocalization;
        private byte[] _recievedData = Array.Empty<byte>();

        public CleanNodeQuery(NodeLocalization nodeLoclization, NodeLocalization masterLocalization)
        {
            _nodeLocalization = nodeLoclization ?? throw new ArgumentNullException(nameof(nodeLoclization));

            byte queryType = (byte)QueryType.CleanNode;
            var headers = new List<HeaderStructure>
            {
                new HeaderStructure(masterLocalization.Ip.Length, TcpConverter.GetBytes(masterLocalization.Ip)),
                new HeaderStructure(masterLocalization.Port.ToString().Length, TcpConverter.GetBytes(masterLocalization.Port.ToString()))
            };

            _packet = new Packet(queryType, headers, Array.Empty<byte>());
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using TcpConnector.tools;
using TcpConnector.Tools;
using WorkerNode.Worker;

namespace WorkerNode.Handlers
{
    public class GetQueryHandler : IHandler
    {
        private readonly NetworkStream _stream;

        public GetQueryHandler(NetworkStream stream)
        {
            _stream = stream;
        }

        public int GetQueryType()
        {
            return (int)QueryType.GetFile;
        }

        public void Handle(byte[] data, IWorker worker)
        {
            var packet = new Packet(data);
            var path = TcpConverter.GetString(packet.Headers[0].Data);

            string fileName = TcpConverter.GetString(packet.Headers[1].Data);

            string filePath = worker.GetWorkerPath() + @"\" + path + @"\" + fileName;

            if(!File.Exists(filePath))
            {
                throw new Exception($"File with path {filePath} does not exist");
            }

            byte[] dataToSend = File.ReadAllBytes(filePath);

            _stream.Write(data, 0, dataToSend.Length);
        }
    }
}

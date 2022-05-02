using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using TcpConnector.tools;
using WorkerNode.Worker;

namespace WorkerNode.Handlers
{
    public class DeleteQueryHandler : IHandler
    {
        public int GetQueryType()
        {
            return (int)QueryType.RemoveFile;
        }

        public void Handle(byte[] data, IWorker worker)
        {
            var packet = new Packet(data);
            var path = TcpConverter.GetString(packet.Headers[0].Data);

            string fileName = TcpConverter.GetString(packet.Headers[1].Data);

            string absoluteDirectory = worker.GetWorkerPath() + @"\" + path;

            var directoryInfo = new DirectoryInfo(absoluteDirectory);
            if (!directoryInfo.Exists)
            {
                throw new Exception("Removing error: Can't find pod directory");
            }

            File.Delete(absoluteDirectory + @"\" + fileName);

            worker.GetLogger().CreateNewMessage($"Query type: {Enum.GetName(typeof(QueryType), GetQueryType())}");
            worker.GetLogger().CreateNewMessage($"File with name {fileName} deleted");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcpConnector.Common;
using TcpConnector.tools;
using WorkerNode.Worker;

namespace WorkerNode.Handlers
{
    public class CreateQueryHandler : IHandler
    {
        public int GetQueryType()
        {
            return (int)QueryType.AddFile;
        }

        public void Handle(byte[] data, IWorker worker)
        {
            var packet = new Packet(data);
            var path = TcpConverter.GetString(packet.Headers[0].Data);

            string fileName = TcpConverter.GetString(packet.Headers[1].Data);

            string absoluteDirectory = worker.GetWorkerPath() + @"\" + path;
            
            var directoryInfo = new DirectoryInfo(absoluteDirectory);
            if(!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }    

            using FileStream fstream = File.Create(absoluteDirectory + @"\" + fileName);
            fstream.Write(packet.Data, 0, packet.Data.Length);
            fstream.Close();

            worker.GetLogger().CreateNewMessage($"Query type: {Enum.GetName(typeof(QueryType), GetQueryType())}");
            worker.GetLogger().CreateNewMessage($"File with name {fileName} added");
        }
    }
}

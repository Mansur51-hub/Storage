using ObjectStorage.MasterNode;
using ObjectStorage.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using TcpConnector.tools;
using WorkerNode.Handlers;
using WorkerNode.Worker;

namespace ObjectStorage.Handlers
{
    public class CleanNodeMasterHandler
    {
        public static int GetQueryType()
        {
            return (int)QueryType.HandleFiles;
        }

        public static List<FilePodStructure> GetFilePodStructures(byte[] data)
        {
            var packet = new Packet(data);
            var pods = new List<FilePodStructure>();    

            var headers = packet.Headers;
            for(int i = 0; i < headers.Count; i += 3)
            {
                var partialPath =  TcpConverter.GetString(headers[i].Data);
                var name = TcpConverter.GetString(headers[i + 1].Data);
                byte[] fileData = headers[i + 2].Data;

                pods.Add(new FilePodStructure(name, partialPath, fileData));
            }

            return pods;
        }
    }
}

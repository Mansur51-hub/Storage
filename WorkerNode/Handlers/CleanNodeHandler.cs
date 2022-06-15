using System.Net.Sockets;
using TcpConnector.Common;
using TcpConnector.tools;
using TcpConnector.Tools;
using WorkerNode.Worker;

namespace WorkerNode.Handlers
{
    public class CleanNodeHandler : IHandler
    {
        private readonly NetworkStream _stream;

        public CleanNodeHandler(NetworkStream client) { 
            _stream = client; 
        }

        public int GetQueryType()
        {
            return (int)QueryType.CleanNode;
        }

        public void Handle(byte[] data, IWorker worker)
        {
            var packet = new Packet(data);
            var masterIp = TcpConverter.GetString(packet.Headers[0].Data);
            var masterPort = int.Parse(TcpConverter.GetString(packet.Headers[1].Data));

            worker.GetLogger().CreateNewMessage($"Query type: {Enum.GetName(typeof(QueryType), GetQueryType())}");
            worker.GetLogger().CreateNewMessage($"Clean node. Master: {masterIp}, {masterPort}");

            string[] allFiles = Directory.GetFiles(worker.GetWorkerPath(), "*.*", SearchOption.AllDirectories);

            var headers = GetHeadersAndRemoveFiles(allFiles, worker);

            var packetToSend = new Packet((byte)QueryType.CleanNode, headers, Array.Empty<byte>());

            SendPacket(packetToSend);
        }

        private static List<HeaderStructure> GetHeadersAndRemoveFiles(string[] allFiles, IWorker worker)
        {
            var headers = new List<HeaderStructure>();

            foreach (string file in allFiles)
            {
               
                var partialPath = GetPartialPath(file, worker);
                var name = partialPath.Split(@"\").Last();

                if (name.Length == partialPath.Length)
                {
                    partialPath = String.Empty;
                }
                else
                {
                    partialPath = partialPath[..^(name.Length + 1)];
                }

                if (name.StartsWith("logger"))
                {
                    continue;
                }

                headers.Add(new HeaderStructure(partialPath.Length, TcpConverter.GetBytes(partialPath)));
                headers.Add(new HeaderStructure(name.Length, TcpConverter.GetBytes(name)));

                var data = File.ReadAllBytes(file);
                headers.Add(new HeaderStructure(data.Length, data));

                File.Delete(file);

                worker.GetLogger().CreateNewMessage($"File named {name} cleaned");
                
            }

            RemoveDirectories(allFiles, worker);

            return headers;
        }

        private void SendPacket(Packet packet)
        {
            byte[] data = packet.GetDataToSend();
         
            _stream.Write(data, 0, data.Length);
            //Console.WriteLine(_stream.Length);
        }

        private static string GetPartialPath(string fileName, IWorker worker)
        {
            return fileName[(worker.GetWorkerPath().Length + 1)..];
        }

        private static void RemoveDirectories(string[] files, IWorker worker)
        {
            foreach (string file in files)
            {

                var partialPath = GetPartialPath(file, worker);
                var name = partialPath.Split(@"\").Last();

                if (name.Length == partialPath.Length)
                {
                    partialPath = String.Empty;
                }
                else
                {
                    partialPath = partialPath[..^(name.Length + 1)];
                }

                if (name.StartsWith("logger"))
                {
                    continue;
                }

                var path = worker.GetWorkerPath() + @"\" + partialPath;

                var dirInfo = new DirectoryInfo(path);

                if(dirInfo.Exists)
                {
                    dirInfo.Delete(true);
                }

            }
        }
    }
}

using ObjectStorage.Algorithms;
using ObjectStorage.Handlers;
using ObjectStorage.Services;
using ObjectStorage.Services.Storages;
using ObjectStorage.Services.WorkerNode;
using ObjectStorage.Tools;
using ObjectStorage.Tools.FileSystem;
using System.Net;
using System.Net.Sockets;
using TcpConnector.Common;
using TcpConnector.QueryConstructor;
using TcpConnector.Tools;
using WorkerNode.Worker;

namespace ObjectStorage.MasterNode
{
    public class Master : IMaster
    {
        private readonly INodeStorage _nodeStorage = new NodeStorage();
        private readonly IFileStorage _fileStorage = new FileStorage();
        private readonly NodeLocalization _localization = new("127.0.0.1", 8080);

        public Master()
        {

        }

        public Master(string ip, int port)
        {
            _localization = new NodeLocalization(ip, port);
        }

        public void AddFile(string path, string partialPath, string name)
        {
            IRepository repository = new FileSystemRepository();
            byte[] data = repository.GetData(path);

            AddFile(data, partialPath, name);
        }

        public void AddFile(byte[] data, string partialPath, string name)
        {
            var node = new SortNodeAlgorithm().GetNode(data.Length, _nodeStorage);

            _nodeStorage.UpdateWorkerInfo(node.GetName(), data.Length, QueryType.AddFile);
            _fileStorage.AddFilePod(new FilePod(name, partialPath, node.GetName(), data.Length));

            new CreatePodQuery(partialPath, name, data, node.GetLocalization()).Execute();
        }

        public void AddNode(string name, NodeLocalization nodeLocalization, int totalMemory)
        {
            _nodeStorage.AddWorker(new LocalWorker(nodeLocalization, name, totalMemory));

             new LocalWorkerNodeStarter(nodeLocalization, name).Start();
        }

        public void BalanceNodes()
        {
            new PrimitiveBalanceNodesAlgorithm().Execute(_nodeStorage, _fileStorage, this);
        }

        public void CleanNode(string name)
        {
            var node = _nodeStorage.GetWorkerNode(name);

            var query = new CleanNodeQuery(node.GetLocalization(), _localization);
            query.Execute();

            var data = query.RecieveData();

            _nodeStorage.RemoveNode(name);

            var pods = CleanNodeMasterHandler.GetFilePodStructures(data);

            pods.ForEach(p =>
            {
                _fileStorage.UpdatePodeInfo(p.Name, string.Empty, QueryType.RemoveFile);
                AddFile(p.Data, p.PartialPath, p.Name);
            });

            node.SetUsedMemory(0);
            _nodeStorage.AddWorker(node);
        }

        public void ExecuteCommands(string path)
        {
            throw new NotImplementedException();
        }

        public NodeLocalization GetLocalization()
        {
            return _localization; 
        }

        public void RemoveFile(string name)
        {
            var nodes = _nodeStorage.GetAllWorkerNodes();
            var pod = _fileStorage.GetFilePod(name);

            var nodeWithPod = nodes.FirstOrDefault(n => n.GetName().Equals(pod.NodeName));

            if(nodeWithPod == null)
            {
                throw new Exception($"Removing error. Could not find node with name {pod.NodeName}");
            }

            new DeletePodQuery(pod.PartialPath, pod.Name, nodeWithPod.GetLocalization()).Execute();
            _fileStorage.UpdatePodeInfo(name, string.Empty, QueryType.RemoveFile);
            _nodeStorage.UpdateWorkerInfo(nodeWithPod.GetName(), pod.Size, QueryType.RemoveFile);
        }
    }
}

using ObjectStorage.Services.WorkerNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;

namespace ObjectStorage.Services
{
    public class NodeStorage : INodeStorage
    {
        private readonly List<IWorkerNode> _workerNodes;

        public NodeStorage(List<IWorkerNode> workerNodes)
        {
            _workerNodes = workerNodes;
        }

        public NodeStorage()
        {
            _workerNodes = new List<IWorkerNode>();
        }

        public void AddWorker(IWorkerNode worker)
        {
            if(_workerNodes.Any(n => n.GetName().Equals(worker.GetName())))
            {
                throw new Exception($"Worker with name {worker.GetName()} already exists");
            }

            _workerNodes.Add(worker);
        }

        public List<IWorkerNode> GetAllWorkerNodes()
        {
            return _workerNodes;
        }

        public MemoryUsage GetMemoryUsage(string workerName)
        {
           var node = _workerNodes.FirstOrDefault(n => n.GetName().Equals(workerName));
           
            if(node == null)
            {
                throw new Exception($"Worker with name {workerName} does not exist");
            }

            return new MemoryUsage(node.GetUsedMemory(), node.GetTotalMemory());
        }

        public void UpdateWorkerInfo(string workerName, int value, QueryType queryType)
        {
            var node = _workerNodes.FirstOrDefault(n => n.GetName().Equals(workerName));

            if (node == null)
            {
                throw new Exception($"Worker with name {workerName} does not exist");
            }

            if(queryType.Equals(QueryType.AddFile))
            {
                node.SetUsedMemory(node.GetUsedMemory() + value);
            } else
            {
                node.SetUsedMemory(node.GetUsedMemory() - value);
            }
        }

        public IWorkerNode GetWorkerNode(string workerName)
        {
            var node = _workerNodes.FirstOrDefault(n => n.GetName().Equals(workerName));

            if(node == null)
            {
                throw new Exception($"Can't find worker with name {workerName}");
            }

            return node;
        }

        public void RemoveNode(string workerName)
        {
            var node = _workerNodes.FirstOrDefault(n => n.GetName().Equals(workerName));

            if (node != null)
            {
                _workerNodes.Remove(node);
            }

        }
    }
}

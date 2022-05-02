using ObjectStorage.Services.WorkerNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using WorkerNode.Worker;

namespace ObjectStorage.Services
{
    public interface INodeStorage
    {
        void AddWorker(IWorkerNode worker);
        MemoryUsage GetMemoryUsage(string workerName);
        void UpdateWorkerInfo(string workerName, int value, QueryType queryType);
        List<IWorkerNode> GetAllWorkerNodes();
        IWorkerNode GetWorkerNode(string workerName);
        void RemoveNode(string workerName);
    }
}

using ObjectStorage.Services;
using ObjectStorage.Services.WorkerNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Algorithms
{
    public interface IFindOptimalNodeAlgorithm
    {
        IWorkerNode GetNode(int fileSize, INodeStorage storage);
    }
}

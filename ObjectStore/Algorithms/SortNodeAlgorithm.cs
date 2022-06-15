using ObjectStorage.Services;
using ObjectStorage.Services.WorkerNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Algorithms
{
    public class SortNodeAlgorithm : IFindOptimalNodeAlgorithm
    {
        public IWorkerNode GetNode(int fileSize, INodeStorage storage)
        {
            var workers = storage.GetAllWorkerNodes().ToArray();

            Array.Sort(workers, new NodeComparer());


            for (int i = 0; i < workers.Length; i++)
            {
                if(workers[i].GetFreeMemory() > fileSize)
                {
                    return workers[i];
                }
            }

            throw new Exception("Unable to save file: No memory enough");
        }
    }
}

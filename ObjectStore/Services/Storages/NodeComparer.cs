using ObjectStorage.Services.WorkerNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Services
{
    public class NodeComparer : IComparer<IWorkerNode>
    {
        public int Compare(IWorkerNode? m1, IWorkerNode? m2)
        {
            if (m1 is null || m2 is null)
                throw new ArgumentException("Incorrect parameter");
            return (int)(m1.GetUsedMemory() -  m2.GetUsedMemory() * 1.0 / m2.GetTotalMemory() * m1.GetTotalMemory());
        }
    }
}

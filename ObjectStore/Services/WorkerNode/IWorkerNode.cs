using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Tools;

namespace ObjectStorage.Services.WorkerNode
{
    public interface IWorkerNode
    {
        NodeLocalization GetLocalization();
        string GetName();
        int GetTotalMemory();
        int GetUsedMemory();
        int GetFreeMemory();
        void SetUsedMemory(int usedMemory);

    }
}

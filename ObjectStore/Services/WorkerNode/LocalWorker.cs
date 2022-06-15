using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Tools;

namespace ObjectStorage.Services.WorkerNode
{
    public class LocalWorker : IWorkerNode
    {
        public LocalWorker(NodeLocalization localization, string name, int totalMemory)
        {
            Localization = localization ?? throw new ArgumentNullException(nameof(localization));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TotalMemory = totalMemory;
        }

        public NodeLocalization Localization { get; }
        public string Name { get; }
        public int TotalMemory { get; }
        public int UsedMemory { get; private set; } = 0;

        public int GetFreeMemory()
        {
            return TotalMemory - UsedMemory;
        }

        public NodeLocalization GetLocalization()
        {
            return Localization;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetTotalMemory()
        {
            return TotalMemory;
        }

        public int GetUsedMemory()
        {
            return UsedMemory;
        }

        public void SetUsedMemory(int usedMemory)
        {
            if(usedMemory > TotalMemory)
            {
                throw new Exception("Memory out of limit");
            }

            UsedMemory = usedMemory;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Services
{
    public class MemoryUsage
    {
        public MemoryUsage(int usedMemory, int totalMemory)
        {
            UsedMemory = usedMemory;
            TotalMemory = totalMemory;
        }

        public MemoryUsage(int totalMemory)
        {
            UsedMemory = 0;
            TotalMemory = totalMemory;
        }

        public int UsedMemory { get; }
        public int TotalMemory { get; }

        public int CompareTo(MemoryUsage? memoryUsage)
        {
            if (memoryUsage == null)
            {
                return 0;
            }

            return (UsedMemory * memoryUsage.TotalMemory).CompareTo(TotalMemory * memoryUsage.UsedMemory);
        }
    }
}

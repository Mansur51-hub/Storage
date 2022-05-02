using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Tools
{
    public class FilePodStructure
    {
        public FilePodStructure(string name, string partialPath, byte[] data)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PartialPath = partialPath ?? throw new ArgumentNullException(nameof(partialPath));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public string Name { get; private set; }
        public string PartialPath { get; private set; }
        public byte[] Data { get; }
    }
}

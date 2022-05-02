using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Services.Storages
{
    public class FilePod
    {
        public FilePod(string name, string partialPath, string nodeName, int size)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PartialPath = partialPath ?? throw new ArgumentNullException(nameof(partialPath));
            NodeName = nodeName ?? throw new ArgumentNullException(nameof(nodeName));
            Size = size;
        }

        public string Name { get; private set; }
        public string PartialPath { get; private set; }
        public string NodeName { get; private set; }
        public int Size { get; }

        public void ChangePartialPath(string newPath)
        {
            if (newPath == null)
            {
                throw new ArgumentNullException(nameof(newPath));
            }

            PartialPath = newPath;
        }

        public void ChangeNodeName(string newNodeName)
        {
            if(newNodeName == null)
            {
                throw new ArgumentNullException(nameof(newNodeName));
            }

            NodeName = newNodeName;
        }
    }
}

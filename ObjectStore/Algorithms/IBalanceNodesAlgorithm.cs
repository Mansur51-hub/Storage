using ObjectStorage.MasterNode;
using ObjectStorage.Services;
using ObjectStorage.Services.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Algorithms
{
    public interface IBalanceNodesAlgorithm
    {
        void Execute(INodeStorage nodeStorage, IFileStorage fileStorage, IMaster master);
    }
}

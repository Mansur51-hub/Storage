using ObjectStorage.MasterNode;
using ObjectStorage.Services;
using ObjectStorage.Services.Storages;

namespace ObjectStorage.Algorithms
{
    public interface IBalanceNodesAlgorithm
    {
        void Execute(INodeStorage nodeStorage, IFileStorage fileStorage, IMaster master);
    }
}

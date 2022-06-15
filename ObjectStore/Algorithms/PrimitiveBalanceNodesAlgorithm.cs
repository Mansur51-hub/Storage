using ObjectStorage.MasterNode;
using ObjectStorage.QueryConstructor;
using ObjectStorage.Services;
using ObjectStorage.Services.Storages;
using ObjectStorage.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Algorithms
{
    public class PrimitiveBalanceNodesAlgorithm : IBalanceNodesAlgorithm
    {
        public void Execute(INodeStorage nodeStorage, IFileStorage fileStorage, IMaster master)
        {
            var files = fileStorage.GetAllPods();
            List<FilePodStructure> pods = new();

            files.ForEach(f =>
            {
                var node = nodeStorage.GetWorkerNode(f.NodeName);
                var getQuery = new GetPodQuery(f.PartialPath, f.Name, node.GetLocalization(), master.GetLocalization());
                getQuery.Execute();
                var data = getQuery.RecieveData();

                pods.Add(new FilePodStructure(f.Name, f.PartialPath, data));
            });
             

            pods.ForEach(pod =>
            {
                master.RemoveFile(pod.Name);
                master.AddFile(pod.Data, pod.PartialPath, pod.Name);
            });
        }
    }
}

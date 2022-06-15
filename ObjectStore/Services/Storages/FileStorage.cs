using ObjectStorage.Services.WorkerNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;

namespace ObjectStorage.Services.Storages
{
    public class FileStorage : IFileStorage
    {
        private readonly List<FilePod> _pods;

        public FileStorage()
        {
            _pods = new List<FilePod>();
        }

        public void AddFilePod(FilePod pod)
        {
            if(_pods.Any(p => p.Name.Equals(pod.Name)))
            {
                throw new Exception($"File with name {pod.Name} already exists");
            }

            _pods.Add(pod);
        }

        public List<FilePod> GetAllPods()
        {
            return _pods;
        }

        public FilePod GetFilePod(string fileName)
        {
            var pod = _pods.FirstOrDefault(p => p.Name.Equals(fileName));


            if(pod == null)
            {
                throw new Exception($"Pod with name {fileName} does not exist");
            }

            return pod;
        }

        public string GetNodeName(string fileName)
        {
            var pod = _pods.FirstOrDefault(p => p.Name.Equals(fileName));
            
            if(pod == null)
            {
                throw new Exception($"File with name {fileName} does not exist");
            }

            return pod.NodeName;
        }

        public string GetPartialPath(string fileName)
        {
            var pod = _pods.FirstOrDefault(p => p.Name.Equals(fileName));

            if (pod == null)
            {
                throw new Exception($"File with name {fileName} does not exist");
            }

            return pod.PartialPath;
        }

        public void UpdatePodeInfo(string fileName, string newNodeName, QueryType queryType)
        {
            var pod = _pods.FirstOrDefault(p => p.Name.Equals(fileName));

            if(pod == null)
            {
                throw new Exception($"File with name {fileName} does not exist");
            }

            if(queryType.Equals(QueryType.AddFile))
            {
                pod.ChangeNodeName(newNodeName);
            } else
            {
                _pods.Remove(pod);
            }

        }
    }
}

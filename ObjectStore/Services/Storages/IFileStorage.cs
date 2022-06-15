using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;

namespace ObjectStorage.Services.Storages
{
    public interface IFileStorage
    {
        string GetPartialPath(string fileName);
        string GetNodeName(string fileName);
        void AddFilePod(FilePod pod);
        void UpdatePodeInfo(string fileName, string newNodeName, QueryType queryType);
        FilePod GetFilePod(string fileName);
        List<FilePod> GetAllPods();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common;
using WorkerNode.Worker;

namespace WorkerNode.Handlers
{
    public interface IHandler
    {
        int GetQueryType();
        void Handle(byte[] data, IWorker worker);
    }
}

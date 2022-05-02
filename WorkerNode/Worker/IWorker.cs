using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common.Loggers;
using TcpConnector.Tools;

namespace WorkerNode.Worker
{
    public interface IWorker
    {
        string GetName();
        NodeLocalization GetNodeLocalization();
        string GetWorkerPath();
        ILogger GetLogger();
    }
}

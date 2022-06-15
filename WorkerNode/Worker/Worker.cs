using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common.Loggers;
using TcpConnector.Tools;

namespace WorkerNode.Worker
{
    public class Worker : IWorker
    {
        private readonly string _workerPath;
        public Worker(NodeLocalization nodeLocalization, string name, string path, ILogger logger)
        {
            Localization = nodeLocalization ?? throw new ArgumentNullException(nameof(nodeLocalization));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _workerPath = path ?? throw new ArgumentNullException(nameof(path));

            var dirInfo = new DirectoryInfo(_workerPath);
            if(!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            Logger.CreateNewMessage($"Worker named {name} started with args: {nodeLocalization.Ip}, {nodeLocalization.Port}");
        }

        public string GetName()
        {
            return Name;
        }

        public NodeLocalization GetNodeLocalization()
        {
            return Localization;
        }

        public string GetWorkerPath()
        {
            return _workerPath;
        }

        public ILogger GetLogger()
        {
            return Logger;
        }

        public NodeLocalization Localization { get; }
        public string Name { get; }
        public ILogger Logger { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Tools;

namespace ObjectStorage.Tools
{
    public class LocalWorkerNodeStarter
    {
        public LocalWorkerNodeStarter(NodeLocalization nodeLocalization, string nodeName)
        {
            Localization  = nodeLocalization ?? throw new ArgumentNullException(nameof(nodeLocalization));
            NodeName = nodeName ?? throw new ArgumentNullException(nameof(nodeName));
        }

        public NodeLocalization Localization { get; }
        public string NodeName { get; }

        public void Start()
        {
            const string exePath = @"WorkerNode.exe";
            ProcessStartInfo startInfo = new(exePath)
            {
                Arguments = Localization.Ip + " " + Localization.Port + " " + NodeName
            };

            Process.Start(startInfo);
        }

    }
}

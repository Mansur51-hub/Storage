using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Common.Loggers;
using TcpConnector.Common.Loggers.Configurations;
using TcpConnector.Tools;
using WorkerNode.Handlers;

namespace WorkerNode.Worker
{
    public class WorkerStarter
    {
        private readonly NodeLocalization _localization;
        private readonly IWorker _worker;
        private readonly string _workerPath = @$"C:\Users\Mansur51\Desktop\Worker-{Guid.NewGuid()}";
        public WorkerStarter(string[] args)
        {
            const int argsCount = 3;
            if(args.Length != argsCount)
            {
                throw new ArgumentException($"Invalid number of arguments. Required: {argsCount}");
            }

            _localization = new NodeLocalization(args[0], int.Parse(args[1]));

            var curWorkerPath = _workerPath + $"-{args[2]}";
            _worker = new Worker(_localization, args[2],
                 curWorkerPath, new FileLogger(new LoggerTimeConfiguration(), curWorkerPath + @"\logger.txt"));
        }

        public void Start()
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse(_localization.Ip);
                var server = new TcpListener(localAddr, _localization.Port);
                server.Start();

                byte[] data = new byte[256];

                while (true)
                {
                    var client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    MemoryStream memoryStream = new();
                    do
                    {
                        int bytes = stream.Read(data, 0, data.Length);
                        memoryStream.Write(data, 0, bytes);
                    }
                    while (stream.DataAvailable);

         
                    new QueryHandler(memoryStream.ToArray(), _worker, stream).Execute();
                    
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
                //_worker.GetLogger().CreateNewMessage(e.Message);
            }
        }
    }
}

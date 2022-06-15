using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WorkerNode.Worker;

namespace WorkerNode.Handlers
{
    public class QueryHandler
    {
        private readonly byte[] _data;
        private readonly IWorker _worker;
        private List<IHandler> _handlers = new();
        private readonly NetworkStream _stream;

        public QueryHandler(byte[] data, IWorker worker, NetworkStream stream)
        {
            _stream = stream;
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
            _worker.GetLogger().CreateNewMessage("Data recieved by handler");
            SetUp();
        }

        public void Execute()
        {
            try
            {
                _handlers.First(h => h.GetQueryType().Equals(_data[0])).Handle(_data, _worker);
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SetUp()
        {
            _handlers = new()
            {
                new CreateQueryHandler(),
                new DeleteQueryHandler(),
                new CleanNodeHandler(_stream),
                new GetQueryHandler(_stream)
            };
    }
    }
}

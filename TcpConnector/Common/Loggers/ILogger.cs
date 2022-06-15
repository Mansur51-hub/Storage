using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.Common.Loggers
{
    public interface ILogger
    {
        IReadOnlyList<string> GetMessages();
        void CreateNewMessage(string message);
    }
}

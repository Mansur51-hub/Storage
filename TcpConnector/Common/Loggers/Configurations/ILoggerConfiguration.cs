using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.Common.Loggers.Configurations
{
    public interface ILoggerConfiguration
    {
        string GetPrefix();
    }
}

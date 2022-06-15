using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.Common.Loggers.Configurations
{
    public class LoggerTimeConfiguration : ILoggerConfiguration
    {
        private readonly string _prefix;

        public LoggerTimeConfiguration()
        {
            _prefix = $"[{DateTime.Now:G}]";
        }

        public string GetPrefix()
        {
            return _prefix;
        }
    }
}

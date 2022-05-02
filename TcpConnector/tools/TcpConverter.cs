using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.tools
{
    public class TcpConverter
    {
        public static byte[] GetBytes(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

        public static string GetString(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}

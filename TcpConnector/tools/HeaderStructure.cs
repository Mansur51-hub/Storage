using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.tools
{
    public class HeaderStructure
    {
        public HeaderStructure(int length, byte[] data)
        {
            Length = length;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public int Length {  get; set; }
        public byte[] Data { get; set; }
    }
}

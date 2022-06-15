using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Tools;
using UI.Tools;

namespace UI.Options
{
    public class NodeLocalizationOption
    {
        public static string GetDescription()
        {
            string description = GetIpDescription() + GetPortDescription();

            return description;
        }

        private static string GetPortDescription()
        {
            return DescriptionConstructor.GetDescription("port", "Node port", true, typeof(int).Name);
        }

        private static string GetIpDescription()
        { 
            return DescriptionConstructor.GetDescription("ip", "Node ip", true, typeof(string).Name);
        }

        public static NodeLocalization GetObject(string ip, string port)
        {
            return new NodeLocalization(ip, int.Parse(port));
        }
    }
}

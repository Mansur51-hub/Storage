using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpConnector.Tools;

namespace TcpConnector.tools
{
    public class TcpSender
    {

        public static NetworkStream SendData(byte[] data, NodeLocalization localization)
        {
            try
            {
                TcpClient tcpClient = new();
                tcpClient.Connect(localization.Ip, localization.Port);

                NetworkStream stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);

                return stream;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static byte[] RecieveData(NetworkStream stream)
        {
           
            try
            {
                    byte[] recievedData = new byte[256];

                    MemoryStream memoryStream = new();
                    do
                    {
                        int bytes = stream.Read(recievedData, 0, recievedData.Length);
                        memoryStream.Write(recievedData, 0, bytes);
                    }
                    while (stream.DataAvailable);

                    stream.Close();
                    return memoryStream.ToArray();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpConnector.tools
{
    public class Packet
    {
        public Packet(byte queryType,  List<HeaderStructure> headers, byte[] data)
        {
            QueryType = queryType;
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            HeadersCount = (byte)Headers.Count;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public Packet(byte[] packetData)
        {
            if(packetData == null) throw new ArgumentNullException(nameof(packetData));

            QueryType = packetData[0];
            HeadersCount = packetData[1];

            var headers = new List<HeaderStructure>();

            int currentIndex = 2;

            for(int i = 0; i < HeadersCount; i++)
            {
                byte[] lengthBytes = new byte[20];

                for(int j = 0; j < 20; j++)
                {
                    lengthBytes[j] = packetData[currentIndex++];
                }

                int length = int.Parse(TcpConverter.GetString(lengthBytes));

                byte[] headerData = new byte[length];

                for(int j = 0; j < length; j++)
                {
                    headerData[j] = packetData[currentIndex++];
                }

                headers.Add(new HeaderStructure(length, headerData));
            }

            Headers = headers;
            
            int dataLength = packetData.Length - currentIndex;

            Data = new byte[dataLength];

            for(int i = 0; i < dataLength; i++)
            {
                Data[i] = packetData[currentIndex++];
            }

        }

        public byte QueryType { get; }
        public byte HeadersCount { get; }
        public List<HeaderStructure> Headers { get; }
        public byte[] Data { get; }

        public byte[] GetDataToSend()
        {
            var packet = new MemoryStream();

            packet.Write(new byte[] { QueryType, HeadersCount }, 0, 2);

            Headers.ForEach(header =>
            {
                packet.Write(TcpConverter.GetBytes(header.Length.ToString("00000000000000000000")), 0, 20);
                packet.Write(header.Data, 0, header.Length);
            });

            packet.Write(Data, 0, Data.Length);

            return packet.ToArray();
        }

    }
}

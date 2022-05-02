// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using TcpConnector.tools;

namespace TcpConnector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            args.ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
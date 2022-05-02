using ObjectStorage.MasterNode;
using UI;

namespace ObjectStorage
{
    public class Program
    {
        static void Main()
        {
            //Starter.Execute();

            var master = new Master();
            master.AddNode("n1", new TcpConnector.Tools.NodeLocalization("127.0.0.1", 8081), 1000);
            master.AddNode("n2", new TcpConnector.Tools.NodeLocalization("127.0.0.1", 8082), 1000);
            master.AddFile(@"C:\Users\Mansur51\Desktop\check.txt", "tmp", "pod1");


            for (int i = 0; i < 100000; i++)
            {
                try
                {
                    int node = (i % 2 == 0 ? 1 : 2);
                    master.CleanNode($"n{node}");
                } catch(Exception e)
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("wow");
        }
    }
}


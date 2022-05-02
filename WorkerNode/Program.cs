using WorkerNode.Worker;

namespace WorkerNode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WorkerStarter(args).Start();
        }
    }
}
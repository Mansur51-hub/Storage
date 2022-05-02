using TcpConnector.Tools;

namespace ObjectStorage.MasterNode
{
    public interface IMaster
    {
        void AddNode(string name, NodeLocalization nodeLocalization, int totalMemory);
        void AddFile(string path, string partialPath, string name);
        void AddFile(byte[] data, string partialPath, string name);
        void RemoveFile(string name);
        void ExecuteCommands(string path);
        void CleanNode(string name);
        void BalanceNodes();
        NodeLocalization GetLocalization();
    }
}

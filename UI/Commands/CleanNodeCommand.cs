using ObjectStorage.MasterNode;
using UI.Tools;

namespace UI.Commands
{
    public class CleanNodeCommand : ICommand
    {
        public void Execute(string command, IMaster master)
        {
            try
            {
                var args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var name = args[0];

                master.CleanNode(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetDescription()
        {
            string description = "Cleaning node. Required parameters:\n<name>\n"
               + DescriptionConstructor.GetDescription("name", "Node name", true, typeof(string).Name);

            return description;
        }

        public string GetName()
        {
            const string name = "/clean-node";

            return name;
        }
    }
}

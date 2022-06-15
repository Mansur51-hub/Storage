using ObjectStorage.MasterNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Options;
using UI.Tools;

namespace UI.Commands
{
    public class AddNodeCommand : ICommand
    {
        private NodeLocalizationOption _option;

        public AddNodeCommand()
        {
            _option = new NodeLocalizationOption();
        }

        public void Execute(string command, IMaster master)
        {
            var args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var name = args[1];
                var localization = NodeLocalizationOption.GetObject(args[2], args[3]);
                int maxSize = int.Parse(args[4]);
                
                master.AddNode(name, localization, maxSize);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetDescription()
        {
            string description = "Adding new node to storage. Required parameters:\n<name> <ip> <port> <maxSize>\n"
                + DescriptionConstructor.GetDescription("name", "Node name", true, typeof(string).Name)
                + NodeLocalizationOption.GetDescription() +
                DescriptionConstructor.GetDescription("maxSize","Node capacity (bytes)", true, typeof(int).Name);

            return description;
        }

        public string GetName()
        {
            const string name = "/add-node";
            return name;
        }
    }
}

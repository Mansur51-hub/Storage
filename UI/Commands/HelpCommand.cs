using ObjectStorage.MasterNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Tools;

namespace UI.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly List<ICommand> _commands;

        public HelpCommand(List<ICommand> commands)
        {
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public void Execute(string command, IMaster master)
        {
            PrintLine.Print();

            _commands.ForEach(c =>
            {
                Console.WriteLine(c.GetName() + " | " + c.GetDescription());
                PrintLine.Print();
            });
        }

        public string GetDescription()
        {
            const string description = "Show all commands";

            return description;
        }

        public string GetName()
        {
            const string name = "help";
            return name;
        }
    }
}

using ObjectStorage.MasterNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Commands;

namespace UI.Controllers
{
    public class Controller : IController
    {
        private readonly List<ICommand> _commands = new();
        public void Execute(IMaster master)
        {
            SetUp();
            
            while(true)
            {
                string command = Console.ReadLine();
                if (command is { Length: 0 } || command == null)
                {
                    continue;
                }

                ICommand requiredCommand = FindCommand(command);
                if (requiredCommand == null)
                {
                    Console.WriteLine($"Could not find a command with name {command}. Type Help to see all commands");
                    continue;
                }

                requiredCommand.Execute(command, master);
            }
        }

        private void SetUp()
        {
            _commands.Add(new HelpCommand(_commands));
            _commands.Add(new AddNodeCommand());
            _commands.Add(new AddFileCommand());
            _commands.Add(new RemoveFileCommand());
            _commands.Add(new CleanNodeCommand());
            _commands.Add(new BalanceNodesCommand());
            _commands.Add(new ExecuteCommandsCommand(this));
        }

        public ICommand FindCommand(string command)
        {
            var commandName = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).First();

            return _commands.FirstOrDefault(p => p.GetName().Equals(commandName));
        }
    }
}

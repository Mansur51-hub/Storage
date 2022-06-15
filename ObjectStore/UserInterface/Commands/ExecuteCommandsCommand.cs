using ObjectStorage.MasterNode;
using UI.Controllers;
using UI.Tools;
using ObjectStorage.UserInterface.Tools;

namespace UI.Commands
{
    public class ExecuteCommandsCommand : ICommand
    {
        private readonly IController _controller;

        public ExecuteCommandsCommand(IController controller)
        {
            _controller = controller;
        }

        public void Execute(string command, IMaster master)
        {
            try
            {
                var args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var path = args[1];

                var lines = File.ReadAllLines(path);

                foreach (var line in lines)
                {
                    if (line.Length > 0)
                    {
                        ICommand requiredCommand = _controller.FindCommand(line);
                        if (requiredCommand == null)
                        {
                            Console.WriteLine($"Could not find a command with name {command}. Type Help to see all commands");
                            continue;
                        }

                        requiredCommand.Execute(line, master);
                    }
                }

                Response.PrintResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetDescription()
        {
            string description = "Executing commands from .txt file. Required parameters:\n<path>\n"
               + DescriptionConstructor.GetDescription("path", "File path", true, typeof(string).Name);

            return description;
        }

        public string GetName()
        {
            const string name = "/exec";

            return name;
        }
    }
}

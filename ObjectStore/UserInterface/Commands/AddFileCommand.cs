using ObjectStorage.MasterNode;
using ObjectStorage.UserInterface.Tools;
using UI.Options;
using UI.Tools;

namespace UI.Commands
{
    public class AddFileCommand : ICommand
    {
        public void Execute(string command, IMaster master)
        {
            try
            {
                var args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var name = args[1];
                var path = args[2];
                var partialPath = args[3];

                master.AddFile(path, partialPath, name);

                Response.PrintResponse();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetDescription()
        {
            string description = "Adding new file to storage. Required parameters:\n<name> <path> <partial-path>\n"
               + DescriptionConstructor.GetDescription("name", "File name", true, typeof(string).Name)
               + DescriptionConstructor.GetDescription("path", "File current path", true, typeof(string).Name)
               + DescriptionConstructor.GetDescription("partial-path", "Partial path in node", true, typeof(string).Name);

            return description;
        }

        public string GetName()
        {
            const string name = "/add-file";

            return name;
        }
    }
}

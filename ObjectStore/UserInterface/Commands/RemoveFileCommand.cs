using ObjectStorage.MasterNode;
using UI.Tools;
using ObjectStorage.UserInterface.Tools;

namespace UI.Commands
{
    public class RemoveFileCommand : ICommand
    {
        public void Execute(string command, IMaster master)
        {
            try
            {
                var args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var name = args[1];

                master.RemoveFile(name);

                Response.PrintResponse();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetDescription()
        {
            string description = "Removing file from storage. Required parameters:\n<name>\n"
               + DescriptionConstructor.GetDescription("name", "File name", true, typeof(string).Name);

            return description;
        }

        public string GetName()
        {
            const string name = "/remove-file";

            return name;
        }
    }
}

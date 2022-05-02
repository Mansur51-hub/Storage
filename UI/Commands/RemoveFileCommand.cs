using ObjectStorage.MasterNode;
using UI.Tools;

namespace UI.Commands
{
    public class RemoveFileCommand : ICommand
    {
        public void Execute(string command, IMaster master)
        {
            try
            {
                var args = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var name = args[0];

                master.RemoveFile(name);
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

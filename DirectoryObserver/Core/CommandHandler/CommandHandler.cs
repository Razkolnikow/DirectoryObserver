using System.Text;
using Core.CommandHandler.Contracts;
using Core.DirectoryOvserver.Contracts;

namespace Core.CommandHandler
{
    public class CommandHandler : ICommandhandler
    {
        private IDirectoryObserver directoryObserver;

        public CommandHandler(IDirectoryObserver directoryObserver)
        {
            this.directoryObserver = directoryObserver;
        }

        public string ConfigFile { get; set; }

        public string HandleCommand(string command)
        {
            if (command.StartsWith("Start"))
            {
                string id = command.Split(' ')[1];
                int idNumber = this.GetNumberFromString(id);
                this.directoryObserver.Watchers[idNumber - 1].EnableRaisingEvents = true;

                return $"Configuration {id} started";
            }
            else if (command.StartsWith("Stop"))
            {
                string id = command.Split(' ')[1];
                int idNumber = this.GetNumberFromString(id);
                this.directoryObserver.Watchers[idNumber - 1].EnableRaisingEvents = false;

                return $"Configuration {id} stopped";
            }
            else if (command.StartsWith("List"))
            {
                var watchersInfo = new StringBuilder();
                for (int i = 0; i < this.directoryObserver.JsonInfo.configItems.Length; i++)
                {
                    string isActive = this.directoryObserver.Watchers[i].EnableRaisingEvents ? "Active" : "Inactive";
                    watchersInfo
                        .AppendLine(
                        $"{this.directoryObserver.JsonInfo.configItems[i].ID} configuration is {isActive}");
                }

                return watchersInfo.ToString();
            }
            else
            {
                return "No such command!";
            }
        }

        private int GetNumberFromString(string id)
        {
            int number = 0;
            var builder = new StringBuilder();
            for (int i = id.Length - 1; i >= 0; i--)
            {
                if (char.IsNumber(id[i]))
                {
                    builder.Insert(0, id[i]);
                }
                else
                {
                    break;
                }
            }

            number = int.Parse(builder.ToString());
            return number;
        }
    }
}

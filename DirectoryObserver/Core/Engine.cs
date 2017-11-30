using System;
using System.Threading.Tasks;
using Core.CommandHandler.Contracts;
using Core.DirectoryOvserver.Contracts;
using Core.IO.Contracts;

namespace Core
{
    public class Engine
    {
        private IWriter writer;
        private IReader reader;
        private IDirectoryObserver directoryObserver;
        private ICommandhandler commandhandler;

        public Engine(IWriter writer, IReader reader, IDirectoryObserver directoryObserver, ICommandhandler commandhandler)
        {
            this.writer = writer;
            this.reader = reader;
            this.directoryObserver = directoryObserver;
            this.commandhandler = commandhandler;
        }

        public virtual void Run()
        {
            Console.WriteLine("Enter directory path for config.json file: ");
            string path = Console.ReadLine();
            var task = this.directoryObserver.ObserveDirectories(path);

            var task2 = Task.Run(() =>
            {
                this.writer.Write("Welcome to the DirectoryObserver!");
                this.writer.Write("Please enter a command: ");
                string enteredCommand = this.reader.Read();

                while (enteredCommand != "Exit")
                {
                    try
                    {
                        string response = this.commandhandler.HandleCommand(enteredCommand);
                        this.writer.Write(response);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    enteredCommand = this.reader.Read();
                }
            });

            Task.WaitAll(task, task2);
        }
    }
}

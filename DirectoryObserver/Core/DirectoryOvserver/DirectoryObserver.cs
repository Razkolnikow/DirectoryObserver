using System;
using System.IO;
using System.Threading.Tasks;
using Core.DirectoryOvserver.Contracts;
using Core.FileManipulator.Contracts;
using Core.FileReader.Contracts;
using Core.IO;
using Core.IO.Contracts;
using Core.JsonConverter.Contracts;
using Core.Models;
using System.Threading;

namespace Core.DirectoryOvserver
{
    public class DirectoryObserver : IDirectoryObserver
    {
        private IFileReader fileReader;
        private IJsonConverter<ConfigInfo> jsonConverter;
        private IFileManipulator fileManipulator;

        public DirectoryObserver(
            IFileReader fileReader, 
            IJsonConverter<ConfigInfo> jsonConverter,
            IFileManipulator fileManipulator)
        {
            this.fileReader = fileReader;
            this.jsonConverter = jsonConverter;
            this.fileManipulator = fileManipulator;
        }

        public string ConfigFile { get; set; }

        public FileSystemWatcher[] Watchers { get; private set; }

        public ConfigInfo JsonInfo { get; set; }

        public async Task ObserveDirectories(string path)
        {
            Func<Task> action = async () =>
            {
                IWriter writer = new ConsoleWriter();

                // var configFile = this.fileReader.ReadFile("..\\..\\Configuration\\config.json");
                var configFile = this.fileReader.ReadFile(path);
                // writer.Write(configFile);

                this.JsonInfo = this.jsonConverter.DeserializeJson(configFile);
                int watchersCount = this.JsonInfo.configItems.Length;
                this.Watchers = new FileSystemWatcher[watchersCount];

                for (int i = 0; i < watchersCount; i++)
                {
                    this.Watchers[i] = new FileSystemWatcher();
                }

                int index = 0;

                foreach (var item in this.JsonInfo.configItems)
                {
                    var watcher = this.Watchers[index];
                    watcher.Path = item.Source;
                    watcher.NotifyFilter = NotifyFilters.LastWrite;
                    watcher.Filter = item.Filter;
                    watcher.IncludeSubdirectories = true;
                    watcher.InternalBufferSize = 19999999;
                   
                    watcher.Changed += async (sender, eventArgs) =>
                    {
                        var fullPath = eventArgs.FullPath;
                        
                        if (this.IsFileReady(fullPath))
                        {
                            var task = Task.Run(() =>
                            {
                                var fileName = eventArgs.Name;
                                if (item.Action == "Move")
                                {
                                    this.fileManipulator.MoveFileTo(item.Source, item.Destination, fileName);
                                    writer.Write("Moved");
                                }
                                else if (item.Action == "Copy")
                                {
                                    this.fileManipulator.CopyFileTo(item.Source, item.Destination, fileName);
                                    writer.Write("Copied");
                                }
                            });
                            
                            await task.ConfigureAwait(false);
                        }
                        
                        await Task.Delay(100);
                    };

                    watcher.EnableRaisingEvents = true;

                    index++;
                }

                await Task.Delay(100);
                // Console.WriteLine(index);
            };

            await Task.Run(() => action());
        }

        private bool IsFileReady(String sFilename)
        {
            try
            {
                using (FileStream inputStream = File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (inputStream.Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

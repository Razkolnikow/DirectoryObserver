using Core;
using Core.CommandHandler;
using Core.CommandHandler.Contracts;
using Core.DirectoryOvserver;
using Core.DirectoryOvserver.Contracts;
using Core.FileManipulator;
using Core.FileManipulator.Contracts;
using Core.FileReader;
using Core.FileReader.Contracts;
using Core.IO;
using Core.IO.Contracts;
using Core.JsonConverter;
using Core.JsonConverter.Contracts;
using Core.Models;

namespace Startup.Configuration
{
    public class ConfigurationService
    {
        /// <summary>
        /// Configures the dependecies and returns Engine instance.
        /// </summary>
        /// <returns>Engine</returns>
        public virtual Engine ConfigureDependencies()
        {
            IWriter writer = new ConsoleWriter();
            IReader reader = new ConsoleReader();
            IFileManipulator fileManipulator = new FileManipulator();
            IJsonConverter<ConfigInfo> jsonConverter = new JsonConverter<ConfigInfo>();
            IFileReader fileReader = new FileReader();
            IDirectoryObserver directoryObserver = new DirectoryObserver(fileReader, jsonConverter, fileManipulator);
            ICommandhandler commandhandler = new CommandHandler(directoryObserver);
            Engine engine = new Engine(writer, reader, directoryObserver, commandhandler);
            return engine;
        }
    }
}

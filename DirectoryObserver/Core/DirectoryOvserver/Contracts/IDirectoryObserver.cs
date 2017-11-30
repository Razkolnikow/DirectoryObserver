using System.IO;
using System.Threading.Tasks;
using Core.Models;

namespace Core.DirectoryOvserver.Contracts
{
    public interface IDirectoryObserver
    {
        FileSystemWatcher[] Watchers { get; }

        ConfigInfo JsonInfo { get; }

        Task ObserveDirectories(string path);
    }
}

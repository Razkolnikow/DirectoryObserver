using System;

namespace Core.FileReader.Contracts
{
    public interface IFileReader
    {
        string ReadFile(string path);
    }
}

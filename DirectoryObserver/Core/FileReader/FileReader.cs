using System.IO;
using Core.FileReader.Contracts;


namespace Core.FileReader
{
    public class FileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}

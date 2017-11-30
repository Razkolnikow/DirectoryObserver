using System.IO;
using Core.FileManipulator.Contracts;

namespace Core.FileManipulator
{
    public class FileManipulator : IFileManipulator
    {
        public void CopyFileTo(string sourcePath, string destinationPath, string fileName)
        {
            var filePath = sourcePath + $"\\{fileName}";
            string fileText = File.ReadAllText(filePath);
            var destinationFilePath = destinationPath + $"\\{fileName}";

            File.Copy(filePath, destinationFilePath, true);
        }

        public void MoveFileTo(string sourcePath, string destinationPath, string fileName)
        {
            var filePath = sourcePath + $"\\{fileName}";
            string fileText = File.ReadAllText(filePath);
            var destinationFilePath = destinationPath + $"\\{fileName}";

            if (! this.CheckIfExists(destinationFilePath))
            {
                File.Move(filePath, destinationFilePath);
            }
            else
            {
                this.ReplaceFile(filePath, destinationFilePath);
            }
        }

        private bool CheckIfExists(string destinationFilePath)
        {
            return File.Exists(destinationFilePath);
        }

        private void ReplaceFile(string sourceFilePath, string destinationFilePath)
        {
            File.Replace(sourceFilePath, destinationFilePath, destinationFilePath + ".bac", false);
        }
    }
}

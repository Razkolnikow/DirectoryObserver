using System;

namespace Core.FileManipulator.Contracts
{
    public interface IFileManipulator
    {
        void CopyFileTo(string sourcePath, string destinationPath, string fileName);

        void MoveFileTo(string sourcePath, string destinationPath, string fileName);
    }
}

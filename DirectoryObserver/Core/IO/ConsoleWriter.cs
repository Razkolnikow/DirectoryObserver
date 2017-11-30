using System;
using Core.IO.Contracts;

namespace Core.IO
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}

namespace WildFarm.IO
{
    using System;

    using WildFarm.IO.Interfaces;
    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}

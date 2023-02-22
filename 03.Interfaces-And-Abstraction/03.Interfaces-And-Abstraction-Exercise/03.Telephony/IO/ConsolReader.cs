
namespace Telephony.IO
{
    using System;

    using Interfaces;
    public class ConsolReader : IReader
    {
        public string ReadLine() 
            => Console.ReadLine();
        
    }
}

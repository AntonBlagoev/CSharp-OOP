namespace Telephony.Exeptions
{
    using System;
    public class InvalidUrlExeption : Exception
    {
        private const string DefaultMessage = "Invalid URL!";
       
        public InvalidUrlExeption() : base(DefaultMessage)
        {
        }
        public InvalidUrlExeption(string message) : base(message)
        {
        }
    }
}

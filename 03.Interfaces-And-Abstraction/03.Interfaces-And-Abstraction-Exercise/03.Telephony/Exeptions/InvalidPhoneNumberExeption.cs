namespace Telephony.Exeptions
{
    using System;
    public class InvalidPhoneNumberExeption : Exception
    {
        private const string DefaultExeptionMessage = "Invalid number!";
        public InvalidPhoneNumberExeption() : base(DefaultExeptionMessage)
        {

        }
        public InvalidPhoneNumberExeption(string message) : base(message)
        {

        }

    }
}

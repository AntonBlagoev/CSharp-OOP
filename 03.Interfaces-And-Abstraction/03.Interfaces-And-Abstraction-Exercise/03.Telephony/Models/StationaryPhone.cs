namespace Telephony.Models
{
    using System;
    using System.Linq;

    using Interfaces;
    using Telephony.Exeptions;

    public class StationaryPhone : IStationaryPhone
    {
        public StationaryPhone()
        {
        }
        public string Call(string phoneNumber)
        {
            if (!this.ValidatePhoneNumber(phoneNumber))
            {
                throw new InvalidPhoneNumberExeption();
            }
            return $"Dialing... {phoneNumber}";
        }
        private bool ValidatePhoneNumber(string phoneNumber)
            => phoneNumber.All(ch => char.IsDigit(ch));
         // => !phoneNumber.Any(ch => !char.IsDigit(ch));

    }
}

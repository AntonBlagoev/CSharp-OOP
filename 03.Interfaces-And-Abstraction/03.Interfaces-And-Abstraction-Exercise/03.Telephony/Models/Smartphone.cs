namespace Telephony.Models
{
    using System;
    using System.Linq;

    using Interfaces;
    using Telephony.Exeptions;
    public class Smartphone : ISmartphone
    {
        public Smartphone()
        {
        }

        public string Call(string phoneNumber)
        {
            if (!this.ValidatePhoneNumber(phoneNumber))
            {
                throw new InvalidPhoneNumberExeption();
            }
            return $"Calling... {phoneNumber}";
        }
        public string BrowseUrl(string url)
        {
            if (!this.ValidateUrl(url))
            {
                throw new InvalidUrlExeption();
            }
            return $"Browsing: {url}!";
        }
        private bool ValidatePhoneNumber(string phoneNumber)
            => phoneNumber.All(ch => char.IsDigit(ch));

        private bool ValidateUrl(string url)
            => url.All(ch => !char.IsDigit(ch));
    }
}

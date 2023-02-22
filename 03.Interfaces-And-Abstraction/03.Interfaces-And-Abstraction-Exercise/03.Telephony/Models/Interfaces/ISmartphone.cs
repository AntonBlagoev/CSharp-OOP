namespace Telephony.Models.Interfaces
{
    public interface ISmartphone : IStationaryPhone
    {
        public string BrowseUrl(string url);
    }
}

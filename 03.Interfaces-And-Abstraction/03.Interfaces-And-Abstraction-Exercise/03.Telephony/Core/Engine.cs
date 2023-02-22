namespace Telephony.Core
{
    using System;
    using Interfaces;
    using Telephony.Exeptions;
    using Telephony.IO.Interfaces;
    using Telephony.Models;
    using Telephony.Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private IStationaryPhone stacionaryPhone;
        private ISmartphone smartPhone;

        public Engine()
        {
            this.stacionaryPhone = new StationaryPhone();
            this.smartPhone = new Smartphone();
        }
        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] phoneNumbers = Console.ReadLine().Split(' ');
            string[] urls = Console.ReadLine().Split(' ');

            foreach (string phoneNumber in phoneNumbers)
            {
                try
                {
                    if (phoneNumber.Length == 10)
                    {
                        this.writer.WriteLine(this.smartPhone.Call(phoneNumber));
                    }
                    else if (phoneNumber.Length == 7)
                    {
                        this.writer.WriteLine(this.stacionaryPhone.Call(phoneNumber));
                    }
                    else
                    {
                        throw new InvalidPhoneNumberExeption();
                    }

                }
                catch (InvalidPhoneNumberExeption ipne)
                {
                    this.writer.WriteLine(ipne.Message);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            foreach (string url in urls)
            {
                try
                {
                    this.writer.WriteLine(this.smartPhone.BrowseUrl(url));

                }
                catch (InvalidUrlExeption iue)
                {
                    this.writer.WriteLine(iue.Message);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}


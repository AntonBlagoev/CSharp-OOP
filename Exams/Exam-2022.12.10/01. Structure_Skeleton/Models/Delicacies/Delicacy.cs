namespace ChristmasPastryShop.Models.Delicacies
{
    using System;

    using Delicacies.Contracts;
    using Utilities.Messages;
    public abstract class Delicacy : IDelicacy
    {
        private string name;
        protected Delicacy(double price, string name)
        {
            this.Price = price;
            this.Name = name;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }
                name = value;
            }
        }
        public double Price { get; private set; }

        public override string ToString()
        {
            return $"{this.Name} - {this.Price:F2} lv";
        }

    }
}

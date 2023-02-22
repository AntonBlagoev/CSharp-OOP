namespace ChristmasPastryShop.Models.Cocktails
{
    using System;

    using Cocktails.Contracts;
    using Utilities.Messages;
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private double price;
        protected Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
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
        public string Size { get; private set; }

        public double Price
        {
            get => price;
            private set
            {
                price = value;
                if (this.Size == "Middle")
                {
                    price = (value / 3) * 2;
                }
                else if (this.Size == "Small")
                {
                    price = value / 3;
                }
            }
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Size}) - {this.Price:F2} lv";
        }
    }
}

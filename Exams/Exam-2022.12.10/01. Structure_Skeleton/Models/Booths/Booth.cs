namespace ChristmasPastryShop.Models.Booths
{
    using System;
    using System.Text;

    using Booths.Contracts;
    using Cocktails.Contracts;
    using Delicacies.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;
    public class Booth : IBooth
    {
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;

        public Booth(int boothId, int capacity)
        {
            this.BoothId = boothId;
            this.Capacity = capacity;

            this.currentBill = 0;
            this.turnover = 0;

            this.delicacyMenu = new DelicacyRepository();
            this.cocktailMenu = new CocktailRepository();
        }

        public int BoothId { get; private set; }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityLessThanOne));
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => this.cocktailMenu;

        public double CurrentBill => currentBill;

        public double Turnover => turnover;

        public bool IsReserved { get; protected set; } = false;

        public void Charge()
        {
            this.turnover += this.CurrentBill;
            this.currentBill = 0;
        }
        public void ChangeStatus()
        {
            this.IsReserved = !this.IsReserved;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.currentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {this.BoothId}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Turnover: {this.Turnover:F2} lv");

            sb.AppendLine($"-Cocktail menu:");
            foreach (var cocktail in cocktailMenu.Models)
            {
                var result = cocktail.ToString();
                sb.AppendLine(result);
            }

            sb.AppendLine($"-Delicacy menu:");
            foreach (var delicacy in delicacyMenu.Models)
            {
                var result = delicacy.ToString();
                sb.AppendLine(result);
            }

            return sb.ToString().TrimEnd();
        }
    }
}

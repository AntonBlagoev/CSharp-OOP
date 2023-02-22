namespace ChristmasPastryShop.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Booths;
    using Models.Cocktails;
    using Models.Cocktails.Contracts;
    using Models.Delicacies;
    using Models.Delicacies.Contracts;
    using Repositories;
    using Utilities.Messages;
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothID = this.booths.Models.Count + 1;
            Booth booth = new Booth(boothID, capacity);
            booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded, boothID, capacity);
        }
        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            var booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            var delicacy = booth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == delicacyName);

            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }
            else if (delicacy != null)
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }
            else
            {
                if (delicacyTypeName == nameof(Gingerbread))
                {
                    IDelicacy model = new Gingerbread(delicacyName);
                    booth.DelicacyMenu.AddModel(model);
                }
                else if (delicacyTypeName == nameof(Stolen))
                {
                    IDelicacy model = new Stolen(delicacyName);
                    booth.DelicacyMenu.AddModel(model);
                }
            }
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            var booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            var cocktail = booth.CocktailMenu.Models.FirstOrDefault(x => x.Name == cocktailName);

            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            else if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            else if (cocktail != null && cocktail.Size == size)
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }
            else
            {
                if (cocktailTypeName == nameof(Hibernation))
                {
                    ICocktail model = new Hibernation(cocktailName, size);
                    booth.CocktailMenu.AddModel(model);
                }
                else if (cocktailTypeName == nameof(MulledWine))
                {
                    ICocktail model = new MulledWine(cocktailName, size);
                    booth.CocktailMenu.AddModel(model);
                }
            }
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            var orderedBooth = booths.Models.Where(x => x.IsReserved == false).OrderBy(x => x.Capacity).ThenByDescending(x => x.BoothId);
            var serchedBooth = orderedBooth.FirstOrDefault(x => x.Capacity >= countOfPeople);

            if (serchedBooth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            else
            {
                serchedBooth.ChangeStatus();
            }
            return string.Format(OutputMessages.BoothReservedSuccessfully, serchedBooth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] currOrder = order.Split('/', StringSplitOptions.RemoveEmptyEntries);

            string itemTypeName = currOrder[0];
            string itemName = currOrder[1];
            int pieces = int.Parse(currOrder[2]);
            string size = string.Empty;
            if (currOrder.Length == 4)
            {
                size = currOrder[3];
            }

            var currBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            ICocktail cocktail = currBooth.CocktailMenu.Models.FirstOrDefault(x => x.Name == itemName);
            IDelicacy delicacy = currBooth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == itemName);

            if (itemTypeName != nameof(Gingerbread) 
                    && itemTypeName != nameof(Stolen) 
                    && itemTypeName != nameof(Hibernation) 
                    && itemTypeName != nameof(MulledWine))
            {
                return $"{itemTypeName} is not recognized type!";
            }
            else if (cocktail == null && delicacy == null)
            {
                return $"There is no {itemTypeName} {itemName} available!";
            }
            else if (cocktail != null)
            {
                if (cocktail.Name == itemName && cocktail.Size == size)
                {
                    double bill = cocktail.Price * pieces;
                    currBooth.UpdateCurrentBill(bill);
                }
                else
                {
                    return $"There is no {size} {itemName} available!";
                }
            }
            else if (delicacy != null)
            {
                if (delicacy.Name == itemName)
                {
                    double bill = delicacy.Price * pieces;
                    currBooth.UpdateCurrentBill(bill);
                }
                else
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }
            }
            return $"Booth {boothId} ordered {pieces} {itemName}!";
        }

        public string LeaveBooth(int boothId)
        {
            var currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            var currentBill = currentBooth.CurrentBill;

            currentBooth.Charge();
            currentBooth.ChangeStatus();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {currentBill:f2} lv");
            sb.AppendLine($"Booth {boothId} is now available!");
            return sb.ToString().TrimEnd();
        }


        public string BoothReport(int boothId)
        {
            var currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {boothId}");
            sb.AppendLine($"Capacity: {currentBooth.Capacity}");
            sb.AppendLine($"Turnover: {currentBooth.Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");

            foreach (var item in currentBooth.CocktailMenu.Models)
            {
                sb.AppendLine($"--{item.ToString()}");
            }

            sb.AppendLine($"-Delicacy menu:");

            foreach (var item in currentBooth.DelicacyMenu.Models)
            {
                sb.AppendLine($"--{item.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}

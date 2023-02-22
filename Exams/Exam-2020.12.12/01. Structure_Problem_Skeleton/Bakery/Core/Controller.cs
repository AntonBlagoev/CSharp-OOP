namespace Bakery.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.BakedFoods;
    using Models.BakedFoods.Contracts;
    using Models.Drinks;
    using Models.Drinks.Contracts;
    using Models.Tables;
    using Models.Tables.Contracts;
    using Utilities.Messages;
    public class Controller : IController
    {
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;
        private decimal totalIncome = 0;
        public Controller()
        {
            this.bakedFoods = new List<IBakedFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<ITable>();
        }
        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food;

            if (type == nameof(Bread))
            {
                food = new Bread(name, price);
                bakedFoods.Add(food);
                return string.Format(OutputMessages.FoodAdded, name, type);
            }
            else if (type == nameof(Cake))
            {
                food = new Cake(name, price);
                bakedFoods.Add(food);
                return string.Format(OutputMessages.FoodAdded, name, type);
            }
            else
            {
                return null;
            }
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink;

            if (type == nameof(Tea))
            {
                drink = new Tea(name, portion, brand);
                drinks.Add(drink);
                return string.Format(OutputMessages.DrinkAdded, name, brand);
            }
            else if (type == nameof(Water))
            {
                drink = new Water(name, portion, brand);
                drinks.Add(drink);
                return string.Format(OutputMessages.DrinkAdded, name, brand);
            }
            else
            {
                return null;
            }
        }
        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table;

            if (type == nameof(InsideTable))
            {
                table = new InsideTable(tableNumber, capacity);
                tables.Add(table);
                return string.Format(OutputMessages.TableAdded, tableNumber);
            }
            else if (type == nameof(OutsideTable))
            {
                table = new OutsideTable(tableNumber, capacity);
                tables.Add(table);
                return string.Format(OutputMessages.TableAdded, tableNumber);
            }
            else
            {
                return null;
            }
        }
        public string ReserveTable(int numberOfPeople)
        {
            var tableToReserve = tables.FirstOrDefault(x => x.IsReserved == false && x.Capacity >= numberOfPeople);

            if (tableToReserve == null)
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }
            else
            {
                tableToReserve.Reserve(numberOfPeople);
                return string.Format(OutputMessages.TableReserved, tableToReserve.TableNumber, numberOfPeople);
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            var food = bakedFoods.FirstOrDefault(x => x.Name == foodName);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            else if (food == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }
            else
            {
                table.OrderFood(food);
                return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
            }
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            var drink = drinks.FirstOrDefault(x => x.Name == drinkName);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            else if (drink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }
            else
            {
                table.OrderDrink(drink);
                return string.Format($"Table {tableNumber} ordered {drinkName} {drinkBrand}");
            }
        }

        public string LeaveTable(int tableNumber)
        {
            var table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            if (table != null)
            {
                var bill = table.GetBill();
                this.totalIncome += bill;
                table.Clear();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Table: {tableNumber}");
                sb.AppendLine($"Bill: {bill:f2}");

                return sb.ToString().TrimEnd();
            }
            else
            {
                return null;
            }
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var table in tables.Where(x => x.IsReserved == false))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome:f2}lv";
        }
    }
}

namespace FoodShortage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models;
    using Models.Interfaces;
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> buyerlList = new List<IBuyer>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine().Split(' ');
                if (tokens.Length == 4)
                {
                    var name = tokens[0];
                    var age = int.Parse(tokens[1]);
                    var id = tokens[2];
                    var birthdate = tokens[3];
                    IBuyer citizen = new Citizen(name, age, id, birthdate);
                    buyerlList.Add(citizen);
                }
                else if (tokens.Length == 3)
                {
                    var name = tokens[0];
                    var age = int.Parse(tokens[1]);
                    var group = tokens[2];
                    IBuyer rebel = new Rebel(name, age, group);
                    buyerlList.Add(rebel);
                }
            };

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "End")
            {
                var currentBuyer = buyerlList.FirstOrDefault(x => x.Name == command);
                if (currentBuyer != null)
                {
                    currentBuyer.BuyFood();
                }
            }
            var totalFood = buyerlList.Sum(x => x.Food);
            Console.WriteLine(totalFood);

           
        }
    }
}

namespace BirthdayCelebrations
{
    using System;
    using System.Collections.Generic;

    using BirthdayCelebrations.Models;
    using BirthdayCelebrations.Models.Interfaces;
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBirthdays> birthdayCelebrationslList = new List<IBirthdays>();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(' ');
                if (tokens[0] == "Citizen")
                {
                    var name = tokens[1];
                    var age = int.Parse(tokens[2]);
                    var id = tokens[3];
                    var birthdate = tokens[4];
                    IBirthdays citizen = new Citizen(name, age, id, birthdate);
                    birthdayCelebrationslList.Add(citizen);
                }
                else if (tokens[0] == "Pet")
                {
                    var name = tokens[1];
                    var birthdate = tokens[2];
                    IBirthdays pet = new Pet(name, birthdate);
                    birthdayCelebrationslList.Add(pet);
                }
            }

            string command = Console.ReadLine();

            foreach (var item in birthdayCelebrationslList)
            {
                if (item.Birthdate.Contains(command))
                {
                    Console.WriteLine(item.Birthdate);

                }
            }
        }
    }
}

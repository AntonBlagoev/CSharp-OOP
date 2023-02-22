namespace BorderControl
{
    using BorderControl.Models;
    using BorderControl.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBorderControl> borderControlList = new List<IBorderControl>();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(' ');

                if (tokens.Length == 3)
                {
                    var name = tokens[0];
                    var age = int.Parse(tokens[1]);
                    var id = tokens[2];
                    IBorderControl citizen = new Citizen(name, age, id);
                    borderControlList.Add(citizen);
                }
                else if (tokens.Length == 2)
                {
                    var model = tokens[0];
                    var id = tokens[1];
                    IBorderControl robot = new Robot(model, id);
                    borderControlList.Add(robot);
                }
            }

            string command = Console.ReadLine();

            foreach (var item in borderControlList)
            {
                if (item.Id.EndsWith(command))
                {
                    Console.WriteLine(item.Id);

                }
            }
        }
    }
}

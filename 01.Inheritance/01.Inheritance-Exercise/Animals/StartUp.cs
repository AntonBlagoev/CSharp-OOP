using System;
using System.Text;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "Beast!")
            {
                string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string gender = string.Empty;

                if (tokens.Length > 2)
                {
                    gender = tokens[2];
                }

                if (command == "Cat")
                {
                    Cat cat = new Cat(name,age, gender);
                    sb.AppendLine(cat.ToString());
                }
                else if (command == "Dog")
                {
                    Dog dog = new Dog(name, age, gender);
                    sb.AppendLine(dog.ToString());
                }
                else if (command == "Frog")
                {
                    Frog frog = new Frog(name, age, gender);
                    sb.AppendLine(frog.ToString());
                }
                else if (command == "Kitten")
                {
                    Kitten kitten = new Kitten(name, age);
                    sb.AppendLine(kitten.ToString());
                }
                else if (command == "Tomcat")
                {
                    Tomcat tomcat = new Tomcat(name, age);
                    sb.AppendLine(tomcat.ToString());
                }
                else
                {
                    throw new ArgumentException("Invalid input!");
                }
            }

            Console.WriteLine(sb.ToString());
        }
    }
}

using System;
using System.Linq;

namespace _05.PlayCatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxExceptions = 3;
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int countofExepctions = 0;
            while (countofExepctions < maxExceptions)
            {
                string[] command = Console.ReadLine().Split();
                string type = command[0];
                string[] commandparams = command.Skip(1).ToArray();
                try
                {
                    switch (type)
                    {
                        case "Replace":
                            int index = int.Parse(commandparams[0]);
                            int element = int.Parse(commandparams[1]);
                            numbers[index] = element;
                            ; break;
                        case "Print":
                            int firstindex = int.Parse(commandparams[0]);
                            int secondindex = int.Parse(commandparams[1]);
                            int[] newArray = new int[secondindex - firstindex + 1];
                            int count = 0;
                            for (int i = firstindex; i <= secondindex; i++)
                            {
                                newArray[count] = numbers[i];
                                count++;
                            }
                            Console.WriteLine(string.Join(", ", newArray));
                            ; break;
                        case "Show":
                            int indexx = int.Parse(commandparams[0]);
                            Console.WriteLine(numbers[indexx]); break;
                    }
                }
                catch (FormatException)
                {
                    countofExepctions++;
                    Console.WriteLine("The variable is not in the correct format!");

                }
                catch (IndexOutOfRangeException)
                {
                    countofExepctions++;
                    Console.WriteLine("The index does not exist!");
                }
            }
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}

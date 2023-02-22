using System;

namespace _02.EnterNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int start = 1;
            const int end = 100;
            ReadNumber(start, end);
        }
        public static void ReadNumber(int start, int end)
        {
            int counter = 0;
            int currentNumber = start;
            int[] numbers = new int[10];
            while (counter < 10)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    if (input > currentNumber && input < end)
                    {
                        currentNumber = input;
                        numbers[counter] = input;
                        counter++;
                    }
                    else
                    {
                        throw new ArgumentException($"Your number is not in range {currentNumber} - {end}!");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Number!");

                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}

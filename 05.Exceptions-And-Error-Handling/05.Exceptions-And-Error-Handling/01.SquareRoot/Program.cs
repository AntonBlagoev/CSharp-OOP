using System;

namespace _01.SquareRoot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            try
            {
                double result = Math.Sqrt(number);
                if (double.IsNaN(result))
                {
                    throw new ArithmeticException("Invalid number.");
                }
                Console.WriteLine(result);
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}

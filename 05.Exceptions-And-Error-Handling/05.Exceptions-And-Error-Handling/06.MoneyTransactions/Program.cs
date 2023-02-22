using System;
using System.Collections.Generic;

namespace _06.MoneyTransactions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] accountsInfo = Console.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, double> accounts = new Dictionary<int, double>();

            foreach (var account in accountsInfo)
            {
                int number = int.Parse(account.Split('-')[0]);
                double sum = double.Parse(account.Split('-')[1]);

                accounts[number] = sum;
            }

            string cmd;
            while ((cmd = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = cmd.Split(' ');
                try
                {
                    string cmdType = cmdArgs[0];
                    int number = int.Parse(cmdArgs[1]);
                    double sum = double.Parse(cmdArgs[2]);

                    switch (cmdType)
                    {
                        case "Deposit":
                            accounts[number] += sum;
                            break;

                        case "Withdraw":
                            if (accounts[number] < sum)
                            {
                                throw new InvalidOperationException("Insufficient balance!");
                            }

                            accounts[number] -= sum;
                            break;

                        default:
                            throw new InvalidOperationException("Invalid command!");
                    }

                    Console.WriteLine($"Account {number} has new balance: {accounts[number]:f2}");
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Invalid account!");
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }
            }
        }
    }
}

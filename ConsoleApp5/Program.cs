using System;
using System.Globalization;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the target date from user input
            Console.Write("Entrez une date (au format dd/MM/yyyy) entre le 1er Janvier 2022 et le 1er Mars 2023 : ");
            string inputDate = Console.ReadLine();


            // Convert the inputDate to valid DateTime and Get the Account Value
            DateTime targetDate;
            if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out targetDate))
            {
                // Load only Transaction for the given date from Account.csv file
                var transactionManager = new Transaction();
                var transactions = transactionManager.LoadAccountTransactions("account.csv", targetDate);

                //Get the Account Value in the given date
                decimal accountValue = transactionManager.GetAccountValue(transactions);

                
                Console.WriteLine($"La valeur du compte le {inputDate} est de {accountValue} EUR");
            }
            else
            {
                Console.WriteLine("Format de date incorrect.");
            }
        }
    }
}

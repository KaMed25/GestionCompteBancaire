using System;
using System.Globalization;

namespace AccountValue
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
            if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out targetDate) && IsInputDateWithinRange(targetDate))
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
                Console.WriteLine("Format de date incorrect ou ne respecte pas l'interval souhaité.");
            }
        }
        public static bool IsInputDateWithinRange(DateTime targetDate)
        {
            return targetDate >= DateTime.ParseExact("01/01/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture) && targetDate <= DateTime.ParseExact("01/03/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}

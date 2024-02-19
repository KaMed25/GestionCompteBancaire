using System;
using System.Globalization;

namespace AccountValue
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the target date from user input
            Console.Write(" Enter a date (in the format dd/MM/yyyy) between January 1, 2022, and March 1, 2023: ");
            string inputDate = Console.ReadLine();
            var startRangeDate = DateTime.ParseExact("01/01/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endRangeDate= DateTime.ParseExact("01/03/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Convert the inputDate to valid DateTime and Get the Account Value
            DateTime targetDate;
            if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out targetDate) && IsInputDateWithinRange(targetDate, startRangeDate,endRangeDate))
            {
                // Load only Transaction for the given date from Account.csv file
                var exchangeRateProvider = new ExchangeRate();
                var transactionManager = new Transaction(exchangeRateProvider);
                var transactions = transactionManager.LoadAccountTransactions("account.csv", targetDate);

                //Get the Account Value in the given date
                decimal accountValue = transactionManager.GetAccountValue(transactions);

                
                Console.WriteLine($"The account value on {inputDate} is {accountValue} EUR.");
            }
            else
            {
                Console.WriteLine("Incorrect date format or does not comply with the given interval.");
            }
        }
        public static bool IsInputDateWithinRange(DateTime targetDate, DateTime startDate, DateTime endDate)
        {
            return targetDate >= startDate && targetDate <= endDate;
        }
    }
}

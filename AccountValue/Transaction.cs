using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AccountValue
{
    /// <summary>
    /// This class handle transactions and calculate the value of an account
    /// </summary>
    public class Transaction : ITransaction
    {
        public DateTime Date { get; set; }
        public decimal Montant { get; set; }
        public Currency Devise { get; set; }
        public string Category { get; set; }

        private readonly ExchangeRateProvider _exchangeRateProvider;

        public Transaction(ExchangeRateProvider exchangeRateProvider)
        {
            _exchangeRateProvider = exchangeRateProvider;
        }

        public Transaction(DateTime date, decimal montant, Currency devise, string category)
        {
            Date = date;
            Montant = montant;
            Devise = devise;
            Category = category;
        }

        /// <summary>
        /// Load Account Transactions from csv file
        /// </summary>
        /// <param name="filePath">the csv file path</param>
        /// <param name="targetDate">the target date</param>
        /// <returns></returns>
        public List<Transaction> LoadAccountTransactions(string filePath, DateTime targetDate)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines.Skip(4)) // Ignore currency and initial amount lines
                {
                    string[] values = line.Split(';');
                    DateTime date;
                    if (!DateTime.TryParseExact(values[0], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        Console.WriteLine($"Error parsing date: {values[0]}");
                        continue;
                    }
                    
                    decimal montant = decimal.Parse(values[1], CultureInfo.InvariantCulture);
                    if (!decimal.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out montant))
                    {
                        Console.WriteLine($"Error parsing montant: {values[1]}");
                        continue;
                    }
                    Currency devise = Enum.TryParse(values[2], out Currency result)? result : Currency.Default;
                    string category = values[3];
                    
                    if (date <= targetDate)
                    transactions.Add(new Transaction(date, montant, devise, category));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des transactions : {ex.Message}");
            }

            return transactions;
        }

        /// <summary>
        /// Function to get the Value of the Account in a given Date 
        /// </summary>
        /// <param name="targetDate">the given date</param>
        /// <param name="transactions">the transactions list of the account</param>
        /// <returns></returns>
        public decimal GetAccountValue(List<Transaction> transactions)
        {
            
            //var exchangeRateProvider = new ExchangeRate();

            //Initial balance
            decimal accountValue = 8300.00m;

            foreach (Transaction transaction in transactions)
            {
                accountValue += transaction.Montant * this._exchangeRateProvider.GetExchangeRateToEuro(transaction.Devise) ;
            }

            return Math.Round(accountValue,2);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccountValue;
using System.IO;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace AccountValueUnitTest
{
    /// <summary>
    /// test class for transaction functions
    /// </summary>
    [TestClass]
    public class TransactionsUnitTest
    {
        private decimal initialBalance = 8300.00m;
        private string GetTestFilePath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
        [TestMethod]
        public void LoadTransaction_Should_Load_the_excpected_transactions()
        {
            var exchangeRateProvider = new ExchangeRate();
            var transactionManager = new Transaction(exchangeRateProvider);
            string filePath = GetTestFilePath("AccountUnitTest1.csv");
            DateTime targetDate = DateTime.ParseExact("16/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactionManager.LoadAccountTransactions(filePath, targetDate);

            Assert.AreEqual(2, transactions.Count);
        }

        [TestMethod]
        public void LoadTransaction_should_return_empty_transaction_list()
        {
            var exchangeRateProvider = new ExchangeRate();
            var transactionManager = new Transaction(exchangeRateProvider);
            string filePath = GetTestFilePath("InvalidInputFile.csv");
            DateTime targetDate = DateTime.ParseExact("16/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactionManager.LoadAccountTransactions(filePath, targetDate);

            Assert.AreEqual(0,transactions.Count);
        }

        [TestMethod]
        public void LoadTransaction_should_return_exact_transactio_number_when_targetDate_between_transactions_date()
        {
            var exchangeRateProvider = new ExchangeRate();
            var transactionManager = new Transaction(exchangeRateProvider);
            string filePath = GetTestFilePath("AccountUnitTest1.csv");
            DateTime targetDate = DateTime.ParseExact("15/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactionManager.LoadAccountTransactions(filePath, targetDate);

            Assert.AreEqual(1, transactions.Count);
        }

        [TestMethod]
        public void GetAccountValue_should_return_initial_balance_when_transaction_is_null()
        {
            var exchangeRateProvider = new ExchangeRate();
            var transactionManager = new Transaction(exchangeRateProvider);
            var transactions = new List<Transaction>();
            var AccountValue = transactionManager.GetAccountValue(transactions);

            Assert.AreEqual(initialBalance, AccountValue);
        }

        [TestMethod]
        public void GetAccountValue_should_handle_negative_value()
        {
            var exchangeRateProvider = new ExchangeRate();
            var transactionManager = new Transaction(exchangeRateProvider);

            var transactions = new List<Transaction>() { new Transaction(exchangeRateProvider) { Date = DateTime.ParseExact("15/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), Category = "testing", Devise = Currency.EUR, Montant = -8300.00m },
                                                       new Transaction(exchangeRateProvider) { Date = DateTime.ParseExact("10/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), Category = "testing", Devise = Currency.JPY, Montant = -1000.00m }
            };
            var AccountValue = transactionManager.GetAccountValue(transactions);

            Assert.AreEqual(-482.00m, AccountValue);
        }

        [TestMethod]
        public void GetAccountValue_should_return_correct_value()
        {
            var exchangeRateProvider = new ExchangeRate();
            var transactionManager = new Transaction(exchangeRateProvider);
            string filePath = GetTestFilePath("AccountUnitTest1.csv");
            DateTime targetDate = DateTime.ParseExact("16/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactionManager.LoadAccountTransactions(filePath, targetDate);
            var accountValue = transactionManager.GetAccountValue(transactions);
            Assert.AreEqual(7598.44m, accountValue);
        }


    }
}

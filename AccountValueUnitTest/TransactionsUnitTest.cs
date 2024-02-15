using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp5;
using System.IO;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace AccountValueUnitTest
{
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
            var transactioManager = new Transaction();
            string filePath = GetTestFilePath("AccountUnitTest1.csv");
            DateTime targetDate = DateTime.ParseExact("16/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactioManager.LoadAccountTransactions(filePath, targetDate);

            Assert.AreEqual(2, transactions.Count);
        }

        [TestMethod]
        public void LoadTransaction_should_return_empty_transaction_list()
        {
            var transactioManager = new Transaction();
            string filePath = GetTestFilePath("InvalidInputFile.csv");
            DateTime targetDate = DateTime.ParseExact("16/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactioManager.LoadAccountTransactions(filePath, targetDate);

            Assert.AreEqual(0,transactions.Count);
        }

        [TestMethod]
        public void LoadTransaction_should_return_exact_transactio_number_when_targetDate_between_transactions_date()
        {
            var transactioManager = new Transaction();
            string filePath = GetTestFilePath("AccountUnitTest1.csv");
            DateTime targetDate = DateTime.ParseExact("15/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactioManager.LoadAccountTransactions(filePath, targetDate);

            Assert.AreEqual(1, transactions.Count);
        }

        [TestMethod]
        public void GetAccountValue_should_return_initial_balance_when_transaction_is_null()
        {
            var transactioManager = new Transaction();

            var transactions = new List<Transaction>();
            var AccountValue = transactioManager.GetAccountValue(transactions);

            Assert.AreEqual(initialBalance, AccountValue);
        }

        [TestMethod]
        public void GetAccountValue_should_handle_negative_value()
        {
            var transactioManager = new Transaction();

            var transactions = new List<Transaction>() { new Transaction() { Date = DateTime.ParseExact("15/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), Category = "testing", Devise = "EUR", Montant = -8300.00m },
                                                       new Transaction() { Date = DateTime.ParseExact("10/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture), Category = "testing", Devise = "EUR", Montant = -1000.00m }
            };
            var AccountValue = transactioManager.GetAccountValue(transactions);

            Assert.AreEqual(-1000m, AccountValue);
        }

        [TestMethod]
        public void GetAccountValue_should_return_correct_value()
        {
            var transactioManager = new Transaction();
            string filePath = GetTestFilePath("AccountUnitTest1.csv");
            DateTime targetDate = DateTime.ParseExact("16/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var transactions = transactioManager.LoadAccountTransactions(filePath, targetDate);
            var accountValue = transactioManager.GetAccountValue(transactions);
            Assert.AreEqual(7598.44m, accountValue);
        }


    }
}

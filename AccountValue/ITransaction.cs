using System;
using System.Collections.Generic;

namespace AccountValue
{
    /// <summary>
    /// Interface of class transaction
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Load Account Transactions from csv file
        /// </summary>
        /// <param name="filePath">the csv file path</param>
        /// <param name="targetDate">the target date</param>
        /// <returns></returns>
        List<Transaction> LoadAccountTransactions(string filePath, DateTime targetDate);

        /// <summary>
        /// Function to get the Value of the Account in a given Date 
        /// </summary>
        /// <param name="targetDate">the given date</param>
        /// <param name="transactions">the transactions list of the account</param>
        decimal GetAccountValue(List<Transaction> transactions);
    }
}

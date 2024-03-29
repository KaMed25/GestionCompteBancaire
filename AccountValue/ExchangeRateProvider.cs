﻿namespace AccountValue
{
    /// <summary>
    /// Interface for the ExchangeRate class
    /// </summary>
    public abstract class ExchangeRateProvider
    {
        /// <summary>
        /// Get the value of the exchange rate to Euro
        /// </summary>
        /// <param name="devise">the currency to exchange</param>
        /// <returns></returns>
        public abstract decimal GetExchangeRateToEuro(Currency currency);
    }
}

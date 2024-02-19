using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountValue
{
    /// <summary>
    /// Class for get the exchange rate from any currency to Euro
    /// </summary>
    public class ExchangeRate : ExchangeRateProvider
    {
        public override decimal GetExchangeRateToEuro(Currency currency)
        {
            switch (currency)
            {
                case Currency.JPY:
                    return 0.482m;
                case Currency.USD:
                    return 1.445m;
                default:
                    return 1.0m;
            }
        }
    }

    /// <summary>
    /// Enumerator for world currencies
    /// </summary>
    public enum Currency
    {
        EUR,
        JPY,
        USD,
        Default=EUR,
    }
}

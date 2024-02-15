using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class ExchangeRate : ExchangeRateProvider
    {
        public override decimal GetExchangeRateToEuro(string devise)
        {
            if (devise == "JPY") return 0.482m;
            if (devise == "USD") return 1.445m;
            return 1.0m;
        }
    }
}

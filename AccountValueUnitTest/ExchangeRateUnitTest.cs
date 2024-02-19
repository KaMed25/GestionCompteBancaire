using AccountValue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccountValueUnitTest
{
    /// <summary>
    /// test class for exchange rate
    /// </summary>
    [TestClass]
    public class ExchangeRateUnitTest
    {
        [TestMethod]
        public void ExchangeRate_Should_Not_Be_Null()
        {
            var ExchangeRateProvider = new ExchangeRate();

            decimal rateJPYtoEUR = ExchangeRateProvider.GetExchangeRateToEuro(Currency.JPY);
            decimal rateUSDToEUR = ExchangeRateProvider.GetExchangeRateToEuro(Currency.USD);

            Assert.IsNotNull(rateJPYtoEUR);
            Assert.IsNotNull(rateUSDToEUR);
        }

        
        [TestMethod]
        public void ExchangeRate_Return_The_Expected_Rate()
        {
            var ExchangeRateProvider = new ExchangeRate();

            decimal rateJPYtoEUR = ExchangeRateProvider.GetExchangeRateToEuro(Currency.JPY);
            decimal rateUSDToEUR = ExchangeRateProvider.GetExchangeRateToEuro(Currency.USD);
            decimal rateEURO = ExchangeRateProvider.GetExchangeRateToEuro(Currency.EUR);

            Assert.AreEqual(0.482m, rateJPYtoEUR);
            Assert.AreEqual(1.445m, rateUSDToEUR);
            Assert.AreEqual(1m, rateEURO);
        }

    }
}

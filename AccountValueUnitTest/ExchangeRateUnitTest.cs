using AccountValue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccountValueUnitTest
{
    [TestClass]
    public class ExchangeRateUnitTest
    {
        [TestMethod]
        public void ExchangeRate_Should_Not_Be_Null()
        {
            var ExchangeRateProvider = new ExchangeRate();

            decimal rateJPYtoEUR = ExchangeRateProvider.GetExchangeRateToEuro("JPY");
            decimal rateUSDToEUR = ExchangeRateProvider.GetExchangeRateToEuro("USD");

            Assert.IsNotNull(rateJPYtoEUR);
            Assert.IsNotNull(rateUSDToEUR);
        }

        [TestMethod]
        public void ExchaneRate_should_return_1_when_invalid_givenRate()
        {
            var ExchangeRateProvider = new ExchangeRate();

            decimal invalidRate = ExchangeRateProvider.GetExchangeRateToEuro("InvalidRate");

            Assert.AreEqual(1, invalidRate);
        }

        [TestMethod]
        public void ExchangeRate_Return_The_Expected_Rate()
        {
            var ExchangeRateProvider = new ExchangeRate();

            decimal rateJPYtoEUR = ExchangeRateProvider.GetExchangeRateToEuro("JPY");
            decimal rateUSDToEUR = ExchangeRateProvider.GetExchangeRateToEuro("USD");
            decimal rateEURO = ExchangeRateProvider.GetExchangeRateToEuro("EUR");

            Assert.AreEqual(0.482m, rateJPYtoEUR);
            Assert.AreEqual(1.445m, rateUSDToEUR);
            Assert.AreEqual(1m, rateEURO);
        }
    }
}

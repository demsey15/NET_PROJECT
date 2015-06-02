using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sklep;


namespace SkelpTests
{
    [TestClass]
    public class CurrencyTest
    {
        [TestMethod]
        public void ExchangeMoneyTest()
        {
            Currency c = new Currency("k", "k");
            c.ExchangeRate = 5;

            double priceForeignCurrency = c.ExchangeMoney(10);

            Assert.AreEqual(Math.Round(10 / c.ExchangeRate, 2), priceForeignCurrency);
        }

        [TestMethod]
        public void ExchangeMoneyTest2()
        {
            Currency c = new Currency("k", "k");
            c.ExchangeRate = 0.5;

            double priceForeignCurrency = c.ExchangeMoney(10);

            Assert.AreEqual(Math.Round(10 / c.ExchangeRate, 2), priceForeignCurrency);
        }
    }
}

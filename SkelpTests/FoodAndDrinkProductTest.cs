using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sklep;

namespace SklepTests
{
    [TestClass]
    public class FoodAndDrinkProductTest
    {
        [TestMethod]
        public void SetExpirationDateTest()
        {
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(3), "jogurt", 3.50, 3);
            Assert.AreEqual(product.ExpirationDate, DateTime.Today.AddDays(3));
        }

        [TestMethod]
        public void SetPriceLessThanZeroTestShouldThrowException()
        {
            try
            {
                FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(3), "jogurt", -3.50, 3);
                Assert.IsFalse(true);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(e.ParamName, "price");
            }

        }

        [TestMethod]
        public void ComputePriceAmountLessThanZeroTestShouldThrowException()
        {
            try
            {
                FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(3), "jogurt", 3.50, 3);
                Currency c = new Currency("pln", "zloty");
                c.ExchangeRate = 1;
                product.ComputePrice(-1, c);
                Assert.IsFalse(true);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(e.ParamName, "amount");
            }

        }

        [TestMethod]
        public void ComputePriceInZlotyFor1ProductWithGoodExpirationTimeTest()
        {
            Currency c = new Currency("pln", "zloty");
            c.ExchangeRate = 1;
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(20), "jogurt", 3.50, 3);
            
            double price = product.ComputePrice(1, c);

            Assert.AreEqual(price, product.Price);
        }

        [TestMethod]
        public void ComputePriceInZlotyFor1ProductWithExpirationTimeLessEq2Test()
        {
            Currency c = new Currency("pln", "zloty");
            c.ExchangeRate = 1;
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(1), "jogurt", 3.50, 3);

            double price = product.ComputePrice(1, c);

            Assert.AreEqual(price, product.Price * 0.6);
        }

        [TestMethod]
        public void ComputePriceInZlotyFor1ProductWithExpirationTimeLessEq4Test()
        {
            Currency c = new Currency("pln", "zloty");
            c.ExchangeRate = 1;
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(3), "jogurt", 3.50, 3);

            double price = product.ComputePrice(1, c);

            Assert.AreEqual(price, product.Price * 0.8);
        }

        [TestMethod]
        public void ComputePriceInZlotyFor1ProductWithExpirationTimeLessEq6Test()
        {
            Currency c = new Currency("pln", "zloty");
            c.ExchangeRate = 1;
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);

            double price = product.ComputePrice(1, c);

            Assert.AreEqual(price, product.Price * 0.9);
        }

        [TestMethod]
        public void ComputePriceInZlotyFor4ProductWithExpirationTimeLessEq6Test()
        {
            Currency c = new Currency("pln", "zloty");
            c.ExchangeRate = 1;
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);

            double price = product.ComputePrice(4, c);

            Assert.AreEqual(price, 4 * product.Price * 0.9);
        }

        [TestMethod]
        public void ComputePriceInForeignCurrencyFor1ProductWithExpirationTimeLessEq6Test()
        {
            Currency c = new Currency("foreign", "foreign");
            c.ExchangeRate = 5;
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);

            double price = product.ComputePrice(1, c);

            Assert.AreEqual(price, Math.Round(product.Price / c.ExchangeRate, 2) * 0.9);
        }

        [TestMethod]
        public void ComputePriceInZlotyFor4ProductWithExpirationTimeLessEq6AsProductObjectTest()
        {
            Currency c = new Currency("pln", "zloty");
            c.ExchangeRate = 1;
            Product product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);

            double price = product.ComputePrice(4, c);

            Assert.AreEqual(price, 4 * product.Price * 0.9);
        }

        [TestMethod]
        public void DeepCopyFoodProductTest()
        {
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);
            Product copy = product.DeepCopy();
            FoodAndDrinkProduct copy2 = (FoodAndDrinkProduct) copy;

            Assert.IsFalse(product == copy2);
        }

        [TestMethod]
        public void FoodProductsWithTheSameIdShouldBeEqualTest()
        {
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);
            FoodAndDrinkProduct product2 = new FoodAndDrinkProduct(DateTime.Today.AddDays(1), "kanapka", 3, 3);

            Assert.AreEqual(product, product2);
        }

        [TestMethod]
        public void FoodProductsWithDifferentIdShouldntBeEqualTest()
        {
            FoodAndDrinkProduct product = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 3);
            FoodAndDrinkProduct product2 = new FoodAndDrinkProduct(DateTime.Today.AddDays(5), "jogurt", 3.50, 4);

            Assert.AreNotEqual(product, product2);
        }
 
    }
}

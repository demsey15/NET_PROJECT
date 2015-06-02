using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sklep;
using System.Windows;


namespace SkelpTests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void RemoveExistingProductTest()
        {
            Product product = new Product("dd", 4, 1);
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 1;
            Transaction t = new Transaction(c);
            t.AddProduct(product);

            bool result = t.RemoveProduct(product);

            Assert.IsTrue(result && !t.BoughtProducts.Contains(product));
        }

        [TestMethod]
        public void RemoveNotExistingProductTest()
        {
            Product product = new Product("dd", 4, 1);
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 1;
            Transaction t = new Transaction(c);

            bool result = t.RemoveProduct(product);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void GetTotalInZlotyTest()
        {
            Product product = new Product("dd", 4, 1);
            Product product2 = new Product("dd", 6.7, 1);
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 1;
            Transaction t = new Transaction(c);
            t.AddProduct(product);
            t.AddProduct(product);
            t.AddProduct(product2);

            double total = t.Total;

            Assert.AreEqual(2 * product.Price + product2.Price, total);
        }

        [TestMethod]
        public void GetTotalInForeignCurrencyTest()
        {
            Product product = new Product("dd", 4, 1);
            Product product2 = new Product("dd", 6.7, 1);
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 2;
            Transaction t = new Transaction(c);
            t.AddProduct(product);
            t.AddProduct(product);
            t.AddProduct(product2);

            double total = t.Total;

            Assert.AreEqual(2 * c.ExchangeMoney(product.Price) + c.ExchangeMoney(product2.Price), total);
        }

        [TestMethod]
        public void GetTotalInForeignCurrencyForFoodProductTooTest()
        {
            Product product = new Product("dd", 4, 1);
            Product product2 = new Product("dd", 6.7, 1);
            FoodAndDrinkProduct product3 = new FoodAndDrinkProduct(DateTime.Today.AddDays(1), "d", 4.53, 2); 
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 2.53;
            Transaction t = new Transaction(c);
            t.AddProduct(product);
            t.AddProduct(product);
            t.AddProduct(product2);
            t.AddProduct(product3);
            t.AddProduct(product3);

            double total = t.Total;

            Assert.AreEqual(Math.Round(2 * c.ExchangeMoney(product.Price) + c.ExchangeMoney(product2.Price) + 2 *
                      product3.ComputePrice(1, c), 2), total);
        }

        [TestMethod]
        public void GetTotalAfterChangeCurrencyTest()
        {
            Product product = new Product("dd", 4, 1);
            Product product2 = new Product("dd", 6.7, 1);
            FoodAndDrinkProduct product3 = new FoodAndDrinkProduct(DateTime.Today.AddDays(1), "d", 4.53, 2);
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 2.53;
            Transaction t = new Transaction(c);
            t.AddProduct(product);
            t.AddProduct(product);
            t.AddProduct(product2);
            t.AddProduct(product3);
            t.AddProduct(product3);
            Currency c2 = new Currency("e", "e");
            c2.ExchangeRate = 4.89;
           
            double totalBefore = t.Total;
            t.Currency = c2;
            double total = t.Total;

            Assert.AreEqual(Math.Round(2 * c2.ExchangeMoney(product.Price) + c2.ExchangeMoney(product2.Price) + 2 *
                      product3.ComputePrice(1, c2), 2), total);
        }

        [TestMethod]
        public void GetChangeTest()
        {
            Product product = new Product("dd", 4, 1);
            Product product2 = new Product("dd", 6.7, 1);
            Currency c = new Currency("a", "a");
            c.ExchangeRate = 1;
            Transaction t = new Transaction(c);
            t.AddProduct(product);
            t.AddProduct(product);
            t.AddProduct(product2);

            double change = t.GetChange(t.Total + 1);
            

            Assert.IsTrue(change == 1);
        }


    }
}

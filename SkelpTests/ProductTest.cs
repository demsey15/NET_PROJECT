using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sklep;

namespace SklepTests
{
    [TestClass]
    public class ProductTest
    {
       [TestMethod]
        public void SetPriceTest()
        {
            Product product = new Product("test1", 4, 1);
            Assert.AreEqual(4, product.Price);
        }

       [TestMethod]
       public void SetPriceLessThanZeroTestShouldThrowException()
       {
           try
           {
               Product product = new Product("test1", -2, 1);
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
               Product product = new Product("test1", 2, 1);
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
       public void ComputePriceInZlotyFor1ProductTest()
       {
           Currency c = new Currency("pln", "zloty");
           c.ExchangeRate = 1;
           Product product = new Product("test1", 4, 1);

           double price = product.ComputePrice(1, c);

           Assert.AreEqual(price, product.Price);
       }

       [TestMethod]
       public void ComputePriceInZlotyFor4ProductTest()
       {
           Currency c = new Currency("pln", "zloty");
           c.ExchangeRate = 1;
           Product product = new Product("test1", 4, 1);

           double price = product.ComputePrice(4, c);

           Assert.AreEqual(price, 4 * product.Price);
       }
    
        [TestMethod]
        public void ComputePriceInForeignCurrencyFor1ProductTest()
        {
            Currency c = new Currency("eur", "euro");
            c.ExchangeRate = 2.50;
            Product product = new Product("test1", 4, 1);

            double price = product.ComputePrice(1, c);

            Assert.AreEqual(price, product.Price / c.ExchangeRate);
        }

       [TestMethod]
        public void ComputePriceInForeignCurrencyFor4ProductTest()
        {
            Currency c = new Currency("eur", "euro");
            c.ExchangeRate = 2.50;
            Product product = new Product("test1", 4, 1);

            double price = product.ComputePrice(4, c);

            Assert.AreEqual(price, (4 *product.Price) / c.ExchangeRate);
        }

        [TestMethod]
        public void DeepCopyProductTest()
        {
            Product product = new Product("test1", 4, 1);
            Product copy = product.DeepCopy();

            Assert.IsFalse(product == copy);
        }

       [TestMethod]
        public void ProductsWithTheSameIdShouldBeEqualTest()
        {
            Product product = new Product("test1", 4, 1);
            Product product2 = new Product("ala", 3, 1);

            Assert.AreEqual(product, product2);
        }

       [TestMethod]
       public void ProductsWithDifferentIdShouldntBeEqualTest()
       {
           Product product = new Product("ala", 3, 1);
           Product product2 = new Product("ala", 3, 2);

           Assert.AreNotEqual(product, product2);
       }
    }
}

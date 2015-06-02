using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sklep;

namespace SkelpTests
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void AddNewProduct()
        {
            Store s = new Store();
            Product product = new Product("a", 4, 100);

            bool result = s.AddProduct(product);

            Assert.IsTrue(s.Products.Contains(product) && result);
        }

        [TestMethod]
        public void AddAlreadyExistingProduct()
        {
            Store s = new Store();
            Product product = new Product("a", 4, 100);
            Product productWithTheSameId = new Product("b", 3, 100);

            bool result = s.AddProduct(product);
            bool result2 = s.AddProduct(productWithTheSameId);

            Assert.IsTrue(s.Products.Contains(product) && result && !result2);
        }
    }
}

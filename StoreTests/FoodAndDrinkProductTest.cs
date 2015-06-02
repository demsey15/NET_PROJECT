using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace StoreTests
{
    [TestClass]
    public class FoodAndDrinkProductTest
    {
        [TestMethod]
        public void DeepCopyTest()
        {
            FoodAndDrinkProduct f1 = new FoodAndDrinkProduct(DateTime.Today, "produkt", 3, 1);

        }
    }
}

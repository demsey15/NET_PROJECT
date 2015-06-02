using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;


namespace Sklep
{
    class ProductFileHandler
    {
        private const string FILENAME = "produkty.txt";
        private ProductXmlSerializer _serializer = new ProductXmlSerializer();

        public void SaveToFile(List<Product> products)
        {
            using (StreamWriter writer = new StreamWriter(FILENAME))
            {
                foreach (Product product in products)
                {
                    writer.WriteLine(_serializer.SerializeProduct(product));
                    writer.WriteLine("%%");

                }
            }
        }

        public List<Product> ReadFromFile()
        {
            string temporary;
            List<Product> products = new List<Product>();

            using (StreamReader reader = new StreamReader(FILENAME))
            {
                string productString = "";
                while ((temporary = reader.ReadLine()) != null)
                {
                    if (temporary.Equals("%%"))
                    {

                        Product product = _serializer.DeserializeProduct(productString);
                        products.Add(product);
                        productString = "";
                        
                    }
                    else
                    {
                        productString = productString + temporary + "\n";
                    }
                }
            }
            return products;
        }
    }
}

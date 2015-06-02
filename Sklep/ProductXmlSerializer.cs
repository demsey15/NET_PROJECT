using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Sklep
{
    public class ProductXmlSerializer
    {
        private readonly XmlSerializer _serializer;

        public ProductXmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(Product), new Type[] { typeof(FoodAndDrinkProduct) });
        }

        public string SerializeProduct(Product product)
        {
            StringBuilder builder = new StringBuilder();

            using (StringWriter s = new StringWriter(builder))
            {
                _serializer.Serialize(s, product);
            }
            return builder.ToString();
        }

        public Product DeserializeProduct(string text)
        {
            Product product = null;
            using (StringReader r = new StringReader(text))
            {
                product = (Product) _serializer.Deserialize(r);
            }
            return product;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Sklep
{
    class LoadProductsListThread
    {

        private ObservableCollection<Product> _products;

        public LoadProductsListThread(ObservableCollection<Product> products)
        {
            _products = products;
        }
        
        public void Run()
        {
            ProductFileHandler f = new ProductFileHandler();
            List<Product> products = f.ReadFromFile();

            foreach (Product p in products)
            {
                _products.Add(p);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;

namespace Sklep
{
    public class Store
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        
        public Store()
        {
            LoadProductsListThread thread = new LoadProductsListThread(_products);
            (new Thread(new ThreadStart(thread.Run))).Start();
        }

        public ObservableCollection<Product> Products {
            get
            {
                return _products;
            }
        }

        public bool AddProduct(Product product)        // można dodać tylko produkty o różnych id
        {
            if (_products.Contains(product))
                return false;
            else
            {
                _products.Add(product);
                return true;
            }
        }
    }
}

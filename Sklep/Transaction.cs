using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;


namespace Sklep
{
    public class Transaction
    {
        private ObservableCollection<Product> _boughtProducts = new ObservableCollection<Product>();
        
        public Currency Currency { get; set; }

        public Transaction(Currency currency)
        {
            Currency = currency;   
        }

        public void AddProduct(Product product){
            _boughtProducts.Add(product);
        }

        public ObservableCollection<Product> BoughtProducts
        {
            get
            {
                return _boughtProducts;
            }
        }

        public bool RemoveProduct(Product product)
        {
            if (_boughtProducts.Contains(product))
            {
                _boughtProducts.Remove(product);
                return true;
            }
            else return false;
        }

        public double Total {
            get{
                double total = 0;
                foreach(Product product in _boughtProducts){
                    total = total + product.ComputePrice(1, Currency);
                }
                return Math.Round(total, 2);
                } 
        }

        public double GetChange(double payed)
        {
            return Math.Round(payed - Total, 2);
        }

}
}

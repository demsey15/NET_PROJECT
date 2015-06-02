using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Sklep
{
    public class Control
    {
        private Store _store = new Store();
        private Transaction _transaction;

        public Transaction StartTransaction(Currency currency){
            CurrentCurrency = currency;
            _transaction = new Transaction(currency);
            return _transaction;
        }

        public bool TransactionStarted { 
            get{
                return _transaction != null;
                }
        }

        public bool BuyProduct(int productId)
        {
            foreach (Product product in _store.Products)
            {
                if (product.Id == productId)
                {
                    _transaction.AddProduct(product);
                    return true;
                }
            }
            return false;

        }

        public bool RemoveProduct(int productId)
        {
            foreach (Product product in _store.Products)
            {
                if (product.Id == productId)
                {
                    _transaction.RemoveProduct(product);
                    return true;
                }
            }
            return false;
        }

        public Store Store
        {
            get
            {
                return _store;
            }
        }

        public ObservableCollection<Product> BoughtProducts
        {
            get
            {
                if (_transaction == null || _transaction.BoughtProducts == null)
                    return new ObservableCollection<Product>();
                else return _transaction.BoughtProducts;
            }
        }

        public Currency CurrentCurrency { 
            get{
                if(_transaction == null) return null;
                else return _transaction.Currency;
            }
            set{
                if (_transaction != null)
                {
                        _transaction.Currency = value; 
                }
            } 
        }
    }
}

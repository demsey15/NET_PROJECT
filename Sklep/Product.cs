using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Sklep
{
    public class Product : IDeepCopy<Product>
    {
        private double _price;
        private string _name;
        private int _id;

        public Product()
        {
                
        }

        public Product(string name, double price, int id)
        {
            if (price < 0) throw new ArgumentOutOfRangeException("price", "Cena nie może być ujemna");
            this._name = name;
            this._price = price;
            this._id = id;
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual double ComputePrice(int amount, Currency currency)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("amount", "Ilość powinna być większa lub równa 0");
            return currency.ExchangeMoney(amount * Price);
        }

        public virtual Product DeepCopy()
        {
            return new Product(_name, _price, _id);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Product p2 = (Product)obj;
            return this._id == p2._id;
        }
    }
}

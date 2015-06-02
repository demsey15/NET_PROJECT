using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class FoodAndDrinkProduct : Product
    {
        private DateTime _expirationDate;

        public FoodAndDrinkProduct()
        {
            _expirationDate = DateTime.Today;
        }

        public FoodAndDrinkProduct(DateTime expirationDate, string name, double price, int id) : base(name, price, id)
        {
            this._expirationDate = expirationDate;
        }

        public DateTime ExpirationDate {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }
        
        public override double ComputePrice(int amount, Currency currency){
            double price = base.ComputePrice(amount, currency);
            if (_expirationDate != null)
            {
                TimeSpan offset = _expirationDate - DateTime.Today;
                if (offset.Days <= 2)
                {
                    price = 0.6 * price;                         //40% znizki w przypadku daty zakupu <= 2 dni do dnia upływu daty przydatności do spożycia
                }
                else if (offset.Days <= 4)
                {
                    price = 0.8 * price;
                }
                else if (offset.Days <= 6)
                {
                    price = 0.9 * price;
                }
            }
            return price;
        }

        public override Product DeepCopy()
        {
            DateTime date = new DateTime(_expirationDate.Year, _expirationDate.Month, _expirationDate.Day);
            return new FoodAndDrinkProduct(date, base.Name, base.Price, base.Id);
        }
    }
}

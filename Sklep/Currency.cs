using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class Currency
    {
        private double _exchange_rate;
        private string _symbol;
        private string _long_name;

        public Currency(string symbol, string longName)
        {
            this._symbol = symbol;
            this._long_name = longName;
        }

        public string Symbol
        {
            get
            {
                return _symbol;
            }
        }

        public string LongName
        {
            get
            {
                return _long_name;
            }
        }

        public double ExchangeRate {
            get
            {
                return _exchange_rate;
            }
            set
            {
                _exchange_rate = value;
            }
        }

        public double ExchangeMoney(double pln)
        {
            return Math.Round(pln / ExchangeRate, 2);   //wymienia pieniądze i zaokrągla je do dwóch miejsc po przecinku
        }
    }
}

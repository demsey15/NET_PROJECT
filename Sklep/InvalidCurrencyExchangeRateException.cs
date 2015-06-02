using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class InvalidCurrencyExchangeRateException : Exception
    {
        public const string ErrorMessage = "Get currency exchange rate is improbabe, the value is: '{1}'";

        public double Value
        {
            get;
            set;
        }

        public InvalidCurrencyExchangeRateException(string text, double value) : base(text)
        {
            Value = value;
        }

        public string getErrorMessage()
        {
            return string.Format(ErrorMessage, Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.IO;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sklep
{
    public class CurrencyInternetProvider
    {
        public const string PLN = "PLN"; //A const object is always static.
        public const string EUR = "EUR";
        public const string USD = "USD";

        public double ProvideExchangeRate(string from, string to)
        {
            string sURL = "http://www.webservicex.net/CurrencyConvertor.asmx/ConversionRate?FromCurrency=" + from + "&ToCurrency=" +
                to;
            WebRequest wrGETURL = WebRequest.Create(sURL);
            Stream objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;
            string sLineTemp = "";
            while (sLineTemp != null)
            {
                i++;
                sLineTemp = objReader.ReadLine();
                if (sLineTemp != null)
                    sLine = sLine + sLineTemp;
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sLine);
            double result = 5;
            string s = doc.DocumentElement.InnerText;
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";
            Double.TryParse(s, NumberStyles.Float, numberFormatInfo, out result);
            return result;
        }
    }
}

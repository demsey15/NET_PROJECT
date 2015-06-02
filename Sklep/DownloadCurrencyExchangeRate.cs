using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;


namespace Sklep
{
    public class DownloadCurrencyExchangeRate
    {
        private Label _label;
        private string _from;
        private string _to;
        private CheckBox _check;
       

        public DownloadCurrencyExchangeRate(Label label, CheckBox check, string from, string to)
        {
            this._label = label;
            this._from = from;
            this._to = to;
            _check = check;
           
        }

        public void Run()
        {
            try
            {
                CurrencyInternetProvider provider = new CurrencyInternetProvider();
                double d = provider.ProvideExchangeRate(_from, _to);
                var disp = _label.Dispatcher;
                disp.BeginInvoke(DispatcherPriority.Normal, (Action)(() => _label.Content = d));
            }
            catch (System.Net.WebException e)
            {
                MessageBox.Show("Brak połączenia z internetem.\nPłatność możliwa tylko w złotówkach!");
                var disp = _check.Dispatcher;
                disp.BeginInvoke(DispatcherPriority.Normal, (Action)(() => _check.IsEnabled = false));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;

namespace Sklep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Control _control = new Control();
        private Store _store;
        private Transaction _transaction;

        public MainWindow()
        {
            _store = _control.Store;
            InitializeComponent();
            InitBinding();
        }

        private void InitBinding()
        {
            ProductsList.ItemsSource = _store.Products;
       
            DownloadCurrencyExchangeRate downloadEuro = new DownloadCurrencyExchangeRate(euroLab, euroCheck, CurrencyInternetProvider.EUR,
                CurrencyInternetProvider.PLN);
            DownloadCurrencyExchangeRate downloadUsd = new DownloadCurrencyExchangeRate(dollarLab, dollarCheck, CurrencyInternetProvider.USD, 
                CurrencyInternetProvider.PLN);
           
                (new Thread(new ThreadStart(downloadEuro.Run))).Start();
                (new Thread(new ThreadStart(downloadUsd.Run))).Start();
        
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_control.TransactionStarted)
            {
                string id = idTxt.Text;
                string amount = amountTxt.Text;
                int idInt;
                int amountInt;
                if (Int32.TryParse(id, out idInt))
                {
                    if (Int32.TryParse(amount, out amountInt))
                    {
                       
                        if (! ((bool) backChBox.IsChecked))
                        {
                            if (!_control.BuyProduct(idInt))
                            {
                                MessageBox.Show("Nie ma produktu o zadanym id");
                            }
                            totalLabel.Content = _transaction.Total;
                            for (int i = 1; i < amountInt; i++)
                            {
                                _control.BuyProduct(idInt);
                                totalLabel.Content = _transaction.Total;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < amountInt; i++)
                            {
                                _control.RemoveProduct(idInt);
                                totalLabel.Content = _transaction.Total;
                            }
                        }
                        idTxt.Text = "";
                        amountTxt.Text = "";
                    }
                    else MessageBox.Show("Ilość musi być cyfrą!");
                }
                else MessageBox.Show("Id musi być cyfrą!");
            }
            else
            {
                MessageBox.Show("Rozpocznij sprzedaż");
            }
        }

        private void startTransactionButt_Click(object sender, RoutedEventArgs e)
        {
            startTransaction();
        }

        private void startTransaction()
        {
            Currency zloty = new Currency("PLN", "złoty");
            zloty.ExchangeRate = 1;

            _transaction = _control.StartTransaction(zloty);
       
            boughtList.ItemsSource = _control.BoughtProducts;

            totalLabel.DataContext = _transaction;
            totalLabel.Content = 0;
            currencyLabel.Content = zloty.Symbol;
            euroCheck.IsChecked = false;
            dollarCheck.IsChecked = false;
        }

        private void dollarCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (_control.TransactionStarted)
            {
                euroCheck.IsChecked = false;
                Currency currency = new Currency("USD", "dolary");
                double exchange = 1;
                NumberFormatInfo info = new NumberFormatInfo();
                info.NumberDecimalSeparator = ",";
                string dollars = dollarLab.Content.ToString();
                Double.TryParse(dollars, NumberStyles.Float, info, out exchange);
                currency.ExchangeRate = exchange;
                _control.CurrentCurrency = currency;
                totalLabel.Content = _transaction.Total;
                currencyLabel.Content = _control.CurrentCurrency.Symbol;
            }
            else
            {
                dollarCheck.IsChecked = false;
                MessageBox.Show("Rozpocznij sprzedaż");
            }
        }

        private void euroCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (_control.TransactionStarted)
            {
                dollarCheck.IsChecked = false;
                Currency currency = new Currency("EUR", "euro");
                double exchange = 1;
                NumberFormatInfo info = new NumberFormatInfo();
                info.NumberDecimalSeparator = ",";
                string euros = euroLab.Content.ToString();
                Double.TryParse(euros, NumberStyles.Float, info, out exchange);
                currency.ExchangeRate = exchange;
                _control.CurrentCurrency = currency;
                totalLabel.Content = _transaction.Total;
                currencyLabel.Content = _control.CurrentCurrency.Symbol;
            }
            else
            {
                euroCheck.IsChecked = false;
                MessageBox.Show("Rozpocznij sprzedaż");
            }
        }

        private void euroCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_control.TransactionStarted)
            {
                Currency currency = new Currency("PLN", "zloty");
                currency.ExchangeRate = 1;
                _control.CurrentCurrency = currency;
                totalLabel.Content = _transaction.Total;
                currencyLabel.Content = _control.CurrentCurrency.Symbol;
            }
            else
            {
                MessageBox.Show("Rozpocznij sprzedaż");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FinishTransactionWindow f = new FinishTransactionWindow(_transaction);
            f.Show();
            startTransaction();
            
        }

     
    }
}

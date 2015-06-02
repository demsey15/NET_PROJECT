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
using System.Windows.Shapes;

namespace Sklep
{
    /// <summary>
    /// Interaction logic for FinishTransactionWindow.xaml
    /// </summary>
    public partial class FinishTransactionWindow : Window
    {
        private readonly Transaction _transaction;
        private readonly double _total;
        
        public FinishTransactionWindow(Transaction transaction)
        {
            _transaction = transaction;
            InitializeComponent();
            currencyLabel.Content = _transaction.Currency.Symbol;
            currencyLabel2.Content = _transaction.Currency.Symbol;
            totalLabel.Content = _transaction.Total;
            _total = _transaction.Total;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double payed = 0;
            if (Double.TryParse(payedText.Text, out payed))
                changeLabel.Content = Math.Round(payed - _total, 2);
            else
                MessageBox.Show("Kwota zapłacono musi być liczbą!");
        }

      

     
    }
}

using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CryptoCurrencies.WPF.Components
{
    /// <summary>
    /// Interaction logic for CurrenciesTable.xaml
    /// </summary>
    public partial class CoinsTable : UserControl
    {
        public CoinsTable()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CoinsProperty =
                   DependencyProperty.Register(
                         "Coins",
                          typeof(Collection<MainCoin>),
                          typeof(CoinsTable));

        public Collection<MainCoin> Coins
        {
            get
            {
                return (Collection<MainCoin>)GetValue(CoinsProperty);
            }
            set
            {
                SetValue(CoinsProperty, value);
            }
        }
    }
}

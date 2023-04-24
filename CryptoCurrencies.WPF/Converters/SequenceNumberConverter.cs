using CryptoCurrencies.CoinGecko.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptoCurrencies.WPF.Converters
{
    public class SequenceNumberConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = (ObservableCollection<TickerInfo>)values[1];
            var itemIndex = collection.IndexOf((TickerInfo)values[0]);

            return (itemIndex + 1).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

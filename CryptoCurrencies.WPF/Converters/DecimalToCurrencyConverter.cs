using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptoCurrencies.WPF.Converters
{
    public class DecimalToCurrencyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null) return string.Empty;

            var cult = (string)values[1];
            var price = (decimal)values[0];

            int precision;
            if (price >= 1 || price < 0) precision = 2;
            else if (price > 0.099M) precision = 4;
            else precision = (int)Math.Log10((double)(1M / price)) * 2;
            return string.Format(new CultureInfo(cult), "{0:C" + precision + "}", price);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

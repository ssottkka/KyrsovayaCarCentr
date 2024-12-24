using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CARCENTRdb
{
    public class TotalSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Collections.IEnumerable deals)
            {
                return deals.Cast<Сделка>().Sum(d => d.Сумма_сделки);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
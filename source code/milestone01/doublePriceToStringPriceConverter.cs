using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace milestone01
{
    class doublePriceToStringPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString().Trim();
            int len = str.Length;

            for(int i = 3; i < len; i = i + 3)
            {
                str = str.Insert(len - i, ",");
            }
            return str + " VNĐ";
        }

        public static string convert(string value)
        {
            int len = value.Length;

            for (int i = 3; i < len; i = i + 3)
            {
                value = value.Insert(len - i, ",");
            }
            return value + " VNĐ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

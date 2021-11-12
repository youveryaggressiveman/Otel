using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Otel.Converter
{
    public class CardNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            var builder = new StringBuilder(Regex.Replace(value.ToString(), @"\D", ""));

            foreach (var item in Enumerable.Range(0, builder.Length / 4).Reverse())
            {
                builder.Insert(4 * item + 4, " ");
            }

            return builder.ToString().Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");

        }
    }
}

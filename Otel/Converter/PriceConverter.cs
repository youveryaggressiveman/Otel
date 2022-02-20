using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Otel.Converter
{
    /// <summary>
    /// Класс, реализующий интерфейс IValueConverter
    /// </summary>
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var builder = new StringBuilder(Regex.Replace(value.ToString(), @"\D", ""));

            foreach (var item in Enumerable.Range(0, builder.Length / 3).Reverse())
            {
                if (builder.Length < 5)
                {
                    builder.Insert(1 * item + 1, " ");

                    continue;
                }

                if (builder.Length == 5)
                {
                    builder.Insert(2 * item + 2, " ");

                    continue;
                }

                if (builder.Length > 5)
                {
                    builder.Insert(3 * item + 3, " ");

                    continue;
                }
            }

            return builder.ToString().Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");
        }
    }
}

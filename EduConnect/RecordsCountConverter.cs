using System;
using System.Globalization;
using System.Windows.Data;

namespace EduConnect
{
    public class RecordsCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is int))
                return "Количество записей: 0";

            int count = (int)value;
            return count == -1 ? "Количество записей: 0" : $"Количество записей: {count} ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

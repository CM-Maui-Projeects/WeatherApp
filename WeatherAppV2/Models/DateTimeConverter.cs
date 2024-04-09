using System.Globalization;

namespace WeatherAppV2.Models
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long ticks)
            {
                return new DateTime(ticks);
            }
            // Fallback value, could also be something else
            return DateTime.MinValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        //DateTime _time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    long dateTime = (long)value;
        //    return $"{_time.AddSeconds(dateTime).ToString()} UTC";
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HisFeldTry1
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(bool))
            {
                if (parameter != null && bool.Parse(parameter as String))
                {
                    if ((bool)value)
                    {
                        return Visibility.Collapsed;
                    }
                    return Visibility.Visible;
                }
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

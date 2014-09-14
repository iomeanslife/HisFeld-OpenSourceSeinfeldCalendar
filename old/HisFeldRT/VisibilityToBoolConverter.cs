using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HisFeldRT
{
    public class VisibilityToBoolConverter : IValueConverter
    {
      

        public object Convert(object value, Type targetType, object parameter, string language)
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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

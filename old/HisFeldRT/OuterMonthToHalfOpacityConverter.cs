using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml.Data;


namespace HisFeldRT
{
    public class OuterMonthToHalfOpacityConverter : IValueConverter
    {
       

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() == typeof(int))
            {
                if ((int)value != DateTime.Today.Month)
                {
                    return 0.25d;
                }
            }
            return 1d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class NumberToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (((int)value % 2) == 0)
                return Color.White;
            else
                return Color.FromHex("#E9ECEF");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((int)value % 2) == 0)
                return Color.White;
            else
                return Color.FromHex("#E9ECEF");
        }
    }
}

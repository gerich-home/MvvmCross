using System;
using System.Linq;
using Cirrious.MvvmCross.Converters;

namespace Tutorial.Core.Converters
{
    public class StringReverseValueConverter
        : MvxBaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var stringValue = value as string;
            if (string.IsNullOrEmpty(stringValue))
                return string.Empty;
            // note that the ToCharArray is needed in WinRT!
            return new string(stringValue.ToCharArray().Reverse().ToArray());
        }
    }
}
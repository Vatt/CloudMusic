using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace CloudMusic.UWP.Models.Converters
{
    internal class ObjectToTracklist : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return value;
            return (TracklistCollection) value;
        }
    }
}

using System;
using Windows.UI.Xaml.Data;

namespace VisualTemplate.View.Converters
{
    class FrameStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is MasterView))
                throw new ArgumentException("Value is not of type MasterView");
            string text = "";
            if(value is MasterView)
                text = "Search a element";
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace DiskVolumesExplorer.Client.Components
{
    class IconTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IconsProvider.GetIcon((IconType) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

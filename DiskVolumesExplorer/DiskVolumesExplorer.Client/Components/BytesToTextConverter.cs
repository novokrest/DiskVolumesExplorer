using System;
using System.Globalization;
using System.Windows.Data;

namespace DiskVolumesExplorer.Client.Components
{
    internal class BytesToGigabytestConverter : IValueConverter
    {
        private const uint BytesInGB = 1 << 30;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ulong bytes = (ulong)value;

            return 1.0 * bytes / BytesInGB;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

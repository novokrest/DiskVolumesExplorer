using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DiskVolumesExplorer.Client.Components
{
    internal class DrivesToVolumesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var drives = value as IReadOnlyCollection<DriveViewModel>;
            if (drives == null) return null;

            var volumes = drives.SelectMany(drive => drive.Volumes).ToList();
            return volumes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DiskVolumesExplorer.Client.Components
{
    internal class DisksToVolumesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var disks = value as DiskViewModelCollection;
            if (disks == null) return null;

            var volumes = disks.SelectMany(disk => disk.Volumes as IEnumerable<VolumeViewModel>).ToList();
            return new VolumeViewModelCollection(volumes, disks.SelectedVolumeObserver);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

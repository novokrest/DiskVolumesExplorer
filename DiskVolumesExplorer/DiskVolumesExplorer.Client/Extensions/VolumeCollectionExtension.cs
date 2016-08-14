using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Linq;

namespace DiskVolumesExplorer.Client.Extensions
{
    internal static class VolumeCollectionExtension
    {
        public static IReadOnlyCollection<VolumeViewModel> ExtractVolumeViewModelCollection(this IVolumeCollection volumes)
        {
            return volumes.ExtractVolumeViewModels().ToList();
        }

        private static IEnumerable<VolumeViewModel> ExtractVolumeViewModels(this IVolumeCollection volumes)
        {
            foreach (IVolume volume in volumes)
            {
                yield return new VolumeViewModel(volume);
            }
        }
    }
}

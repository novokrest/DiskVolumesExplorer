using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Collections;

namespace DiskVolumesExplorer.Client.Services
{
    class VolumeDataCollection : IVolumeCollection
    {
        private readonly VolumeData[] _volumes;

        public VolumeDataCollection(VolumeData[] volumes)
        {
            _volumes = volumes;
        }

        public IVolume this[int index] => _volumes[index];

        public IEnumerator<IVolume> GetEnumerator()
        {
            return ((IEnumerable<VolumeData>)_volumes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

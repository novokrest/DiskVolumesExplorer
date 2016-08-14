using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Collections;

namespace DiskVolumesExplorer.Client.Services
{
    class DriveDataCollection : IDriveCollection
    {
        private DriveData[] _drives;

        public DriveDataCollection(DriveData[] drives)
        {
            _drives = drives;
        }

        public IDrive this[int index] => _drives[index];

        public IEnumerator<IDrive> GetEnumerator()
        {
            return ((IEnumerable<IDrive>)_drives).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

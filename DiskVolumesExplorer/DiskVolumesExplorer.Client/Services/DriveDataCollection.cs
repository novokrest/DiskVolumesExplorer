using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Collections;

namespace DiskVolumesExplorer.Client.Services
{
    class DriveDataCollection : IDiskCollection
    {
        private DriveData[] _drives;

        public DriveDataCollection(DriveData[] drives)
        {
            _drives = drives;
        }

        public IDisk this[int index] => _drives[index];

        public IEnumerator<IDisk> GetEnumerator()
        {
            return ((IEnumerable<IDisk>)_drives).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

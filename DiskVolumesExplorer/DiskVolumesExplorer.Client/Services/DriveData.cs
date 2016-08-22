using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client.Services
{
    public partial class DriveData : IDisk
    {
        private IVolumeCollection _volumeCollection;

        IVolumeCollection IDisk.Volumes
        {
            get
            {
                if (_volumeCollection == null)
                {
                    _volumeCollection = new VolumeDataCollection(Volumes);
                }
                return _volumeCollection;
            }
        }
    }
}

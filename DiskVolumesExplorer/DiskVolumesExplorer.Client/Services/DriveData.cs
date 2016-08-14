using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client.Services
{
    public partial class DriveData : IDrive
    {
        private IVolumeCollection _volumeCollection;

        IVolumeCollection IDrive.Volumes
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

using Prism.Mvvm;
using System;

namespace DiskVolumesExplorer.Client
{
    internal class SelectedVolumeViewModelNotifier : BindableBase
    {
        private VolumeViewModel _volumeViewModel;

        public SelectedVolumeViewModelNotifier()
        {

        }

        public event Action<VolumeViewModel> SelectedVolumeChanged;

        public VolumeViewModel SelectedVolume
        {
            get { return _volumeViewModel; }
            set
            {
                if (_volumeViewModel != value)
                {
                    _volumeViewModel = value;
                    OnSelectedViewModelChanged(value);
                }
            }
        }

        private void OnSelectedViewModelChanged(VolumeViewModel volume)
        {
            var handler = SelectedVolumeChanged;
            if (handler != null)
            {
                handler.Invoke(volume);
            }
        }
    }
}

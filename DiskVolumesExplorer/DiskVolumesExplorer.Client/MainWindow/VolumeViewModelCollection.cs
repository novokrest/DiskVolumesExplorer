using DiskVolumesExplorer.Core;
using Prism.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiskVolumesExplorer.Client
{
    internal class VolumeViewModelCollection : BindableBase, IReadOnlyCollection<VolumeViewModel>
    {
        private readonly IReadOnlyCollection<VolumeViewModel> _volumeViewModels;
        private readonly SelectedVolumeViewModelNotifier _selectedVolumeNotifier;
        private VolumeViewModel _selectedVolume;

        public VolumeViewModelCollection(IVolumeCollection volumes, SelectedVolumeViewModelNotifier selectedVolumeObserver)
            : this(volumes.Select(volume => new VolumeViewModel(volume)).ToList(), selectedVolumeObserver)
        {
        }

        public VolumeViewModelCollection(IReadOnlyCollection<VolumeViewModel> volumeViewModels, SelectedVolumeViewModelNotifier selectedVolumeNotifier)
        {
            _volumeViewModels = volumeViewModels;
            _selectedVolumeNotifier = selectedVolumeNotifier;
            _selectedVolumeNotifier.SelectedVolumeChanged += volume => SelectedVolume = volume;
        }

        public VolumeViewModel SelectedVolume
        {
            get { return _selectedVolume; }
            set
            {
                if (_selectedVolumeNotifier.SelectedVolume != value)
                {
                    _selectedVolumeNotifier.SelectedVolume = value;
                }

                if (_selectedVolume != value)
                {
                    _selectedVolume = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Count => _volumeViewModels.Count;

        public IEnumerator<VolumeViewModel> GetEnumerator()
        {
            return _volumeViewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

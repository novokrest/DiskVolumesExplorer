using DiskVolumesExplorer.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiskVolumesExplorer.Client
{
    internal class DiskViewModelCollection : IReadOnlyCollection<DiskViewModel>
    {
        public static readonly DiskViewModelCollection Empty = new DiskViewModelCollection();

        private readonly SelectedVolumeViewModelNotifier _selectedVolumeObserver = new SelectedVolumeViewModelNotifier();
        private readonly IReadOnlyCollection<DiskViewModel> _diskViewModels;

        private DiskViewModelCollection()
        {
            _diskViewModels = new DiskViewModel[0];
        }

        public DiskViewModelCollection(IDiskCollection diskCollection)
        {
            Verifiers.ArgNullVerify(diskCollection, nameof(diskCollection));
            _diskViewModels = diskCollection?.Select(disk => new DiskViewModel(disk, _selectedVolumeObserver)).ToArray()
                              ?? new DiskViewModel[0];
        }

        public SelectedVolumeViewModelNotifier SelectedVolumeObserver => _selectedVolumeObserver;

        public int Count => _diskViewModels.Count;

        public IEnumerator<DiskViewModel> GetEnumerator()
        {
            return _diskViewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

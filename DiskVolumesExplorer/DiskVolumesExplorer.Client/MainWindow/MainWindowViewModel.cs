using System.Collections.ObjectModel;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Core;
using Prism.Commands;
using Prism.Mvvm;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly IConnectionDialogService _connectionDialogService;
        private readonly DelegateCommand _showConnectionDialogCommand;

        public MainWindowViewModel(IConnectionDialogService connectionDialogService)
        {
            _connectionDialogService = connectionDialogService;
            _showConnectionDialogCommand = new DelegateCommand(ShowConnectionDialog);

            Volumes = new ObservableCollection<VolumeViewModel>()
            {
                new VolumeViewModel(),
                new VolumeViewModel(),
                new VolumeViewModel()
            };
        }

        public ObservableCollection<VolumeViewModel> Volumes { get; set; }


        public ICommand ShowConnectionDialogCommand => _showConnectionDialogCommand;

        private void ShowConnectionDialog()
        {
            var connectionConfig = new ConnectionConfig();
            _connectionDialogService.ShowConnectionDialog();
        }
    }
}

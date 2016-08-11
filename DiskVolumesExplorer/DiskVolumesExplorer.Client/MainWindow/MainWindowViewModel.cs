using System.Collections.ObjectModel;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Dialogs;
using Prism.Commands;
using Prism.Mvvm;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly IWindowCloseService _closeDialogService;
        private readonly IConnectionDialogService _connectionDialogService;
        private readonly DelegateCommand _showConnectionDialogCommand;

        public MainWindowViewModel(IWindowCloseService closeDialogService, IConnectionDialogService connectionDialogService)
        {
            _closeDialogService = closeDialogService;
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
            if (_connectionDialogService.ShowConnectionDialog() != true)
            {
                _closeDialogService.Close();
            }
        }
    }
}

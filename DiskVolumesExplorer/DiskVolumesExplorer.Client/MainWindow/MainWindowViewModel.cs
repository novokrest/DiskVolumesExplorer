using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Base;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;
using Prism.Commands;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : CleanUpViewModel
    {
        private readonly IWindowCloseService _closeDialogService;
        private readonly IConnectionDialogService _connectionDialogService;
        private readonly IVirtualMachineNamesLoader _virtualMachineNamesLoader;
        private readonly ICleanUpService _cleanUpService;

        private readonly DelegateCommand _showConnectionDialogCommand;
        private readonly DelegateCommand _closeCommand;

        private IReadOnlyCollection<string> _virtualMachineNames = Array.AsReadOnly(new string[] {});

        public MainWindowViewModel(IWindowCloseService closeDialogService, IConnectionDialogService connectionDialogService, IVirtualMachineNamesLoader virtualMachineNamesLoader, ICleanUpService cleanUpService)
        {
            _closeDialogService = closeDialogService;
            _connectionDialogService = connectionDialogService;
            _virtualMachineNamesLoader = virtualMachineNamesLoader;
            _cleanUpService= cleanUpService;

            _showConnectionDialogCommand = new DelegateCommand(ShowConnectionDialog);
            _closeCommand = new DelegateCommand(Close);
            CancelClose = true;

            Volumes = new ObservableCollection<VolumeViewModel>()
            {
                new VolumeViewModel(),
                new VolumeViewModel(),
                new VolumeViewModel()
            };
        }

        public IReadOnlyCollection<string> VirtualMachineNames
        {
            get { return _virtualMachineNames; }
            set
            {
                if (_virtualMachineNames != value)
                {
                    _virtualMachineNames = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VolumeViewModel> Volumes { get; set; }

        public ICommand ShowConnectionDialogCommand => _showConnectionDialogCommand;
        public ICommand CloseCommand => _closeCommand;

        private void ShowConnectionDialog()
        {
            if (_connectionDialogService.ShowConnectionDialog() != true)
            {
                Close();
            }

            LoadVirtualMachineNames();
        }

        private async void Close()
        {
            await CleanUpAsync();
            CancelClose = false;
            _closeDialogService.Close();
        }

        private async Task CleanUpAsync()
        {
            CleanUp = true;
            await _cleanUpService.CleanUpAsync();
            CleanUp = false;
        }

        private async void LoadVirtualMachineNames()
        {
            IReadOnlyCollection<string> virtualMachineNames = await _virtualMachineNamesLoader.LoadVirtualMachineNamesAsync();
            VirtualMachineNames = virtualMachineNames;
        }
    }
}

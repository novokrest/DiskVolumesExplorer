using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;
using Prism.Commands;
using Prism.Mvvm;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly IWindowCloseService _closeDialogService;
        private readonly IConnectionDialogService _connectionDialogService;
        private readonly IVirtualMachineNamesLoader _virtualMachineNamesLoader;
        private readonly DelegateCommand _showConnectionDialogCommand;

        private IReadOnlyCollection<string> _virtualMachineNames = Array.AsReadOnly(new string[] {});

        public MainWindowViewModel(IWindowCloseService closeDialogService, IConnectionDialogService connectionDialogService, IVirtualMachineNamesLoader virtualMachineNamesLoader)
        {
            _closeDialogService = closeDialogService;
            _connectionDialogService = connectionDialogService;
            _virtualMachineNamesLoader = virtualMachineNamesLoader;
            _showConnectionDialogCommand = new DelegateCommand(ShowConnectionDialog);

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

        private void ShowConnectionDialog()
        {
            if (_connectionDialogService.ShowConnectionDialog() != true)
            {
                _closeDialogService.Close();
            }

            LoadVirtualMachineNames();
        }

        private async void LoadVirtualMachineNames()
        {
            IReadOnlyCollection<string> virtualMachineNames = await _virtualMachineNamesLoader.LoadVirtualMachineNamesAsync();
            VirtualMachineNames = virtualMachineNames;
        }
    }
}

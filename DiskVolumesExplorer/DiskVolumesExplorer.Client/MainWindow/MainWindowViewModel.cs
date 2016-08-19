using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Base;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;
using Prism.Commands;
using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Client.Extensions;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : CleanUpViewModel
    {
        private readonly IWindowCloseService _closeDialogService;
        private readonly IConnectionDialogService _connectionDialogService;
        private readonly IVirtualMachineNamesLoader _virtualMachineNamesLoader;
        private readonly IVirtualMachineDisksLoader _virtualMachineDisksLoader;
        private readonly ICleanUpService _cleanUpService;

        private readonly DelegateCommand _closeCommand;
        private readonly DelegateCommand _loadVirtualMachinesCommand;
        private readonly DelegateCommand _loadVirtualMachineDisksCommand;

        private IReadOnlyList<string> _virtualMachineNames = Array.AsReadOnly(new string[] {});
        private int _selectedVirtualMachineIndex;
        private IReadOnlyCollection<DriveViewModel> _drives;
        private VolumeViewModel _selectedVolume;

        public MainWindowViewModel(IWindowCloseService closeDialogService, 
                                   IConnectionDialogService connectionDialogService, 
                                   IVirtualMachineNamesLoader virtualMachineNamesLoader,
                                   IVirtualMachineDisksLoader virtualMachineDisksLoader,
                                   ICleanUpService cleanUpService)
        {
            _closeDialogService = closeDialogService;
            _connectionDialogService = connectionDialogService;
            _virtualMachineNamesLoader = virtualMachineNamesLoader;
            _virtualMachineDisksLoader = virtualMachineDisksLoader;
            _cleanUpService= cleanUpService;

            _closeCommand = new DelegateCommand(Close);
            _loadVirtualMachinesCommand = new DelegateCommand(LoadVirtualMachines);
            _loadVirtualMachineDisksCommand = new DelegateCommand(LoadVirtualMachineDisks, CanLoadVirtualMachineDisks);

            CancelClose = true;
        }

        public IReadOnlyList<string> VirtualMachineNames
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

        public int SelectedVirtualMachineIndex
        {
            get { return _selectedVirtualMachineIndex; }
            set
            {
                if (_selectedVirtualMachineIndex != value)
                {
                    _selectedVirtualMachineIndex = value;
                    OnPropertyChanged();
                    if (_selectedVirtualMachineIndex != -1)
                    {
                        LoadVirtualMachineDisks();
                    }
                    
                }
            }
        }

        public IReadOnlyCollection<DriveViewModel> Drives
        {
            get { return _drives; }
            set
            {
                if (_drives != value)
                {
                    _drives = value;
                    OnPropertyChanged();
                }
            }
        }

        public VolumeViewModel SelectedVolume
        {
            get { return _selectedVolume; }
            set
            {
                if (_selectedVolume != value)
                {
                    _selectedVolume = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<VolumeViewModel> Volumes { get; set; }

        public ICommand LoadVirtualMachinesCommand => _loadVirtualMachinesCommand;
        public ICommand CloseCommand => _closeCommand;

        private void LoadVirtualMachines()
        {
            if (_connectionDialogService.ShowConnectionDialog() != true) return;
            LoadVirtualMachinesAsync();
        }

        private async void Close()
        {
            await CleanUpAsync();
            CancelClose = false;
            _closeDialogService.Close();
        }

        private async Task CleanUpAsync()
        {
            StartProcessing("Clean up...");
            await _cleanUpService.CleanUpAsync();
            StopProcessing();
        }

        private async void LoadVirtualMachinesAsync()
        {
            StartProcessing($"Loading virtual machine names...");
            IReadOnlyList<string> virtualMachineNames = await _virtualMachineNamesLoader.LoadVirtualMachineNamesAsync();
            VirtualMachineNames = virtualMachineNames;
            StopProcessing();
        }

        private async void LoadVirtualMachineDisks()
        {
            StartProcessing($"Loading disks and volumes for '{CurrentVirtualMachineName}'...");
            var disks = await LoadVirtualMachineDisksAsync();
            Drives = disks.ExtractDriveViewModelCollection();
            StopProcessing();
        }

        private string CurrentVirtualMachineName => _virtualMachineNames[_selectedVirtualMachineIndex];

        private Task<IDriveCollection> LoadVirtualMachineDisksAsync()
        {
            return _virtualMachineDisksLoader.LoadVirtualMachineDisks(_virtualMachineNames[_selectedVirtualMachineIndex]);
        }

        private bool CanLoadVirtualMachineDisks()
        {
            return _selectedVirtualMachineIndex != -1;
        }
    }
}

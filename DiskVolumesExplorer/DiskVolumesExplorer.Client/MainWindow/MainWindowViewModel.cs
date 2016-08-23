using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Base;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;
using Prism.Commands;
using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : CleanUpViewModel
    {
        private readonly IWindowCloseService _closeDialogService;
        private readonly IConnectionDialogService _connectionDialogService;
        private readonly IErrorDialogService _errorDialogService;
        private readonly IVirtualMachineNamesLoader _virtualMachineNamesLoader;
        private readonly IVirtualMachineDisksLoader _virtualMachineDisksLoader;
        private readonly ICleanUpService _cleanUpService;

        private readonly DelegateCommand _closeCommand;
        private readonly DelegateCommand _loadVirtualMachinesCommand;
        private readonly DelegateCommand _loadVirtualMachineDisksCommand;

        private IReadOnlyList<string> _virtualMachineNamesStub = Array.AsReadOnly(new string[0]);
        private IReadOnlyList<string> _virtualMachineNames; 
        private int _selectedVirtualMachineIndex;
        private DiskViewModelCollection _disksStub = DiskViewModelCollection.Empty;
        private DiskViewModelCollection _disks;
        private VolumeViewModel _selectedVolume;

        public MainWindowViewModel(IWindowCloseService closeDialogService, 
                                   IConnectionDialogService connectionDialogService, 
                                   IErrorDialogService errorDialogService,
                                   IVirtualMachineNamesLoader virtualMachineNamesLoader,
                                   IVirtualMachineDisksLoader virtualMachineDisksLoader,
                                   ICleanUpService cleanUpService)
        {
            _closeDialogService = closeDialogService;
            _connectionDialogService = connectionDialogService;
            _errorDialogService = errorDialogService;
            _virtualMachineNamesLoader = virtualMachineNamesLoader;
            _virtualMachineDisksLoader = virtualMachineDisksLoader;
            _cleanUpService= cleanUpService;

            _virtualMachineNames = _virtualMachineNamesStub;
            _disks = _disksStub;

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

        public DiskViewModelCollection Disks
        {
            get { return _disks; }
            set
            {
                if (_disks != value)
                {
                    _disks = value;
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

        public ICommand LoadVirtualMachinesCommand => _loadVirtualMachinesCommand;
        public ICommand CloseCommand => _closeCommand;

        private async void LoadVirtualMachines()
        {
            if (_connectionDialogService.ShowConnectionDialog() != true) return;
            VirtualMachineNames = _virtualMachineNamesStub;
            Disks = _disksStub;
            StartProcessing($"Loading virtual machine names...");
            await LoadVirtualMachinesAsync();
            StopProcessing();
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

        private async Task LoadVirtualMachinesAsync()
        {
            try
            {
                IReadOnlyList<string> virtualMachineNames = await _virtualMachineNamesLoader.LoadVirtualMachineNamesAsync();
                VirtualMachineNames = virtualMachineNames;
            }
            catch (Exception exception)
            {
                _errorDialogService.ShowErrorDialog(exception);
            }
        }

        private async void LoadVirtualMachineDisks()
        {
            StartProcessing($"Loading disks and volumes for '{CurrentVirtualMachineName}'...");
            await LoadVirtualMachineDisksAsync();
            StopProcessing();
        }

        private string CurrentVirtualMachineName => _virtualMachineNames[_selectedVirtualMachineIndex];

        private async Task LoadVirtualMachineDisksAsync()
        {
            try
            {
                IDiskCollection disks = await _virtualMachineDisksLoader.LoadVirtualMachineDisks(_virtualMachineNames[_selectedVirtualMachineIndex]);
                Disks = new DiskViewModelCollection(disks);
            }
            catch(Exception exception)
            {
                _errorDialogService.ShowErrorDialog(exception);
            }
        }

        private bool CanLoadVirtualMachineDisks()
        {
            return _selectedVirtualMachineIndex != -1;
        }
    }
}

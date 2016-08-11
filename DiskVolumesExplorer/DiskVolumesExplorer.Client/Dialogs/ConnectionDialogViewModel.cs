using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Base;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Core.Configs;
using Prism.Commands;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal class ConnectionDialogViewModel : ErrorViewModel
    {
        private readonly IHypervisorServiceConnector _hypervisorServiceConnector;
        private readonly ICloseDialogService _closeDialogService;

        private readonly DelegateCommand _connectCommand;
        private readonly DelegateCommand _closeCommand;

        private string _serverAddress;
        private string _userName;
        private SecureString _password;
        private bool _connecting;
        private bool _cancelClose;

        public ConnectionDialogViewModel(ICloseDialogService closeDialogService, IHypervisorServiceConnector hypervisorServiceConnector)
        {
            _closeDialogService = closeDialogService;
            _hypervisorServiceConnector = hypervisorServiceConnector;
            _connectCommand = new DelegateCommand(Connect, CanConnect);
            _closeCommand = new DelegateCommand(Close);
        }

        public ICommand ConnectCommand => _connectCommand;
        public ICommand CloseCommand => _closeCommand;

        public string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                if (!string.Equals(_serverAddress, value, StringComparison.Ordinal))
                {
                    _serverAddress = value;
                    _connectCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged();
                }
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (!string.Equals(_userName, value, StringComparison.Ordinal))
                {
                    _userName = value;
                    _connectCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged();
                }
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    _connectCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsConnecting
        {
            get { return _connecting; }
            set
            {
                if (_connecting != value)
                {
                    _connecting = value;
                    CancelClose = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CancelClose
        {
            get { return _cancelClose; }
            set
            {
                if (_cancelClose != value)
                {
                    _cancelClose = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(_serverAddress) && !string.IsNullOrEmpty(_userName) && _password != null;
        }

        private async void Connect()
        {
            DisableError();

            if (await ConnectAsync())
            {
                _closeDialogService.CloseDialog(true);
            }
            else
            {
                SetError("Failed to connect to server service");
            }
        }

        private async Task<bool> ConnectAsync()
        {
            IsConnecting = true;

            var connectionConfig = CreateConnectionConfig();
            bool connectResult = await _hypervisorServiceConnector.ConnectAsync(connectionConfig);

            IsConnecting = false;

            return connectResult;
        }

        private ISecureConnectionConfig CreateConnectionConfig()
        {
            return new SecureConnectionConfig
            {
                ServerAddress = _serverAddress,
                User = _userName,
                Password = _password
            };
        }

        private bool CanConnect()
        {
            return IsValid();
        }

        private void Close()
        {
            
        }
    }
}

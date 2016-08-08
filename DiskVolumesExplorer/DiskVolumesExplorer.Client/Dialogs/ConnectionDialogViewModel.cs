using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Base;
using DiskVolumesExplorer.Core;
using Prism.Commands;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal class ConnectionDialogViewModel : ErrorViewModel
    {
        private readonly IServerServiceConnector _serverServiceConnector;
        private readonly ICloseDialogService _closeDialogService;
        private readonly ConnectionConfig _connectionConfig;

        private readonly DelegateCommand _connectCommand;
        private readonly DelegateCommand _closeCommand;

        private string _serverAddress;
        private string _userName;
        private SecureString _password;
        private bool _connecting;
        private bool _cancelClose;

        public ConnectionDialogViewModel(ConnectionConfig connectionConfig, ICloseDialogService closeDialogService, IServerServiceConnector serverServiceConnector)
        {
            _connectionConfig = connectionConfig;
            _closeDialogService = closeDialogService;
            _serverServiceConnector = serverServiceConnector;
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
                    _connectionConfig.ServerAddress = _serverAddress;
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
                    _connectionConfig.UserName = _userName;
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
                    _connectionConfig.Password = _password;
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
                    _cancelClose = value;
                    _connecting = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CancelClose => _cancelClose;

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(_serverAddress) && !string.IsNullOrEmpty(_userName) && _password != null;
        }

        private async void Connect()
        {
            DisableError();

            if (await ConnectAsync())
            {
                _closeDialogService.Close(true);
            }
            else
            {
                SetError("Failed to connect to server service");
            }
        }

        private async Task<bool> ConnectAsync()
        {
            IsConnecting = true;
            bool connectResult = await _serverServiceConnector.ConnectAsync();
            IsConnecting = false;

            return connectResult;
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

using System;
using System.Security;
using System.Windows.Input;
using DiskVolumesExplorer.Client.Base;
using DiskVolumesExplorer.Core.Configs;
using Prism.Commands;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal class ConnectionDialogViewModel : ErrorViewModel
    {
        private readonly IConnectionConfigObserver _connectionConfigObserver;
        private readonly ICloseDialogService _closeDialogService;

        private readonly DelegateCommand _saveServerSettingsCommand;
        private readonly DelegateCommand _closeCommand;

        private string _serverAddress;
        private string _userName;
        private SecureString _password;

        public ConnectionDialogViewModel(ICloseDialogService closeDialogService, IConnectionConfigObserver connectionConfigObserver)
        {
            _closeDialogService = closeDialogService;
            _connectionConfigObserver = connectionConfigObserver;
            _saveServerSettingsCommand = new DelegateCommand(SaveServerSettings, CanSaveServerSettings);
            _closeCommand = new DelegateCommand(Close);

#if DEBUG
            _serverAddress = "localhost:8733";
            _userName = "user";
#endif
        }

        public ICommand SaveServerSettingsCommand => _saveServerSettingsCommand;
        public ICommand CloseCommand => _closeCommand;

        public string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                if (!string.Equals(_serverAddress, value, StringComparison.Ordinal))
                {
                    _serverAddress = value;
                    _saveServerSettingsCommand.RaiseCanExecuteChanged();
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
                    _saveServerSettingsCommand.RaiseCanExecuteChanged();
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
                    _saveServerSettingsCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged();
                }
            }
        }

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(_serverAddress) && !string.IsNullOrEmpty(_userName) && _password != null;
        }

        private void SaveServerSettings()
        {
            var connectionConfig = CreateConnectionConfig();
            _connectionConfigObserver.SetConnectionConfig(connectionConfig);
            _closeDialogService.CloseDialog(true);
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

        private bool CanSaveServerSettings()
        {
            return IsValid();
        }

        private void Close()
        {
            _closeDialogService.CloseDialog(false);
        }
    }
}

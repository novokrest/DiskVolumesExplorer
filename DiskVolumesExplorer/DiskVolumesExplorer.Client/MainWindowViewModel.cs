using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace DiskVolumesExplorer.Client
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly ServerServiceProvider _provider = new ServerServiceProvider();
        private readonly DelegateCommand _connectCommand;
        private string _connectionUrl;

        public MainWindowViewModel()
        {
            _connectCommand = new DelegateCommand(Connect, CanConnect);
        }

        public string ConnectionUrl
        {
            get { return _connectionUrl; }
            set
            {
                if (!string.Equals(_connectionUrl, value, StringComparison.Ordinal))
                {
                    _connectionUrl = value;
                    OnPropertyChanged();
                    _connectCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand ConnectCommand => _connectCommand;

        private void Connect()
        {
            _provider.Connect(ConnectionUrl);
        }

        private bool CanConnect()
        {
            return !string.IsNullOrEmpty(ConnectionUrl);
        }
    }
}

using System.Windows;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal interface IConnectionDialogService
    {
        bool? ShowConnectionDialog();
    }

    internal class ConnectionDialogService : ICloseDialogService, IConnectionDialogService
    {
        private readonly Window _owner;
        private readonly IConnectionConfigObserver _connectionConfigObserver;

        private ICloseDialogService _connectionDialogCloseService;
        private Window _connectionDialog;

        public ConnectionDialogService(Window owner, IConnectionConfigObserver connectionConfigObserver)
        {
            _owner = owner;
            _connectionConfigObserver = connectionConfigObserver;
        }

        public bool? ShowConnectionDialog()
        {
            CreateConnectionDialog();
            CreateConnectionDialogCloseService();

            return _connectionDialog.ShowDialog();
        }

        public void CloseDialog(bool? dialogResult)
        {
            _connectionDialogCloseService.CloseDialog(dialogResult);
        }

        private void CreateConnectionDialog()
        {
            var connectionDialog = new ConnectionDialog(_owner);
            var connectionDialogViewModel = CreateConnectionDialogViewModel();
            connectionDialog.DataContext = connectionDialogViewModel;

            _connectionDialog = connectionDialog;
        }

        private void CreateConnectionDialogCloseService()
        {
            _connectionDialogCloseService = new CloseDialogService(_connectionDialog);
        }

        private ConnectionDialogViewModel CreateConnectionDialogViewModel()
        {
            return new ConnectionDialogViewModel(this, _connectionConfigObserver);
        }
    }
}

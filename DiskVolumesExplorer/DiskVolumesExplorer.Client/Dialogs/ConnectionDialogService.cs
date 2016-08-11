using System.Windows;
using DiskVolumesExplorer.Client.Hypervisor;
using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal interface IConnectionDialogService
    {
        bool? ShowConnectionDialog();
    }

    internal class ConnectionDialogService : ICloseDialogService, IConnectionDialogService
    {
        private readonly Window _owner;
        private readonly IHypervisorServiceConnector _hypervisorServiceConnector;

        private ICloseDialogService _connectionDialogCloseService;
        private Window _connectionDialog;

        public ConnectionDialogService(Window owner, IHypervisorServiceConnector hypervisorServiceConnector)
        {
            _owner = owner;
            _hypervisorServiceConnector = hypervisorServiceConnector;
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
            return new ConnectionDialogViewModel(this, _hypervisorServiceConnector);
        }
    }
}

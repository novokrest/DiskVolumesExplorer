using System.Windows;
using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client.Dialogs
{
    public interface ICloseDialogService
    {
        void CloseDialog(bool result);
    }

    internal interface IConnectionDialogService
    {
        bool? ShowConnectionDialog();
    }

    internal class ConnectionDialogService : IConnectionDialogService, ICloseDialogService
    {
        private readonly Window _owner;
        private readonly IHypervisorServiceConnector _hypervisorServiceConnector;

        private Window _connectionDialog;

        public ConnectionDialogService(Window owner, IHypervisorServiceConnector hypervisorServiceConnector)
        {
            _owner = owner;
            _hypervisorServiceConnector = hypervisorServiceConnector;
        }

        public bool? ShowConnectionDialog()
        {
            _connectionDialog = CreateConnectionDialog();
            return _connectionDialog.ShowDialog();
        }

        public void CloseDialog(bool dialogResult)
        {
            _connectionDialog.DialogResult = dialogResult;
            _connectionDialog.Close();
        }

        private Window CreateConnectionDialog()
        {
            var connectionDialog = new ConnectionDialog(_owner);
            var connectionDialogViewModel = CreateConnectionDialogViewModel();
            connectionDialog.DataContext = connectionDialogViewModel;

            return connectionDialog;
        }

        private ConnectionDialogViewModel CreateConnectionDialogViewModel()
        {
            return new ConnectionDialogViewModel(this, _hypervisorServiceConnector);
        }
    }
}

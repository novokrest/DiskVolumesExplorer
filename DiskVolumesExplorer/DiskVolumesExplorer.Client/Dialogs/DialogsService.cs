using System.Windows;
using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal interface IDialogsService
    {
        bool? ShowConnectionDialog(ConnectionConfig connectionConfig);
    }

    internal class DialogsService : IDialogsService
    {
        private readonly Window _owner;
        private readonly IServerServiceConnector _serverServiceConnector;


        public DialogsService(Window owner, IServerServiceConnector serverServiceConnector)
        {
            _owner = owner;
            _serverServiceConnector = serverServiceConnector;
        }

        public bool? ShowConnectionDialog(ConnectionConfig connectionConfig)
        {
            var connectionDialog = new ConnectionDialog(_owner);
            var connectionDialogViewModel = CreateConnectionDialogViewModel(connectionConfig, connectionDialog);
            connectionDialog.DataContext = connectionDialogViewModel;
            return connectionDialog.ShowDialog();
        }

        private ConnectionDialogViewModel CreateConnectionDialogViewModel(ConnectionConfig connectionConfig, Window connectionDialogWindow)
        {
            return new ConnectionDialogViewModel(connectionConfig, new CloseDialogService(connectionDialogWindow), _serverServiceConnector);
        }
    }
}

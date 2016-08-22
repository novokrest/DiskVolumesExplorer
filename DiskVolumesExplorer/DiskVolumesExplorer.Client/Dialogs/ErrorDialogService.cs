using System;
using System.Windows;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal interface IErrorDialogService
    {
        void ShowErrorDialog(Exception exception);
    }

    internal class ErrorDialogService : IErrorDialogService
    {
        private readonly Window _owner;

        public ErrorDialogService(Window owner)
        {
            _owner = owner;
        }

        public void ShowErrorDialog(Exception exception)
        {
            MessageBox.Show(_owner, exception.Message);
        }
    }
}

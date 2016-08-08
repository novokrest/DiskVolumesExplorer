using System.Windows;

namespace DiskVolumesExplorer.Client.Dialogs
{
    public interface ICloseDialogService
    {
        void Close(bool result);
    }

    class CloseDialogService : ICloseDialogService
    {
        private readonly Window _window;

        public CloseDialogService(Window window)
        {
            _window = window;
        }

        public void Close(bool result)
        {
            _window.DialogResult = result;
            _window.Close();
        }
    }
}

using System.Windows;

namespace DiskVolumesExplorer.Client.Dialogs
{
    internal interface IWindowCloseService
    {
        void Close();
    }

    internal class WindowCloseService : IWindowCloseService
    {
        private readonly Window _window;

        public WindowCloseService(Window window)
        {
            _window = window;
        }

        public void Close()
        {
            _window.Close();
        }
    }

    internal interface ICloseDialogService
    {
        void CloseDialog(bool? dialogResult);
    }

    internal class CloseDialogService : ICloseDialogService
    {
        private readonly Window _window;
        private readonly WindowCloseService _windowCloseService;

        public CloseDialogService(Window window)
        {
            _window = window;
            _windowCloseService = new WindowCloseService(window);
        }

        public void CloseDialog(bool? dialogResult)
        {
            _window.DialogResult = dialogResult;
            _windowCloseService.Close();
        }
    }
}

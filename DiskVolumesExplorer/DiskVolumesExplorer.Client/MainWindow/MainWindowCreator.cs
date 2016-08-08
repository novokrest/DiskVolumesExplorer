using System.Windows;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Core.Mocks;

namespace DiskVolumesExplorer.Client
{
    internal static class MainWindowCreator
    {
        public static Window Create()
        {
            var serverServiceConnector = new MockServerServiceConnector();
            var mainWindow = new MainWindow();
            var dialogService = new DialogsService(mainWindow, serverServiceConnector);
            var mainWindowViewModel = new MainWindowViewModel(dialogService);
            mainWindow.DataContext = mainWindowViewModel;
            return mainWindow;
        }
    }
}

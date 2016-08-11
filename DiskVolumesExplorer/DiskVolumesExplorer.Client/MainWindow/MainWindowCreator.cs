using System.Windows;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;

namespace DiskVolumesExplorer.Client
{
    internal static class MainWindowCreator
    {
        public static Window Create()
        {
            var hypervisor = new Hypervisor.Hypervisor();
            var serverServiceConnector = new HypervisorServiceConnector(hypervisor);
            var virtualMachineNamesLoader = new VirtualMachineNamesLoader(serverServiceConnector);

            var mainWindow = new MainWindow();
            var mainWindowCloseService = new WindowCloseService(mainWindow);
            var connectionDialogService = new ConnectionDialogService(mainWindow, serverServiceConnector);
            var mainWindowViewModel = new MainWindowViewModel(mainWindowCloseService, connectionDialogService, virtualMachineNamesLoader);
            mainWindow.DataContext = mainWindowViewModel;

            return mainWindow;
        }
    }
}

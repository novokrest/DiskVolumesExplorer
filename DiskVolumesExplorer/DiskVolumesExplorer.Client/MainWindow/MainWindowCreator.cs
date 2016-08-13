using System.Windows;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;
using DiskVolumesExplorer.Client.Hypervisor.Mocks;

namespace DiskVolumesExplorer.Client
{
    internal static class MainWindowCreator
    {
        public static Window Create()
        {
            var serverServiceConnector = new MockHypervisorServiceConnector();
            var hypervisorServiceProvider = new MockHypervisorServiceProvider();
            var virtualMachineNamesLoader = new VirtualMachineNamesLoader(hypervisorServiceProvider);
            var virtualMachineDisksLoader = new VirtualMachineDisksLoader(hypervisorServiceProvider);
            var cleanupService = new MockCleanupService();

            var mainWindow = new MainWindow();
            var mainWindowCloseService = new WindowCloseService(mainWindow);
            var connectionDialogService = new ConnectionDialogService(mainWindow, serverServiceConnector);
            var mainWindowViewModel = new MainWindowViewModel(mainWindowCloseService, connectionDialogService, virtualMachineNamesLoader, virtualMachineDisksLoader, cleanupService);
            mainWindow.DataContext = mainWindowViewModel;

            return mainWindow;
        }
    }
}

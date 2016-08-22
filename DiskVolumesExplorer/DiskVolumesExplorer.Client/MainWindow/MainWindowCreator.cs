using System.Windows;
using DiskVolumesExplorer.Client.Dialogs;
using DiskVolumesExplorer.Client.Hypervisor;
using DiskVolumesExplorer.Client.Hypervisor.Mocks;
using DiskVolumesExplorer.Client.Hypervisor.Web;

namespace DiskVolumesExplorer.Client
{
    internal static class MainWindowCreator
    {
        public static Window Create()
        {
            var hypervisorServiceProvider = new WebHypervisorServiceProvider();
            var asyncHypervisorServiceProvider = new AsyncHypervisorServiceProviderAdapter(hypervisorServiceProvider);
            var virtualMachineNamesLoader = new VirtualMachineNamesLoader(asyncHypervisorServiceProvider);
            var virtualMachineDisksLoader = new VirtualMachineDisksLoader(asyncHypervisorServiceProvider);
            var cleanupService = new MockCleanupService();

            var mainWindow = new MainWindow();
            var mainWindowCloseService = new WindowCloseService(mainWindow);
            var connectionDialogService = new ConnectionDialogService(mainWindow, hypervisorServiceProvider);
            var errorDialogService = new ErrorDialogService(mainWindow);
            var mainWindowViewModel = new MainWindowViewModel(mainWindowCloseService, 
                                                              connectionDialogService, errorDialogService, 
                                                              virtualMachineNamesLoader, virtualMachineDisksLoader, 
                                                              cleanupService);
            mainWindow.DataContext = mainWindowViewModel;

            return mainWindow;
        }
    }
}

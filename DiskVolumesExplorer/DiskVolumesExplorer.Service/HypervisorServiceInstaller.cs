using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DiskVolumesExplorer.Service
{
    [RunInstaller(true)]
    public class HypervisorServiceInstaller : Installer
    {
        private ServiceProcessInstaller _processInstaller;
        private ServiceInstaller _serviceInstaller;

        public HypervisorServiceInstaller()
        {
            CreateProcessInstaller();
            CreateServiceInstaller();
            RegisterInstallers();
        }

        private void CreateProcessInstaller()
        {
            _processInstaller = new ServiceProcessInstaller();
            _processInstaller.Account = ServiceAccount.LocalSystem;
        }

        private void CreateServiceInstaller()
        {
            _serviceInstaller = new ServiceInstaller();
            _serviceInstaller.ServiceName = HypervisorWindowsService.HypervisorServiceName;
        }

        private void RegisterInstallers()
        {
            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}

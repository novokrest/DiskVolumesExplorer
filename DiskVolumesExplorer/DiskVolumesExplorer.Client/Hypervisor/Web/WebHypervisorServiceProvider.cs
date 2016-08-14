using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Client.Hypervisor.Web
{
    internal class WebHypervisorServiceProvider : IHypervisorServiceProvider, IConnectionConfigObserver
    {
        private ISecureConnectionConfig _connectionConfig;
        private IHypervisorService _hypervisorService;

        public IHypervisorService HypervisorService
        {
            get
            {
                if (_hypervisorService == null)
                {
                    _hypervisorService = CreateHypervisorService();
                }

                return _hypervisorService;
            }
        }

        public void SetConnectionConfig(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
            _hypervisorService = null;
        }

        private IHypervisorService CreateHypervisorService()
        {
            return new WebHypervisorService(_connectionConfig);
        }
    }
}

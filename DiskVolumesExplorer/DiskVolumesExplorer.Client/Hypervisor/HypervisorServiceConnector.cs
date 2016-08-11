using System;
using System.Threading.Tasks;
using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal class HypervisorServiceConnector : IHypervisorServiceConnector
    {
        private readonly IHypervisor _hypervisor;

        public HypervisorServiceConnector(IHypervisor hypervisor)
        {
            _hypervisor = hypervisor;
        }

        public async Task<bool> ConnectAsync(ISecureConnectionConfig connectionConfig)
        {
            try
            {
                await _hypervisor.ConnectAsync(connectionConfig);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task CancelConnectingAsync()
        {
            throw new NotImplementedException();
        }
    }
}

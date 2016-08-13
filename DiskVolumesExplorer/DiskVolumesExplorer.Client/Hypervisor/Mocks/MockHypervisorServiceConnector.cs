using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Client.Hypervisor.Mocks
{
    internal class MockHypervisorServiceConnector : IHypervisorServiceConnector
    {
        public Task<bool> ConnectAsync(ISecureConnectionConfig connectionConfig)
        {
            return DelayTasks.WithResult<bool>(3000, true);
        }

        public Task CancelConnectingAsync()
        {
            return Task.Delay(2000);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor.Mocks
{
    internal sealed class MockHypervisorServiceProvider : IHypervisorServiceProvider
    {
        public MockHypervisorServiceProvider()
        {
            HypervisorService = new MockHypervisorService();
        }

        public IHypervisorService HypervisorService { get; }
    }
}

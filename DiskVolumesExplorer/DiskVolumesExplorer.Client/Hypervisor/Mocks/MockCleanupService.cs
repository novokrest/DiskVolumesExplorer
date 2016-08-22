using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor.Mocks
{
    internal class MockCleanupService : ICleanUpService
    {
        public Task CleanUpAsync()
        {
            return Task.Delay(1000);
        }
    }
}

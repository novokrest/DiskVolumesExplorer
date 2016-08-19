using System;
using DiskVolumesExplorer.Service.Data;

namespace DiskVolumesExplorer.Service
{
    class HypervisorService : IHypervisorService
    {
        public DiskData[] GetDisks(string virtualMachineName)
        {
            throw new NotImplementedException();
        }

        public string[] GetVirtualMachines()
        {
            throw new NotImplementedException();
        }
    }
}

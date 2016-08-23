using System;
using DiskVolumesExplorer.Service.VmWare;
using System.Linq;
using DiskVolumesExplorer.Service.Configs.VmWare;
using DiskVolumesExplorer.Service.Core.Configs;
using DiskVolumesExplorer.Service.Core.Data;

namespace DiskVolumesExplorer.Service
{
    class HypervisorService : IHypervisorService
    {
        private readonly IVmWareConfigLoader _vmWareConfigLoader;
        private readonly Lazy<IVmWareConnectionConfig> _vmWareConnectionConfig;

        public HypervisorService(IVmWareConfigLoader vmWareConfigLoader)
        {
            _vmWareConfigLoader = vmWareConfigLoader;
            _vmWareConnectionConfig = new Lazy<IVmWareConnectionConfig>(LoadVmWareConnectionConfig);
        }

        public DiskData[] GetDisks(string virtualMachineName)
        {
            using (var vmWareHypervisor = new VmWareHypervisor(_vmWareConnectionConfig.Value))
            {
                //return vmWareHypervisor.GetDisks(virtualMachineName);
                return new Mocks.MockHypervisorService().GetDisks(virtualMachineName);
            }
        }

        public string[] GetVirtualMachines()
        {
            using (var vmWareHypervisor = new VmWareHypervisor(_vmWareConnectionConfig.Value))
            {
                return vmWareHypervisor.GetVirtualMachineNames().ToArray();
            }
        }

        private IVmWareConnectionConfig LoadVmWareConnectionConfig()
        {
            IVmWareConnectionConfig vmWareConfig = _vmWareConfigLoader.LoadVmWareConfig();
            return vmWareConfig;
        }
    }
}

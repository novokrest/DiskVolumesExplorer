using System;
using DiskVolumesExplorer.Service.Data;
using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Service.VmWare;
using System.Linq;
using DiskVolumesExplorer.Service.Configs.VmWare;
using DiskVolumesExplorer.Service.Extensions;
using DiskVolumesExplorer.Service.Mocks;

namespace DiskVolumesExplorer.Service
{
    class HypervisorService : IHypervisorService
    {
        private readonly IVmWareConfigLoader _vmWareConfigLoader;
        private readonly Lazy<ISecureConnectionConfig> _vmWareConnectionConfig;

        public HypervisorService(IVmWareConfigLoader vmWareConfigLoader)
        {
            _vmWareConfigLoader = vmWareConfigLoader;
            _vmWareConnectionConfig = new Lazy<ISecureConnectionConfig>(LoadVmWareConnectionConfig);
        }

        public DiskData[] GetDisks(string virtualMachineName)
        {
            using (var vmWareHypervisor = new VmWareHypervisor(_vmWareConnectionConfig.Value))
            {
                vmWareHypervisor.GetDisks(virtualMachineName);
            }
            return new MockHypervisorService().GetDisks(virtualMachineName);
        }

        public string[] GetVirtualMachines()
        {
            using (var vmWareHypervisor = new VmWareHypervisor(_vmWareConnectionConfig.Value))
            {
                return vmWareHypervisor.GetVirtualMachineNames().ToArray();
            }
        }

        private ISecureConnectionConfig LoadVmWareConnectionConfig()
        {
            IVmWareConnectionConfig vmWareConfig = _vmWareConfigLoader.LoadVmWareConfig();
            return vmWareConfig.ToSecureConnectionConfig();
        }
    }
}

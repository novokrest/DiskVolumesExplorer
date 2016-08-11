using System.Linq;
using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Core.Extensions;
using DiskVolumesExplorer.Core.Mocks;
using DiskVolumesExplorer.Core.VmWare;

namespace DiskVolumesExplorer.Service
{
    internal class HypervisorService : IHypervisorService
    {
        private VmWareHypervisor _hypervisor;

        public void Connect(ConnectionConfig connectionConfig)
        {
            _hypervisor = new VmWareHypervisor(new SecureConnectionConfig()
            {
                ServerAddress = connectionConfig.ServerAddress,
                User = connectionConfig.User,
                Password = connectionConfig.Password.ConvertToSecureString()
            });
            _hypervisor.Connect();
        }

        public string[] GetVirtualMachineNames()
        {
            return _hypervisor.GetVirtualMachineNames().ToArray();
        }

        public IVirtualMachine GetVirtualMachine(string name)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class MockHypervisorService : IHypervisorService
    {
        private static readonly string[] VirtualMachineNames =
        {
            "Virtual Machine #1",
            "Virtual Machine #2",
            "Virtual Machine #3",
            "Virtual Machine #4"
        };

        public void Connect(ConnectionConfig connectionConfig)
        {
            
        }

        public string[] GetVirtualMachineNames()
        {
            return VirtualMachineNames;
        }

        public IVirtualMachine GetVirtualMachine(string name)
        {
            return new MockVirtualMachine();
        }
    }
}

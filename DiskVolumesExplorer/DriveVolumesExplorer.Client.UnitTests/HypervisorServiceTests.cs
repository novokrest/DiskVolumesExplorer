using DiskVolumesExplorer.Client.Hypervisor;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;
using NUnit.Framework;

namespace DriveVolumesExplorer.Client.UnitTests
{
    [TestFixture]
    public class HypervisorServiceTests
    {
        private readonly ISecureConnectionConfig _validConnectionConfig = new SecureConnectionConfig
        {
            ServerAddress = "http://localhost:8733/Design_Time_Addresses/hypervisor"
        };

        private readonly ISecureConnectionConfig _invalidConnectionConfig = new SecureConnectionConfig
        {
            ServerAddress = "http://localhost:8733/Design_Time_Addresses/NONhypervisor"
        };

        [Test]
        public void TestHypervisorServiceConnection()
        {
            var hypervisorServiceProvider = CreateValidHypervisorServiceProvider();

            var hypervisorService = hypervisorServiceProvider.HypervisorService;

            Assert.NotNull(hypervisorService);
        }

        [Test]
        public void TestHypervisorServiceInvalidConnection()
        {
            var hypervisorSeviceProvider = CreateInvalidHypervisorServiceProvider();

            var hypervisorService = hypervisorSeviceProvider.HypervisorService;
        }

        [Test]
        public void TestHypervisorServiceGetVirtualMachines()
        {
            var webHypervisorService = new WebHypervisorService(_validConnectionConfig);

            string[] virtualMachineNames = webHypervisorService.GetVirtualMachines();

            Assert.IsTrue(virtualMachineNames.Length > 0);
        }

        [Test]
        public void TestHypervisorServiceGetDrives()
        {
            const string virtualMachine = "VirtualMachine";
            var webHypervisorService = new WebHypervisorService(_validConnectionConfig);

            DriveData[] drives = webHypervisorService.GetDrives(virtualMachine);

            Assert.IsTrue(drives.Length > 0);
        }

        private HypervisorServiceClientProvider CreateValidHypervisorServiceProvider()
        {
            return CreateHypervisorServiceProvier(_validConnectionConfig);
        }

        private HypervisorServiceClientProvider CreateInvalidHypervisorServiceProvider()
        {
            return CreateHypervisorServiceProvier(_invalidConnectionConfig);
        }

        private static HypervisorServiceClientProvider CreateHypervisorServiceProvier(ISecureConnectionConfig secureConnectionConfig)
        {
            return new HypervisorServiceClientProvider(secureConnectionConfig);
        }
    }
}

using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Core.Extensions;
using DiskVolumesExplorer.Core.VmWare;
using NUnit.Framework;

namespace DiskVolumesExplorer.Core.UnitTests
{
    [TestFixture]
    public class VmWareApiTests
    {
        [Test]
        public void GetVirtualMachinesTest()
        {
            var connectionConfig = CreateConnectionConfig();
            var hypervisor = new VmWareHypervisor(connectionConfig);
            hypervisor.Connect();

            var virtualMachineNames = hypervisor.GetVirtualMachineNames();

            Assert.IsTrue(virtualMachineNames.Count > 0);
        }

        [Test]
        public void ConnectToHypervisor()
        {
            var connectionConfig = CreateConnectionConfig();
            var hypervisor = new VmWareHypervisor(connectionConfig);

            hypervisor.Connect();

        }

        private ISecureConnectionConfig CreateConnectionConfig()
        {
            return new SecureConnectionConfig
            {
                ServerAddress = @"https://192.168.36.128/sdk",
                User = @"root",
                Password = @"1234567".ConvertToSecureString()
            };
        }
    }
}

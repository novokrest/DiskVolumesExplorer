using NUnit.Framework;
using Rhino.Mocks;
using DiskVolumesExplorer.Service.Configs.VmWare;

namespace DiskVolumesExplorer.Service.UnitTests
{
    [TestFixture]
    public class HypervisorServiceTests
    {
        [Test]
        public void TestGetVirtualMachines()
        {
            var hypervisorService = CreateHypervisorService();

            string[] virtualMachines = hypervisorService.GetVirtualMachines();

            Assert.IsTrue(virtualMachines.Length > 0 && virtualMachines[0] == "WindowsVM");
        }

        private HypervisorService CreateHypervisorService()
        {
            var vmWareConfig = MockRepository.GenerateStub<IVmWareConfig>();
            vmWareConfig.Expect(s => s.Url).Return("http://192.168.36.128/sdk");
            vmWareConfig.Expect(s => s.User).Return("root");
            vmWareConfig.Expect(s => s.Password).Return("1234567");

            var vmWareConfigLoader = MockRepository.GenerateStub<IVmWareConfigLoader>();
            vmWareConfigLoader.Expect(s => s.LoadVmWareConfig()).Return(vmWareConfig);

            var hypervisorService = new HypervisorService(vmWareConfigLoader);

            return hypervisorService;
        }
    }
}

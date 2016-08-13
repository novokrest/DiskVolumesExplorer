namespace DiskVolumesExplorer.Core.Mocks
{
    public static class MockVirtualMachinesRepository
    {
        public static IVirtualMachine GetVirtualMachine(string virtualMachineName)
        {
            return new MockVirtualMachine(virtualMachineName);
        }
    }
}

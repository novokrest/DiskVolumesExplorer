namespace DiskVolumesExplorer.Client.Hypervisor.Mocks
{
    internal sealed class MockAsyncHypervisorServiceProvider : IAsyncHypervisorServiceProvider
    {
        public MockAsyncHypervisorServiceProvider()
        {
            AsyncHypervisorService = new MockHypervisorService();
        }

        public IAsyncHypervisorService AsyncHypervisorService { get; }
    }
}

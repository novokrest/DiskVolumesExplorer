namespace DiskVolumesExplorer.Client.Hypervisor
{
    class AsyncHypervisorServiceProviderAdapter : IAsyncHypervisorServiceProvider
    {
        private readonly IHypervisorServiceProvider _provider;
        private IAsyncHypervisorService _asyncHypervisorService;

        public AsyncHypervisorServiceProviderAdapter(IHypervisorServiceProvider provider)
        {
            _provider = provider;
        }

        public IAsyncHypervisorService AsyncHypervisorService
        {
            get
            {
                if (_asyncHypervisorService == null)
                {
                    _asyncHypervisorService = new AsyncHypervisorService(_provider.HypervisorService);
                }
                return _asyncHypervisorService;
            }
        }
    }
}

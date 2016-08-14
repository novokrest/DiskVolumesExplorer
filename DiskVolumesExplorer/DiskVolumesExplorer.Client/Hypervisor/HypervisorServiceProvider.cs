using System;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IHypervisorServiceProvider
    {
        IHypervisorService HypervisorService { get; }
    }

    public class HypervisorServiceClientProvider : IHypervisorServiceProvider
    {
        private readonly ISecureConnectionConfig _connectionConfig;
        private readonly Lazy<HypervisorServiceClient> _hypervisorServiceClient;

        public HypervisorServiceClientProvider(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
            _hypervisorServiceClient = new Lazy<HypervisorServiceClient>(CreateHypervisorServiceClient);
        }

        private HypervisorServiceClient CreateHypervisorServiceClient()
        {
            var binding = new WebHttpBinding();
            var endpoint = new EndpointAddress(_connectionConfig.ServerAddress);
            var hypervisorServiceClient = new HypervisorServiceClient(binding, endpoint);
            hypervisorServiceClient.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());

            return hypervisorServiceClient;
        }

        public IHypervisorService HypervisorService => _hypervisorServiceClient.Value;
    }
}

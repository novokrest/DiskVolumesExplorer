using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;
using System.ServiceModel;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IAsyncHypervisorService
    {
        Task<IReadOnlyList<string>> GetVirtualMachinesAsync();
        Task<IDriveCollection> GetDrivesAsync(string virtualMachineName);
    }

    internal class AsyncHypervisorService : IAsyncHypervisorService
    {
        private readonly IHypervisorService _hypervisorService;

        public AsyncHypervisorService(IHypervisorService hypervisorService)
        {
            _hypervisorService = hypervisorService;
        }

        public Task<IDriveCollection> GetDrivesAsync(string virtualMachineName)
        {
            return Task.Run(() => GetDrives(virtualMachineName));
        }

        private IDriveCollection GetDrives(string virtualMachineName)
        {
            DriveData[] drives = _hypervisorService.GetDrives(virtualMachineName);
            return new DriveDataCollection(drives);
        }

        public Task<IReadOnlyList<string>> GetVirtualMachinesAsync()
        {
            return Task.Run((Func<IReadOnlyList<string>>)GetVirtualMachines);
        }

        private IReadOnlyList<string> GetVirtualMachines()
        {
            string[] virtualMachineNames = _hypervisorService.GetVirtualMachines();
            return virtualMachineNames;
        }
    }

    internal class AsyncHypervisorService1 : IAsyncHypervisorService
    {
        private readonly ISecureConnectionConfig _connectionConfig;
        private Lazy<HypervisorServiceClient> _hypervisorServiceClient;
        private HypervisorServiceClient HypervisorServiceClient => _hypervisorServiceClient.Value;

        public AsyncHypervisorService1(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
            _hypervisorServiceClient = new Lazy<HypervisorServiceClient>(CreateHypervisorServiceClient);
        }

        private HypervisorServiceClient CreateHypervisorServiceClient()
        {
            var binding = new WebHttpBinding(WebHttpSecurityMode.None);
            var endpoint = new EndpointAddress(_connectionConfig.ServerAddress);
            var hypervisorServiceClient = new HypervisorServiceClient(binding, endpoint);

            return hypervisorServiceClient;
        }

        public Task<IDriveCollection> GetDrivesAsync(string virtualMachineName)
        {
            return Task.Run(() => GetVirtualMachineDrives(virtualMachineName));
        }

        private IDriveCollection GetVirtualMachineDrives(string virtualMachineName)
        {
            DriveData[] drives = HypervisorServiceClient.GetDrives(virtualMachineName);
            return new DriveDataCollection(drives);
        }

        public Task<IReadOnlyList<string>> GetVirtualMachinesAsync()
        {
            return Task.Run((Func<IReadOnlyList<string>>)GetVirtualMachineNames);
        }

        private IReadOnlyList<string> GetVirtualMachineNames()
        {
            string[] virtualMachineNames = HypervisorServiceClient.GetVirtualMachines();
            return virtualMachineNames;
        }
    }
}

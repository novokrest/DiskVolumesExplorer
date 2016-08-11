using System;
using System.Threading.Tasks;
using DiskVolumesExplorer.Client.Extensions;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IHypervisor
    {
        Task ConnectAsync(ISecureConnectionConfig connectionConfig);
        Task<string[]> GetVirtualMachineNamesAsync();
    }

    internal class Hypervisor : IHypervisor
    {
        private HypervisorServiceClient _client;
        private ISecureConnectionConfig _connectionConfig;

        public Task ConnectAsync(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
            return Task.Run((Action) ConnectToService);
        }

        private void ConnectToService()
        {
            _client = new HypervisorServiceClient();
            _client.Connect(new ConnectionConfig()
            {
                ServerAddress = _connectionConfig.ServerAddress,
                User = _connectionConfig.User,
                Password = _connectionConfig.Password.ConvertToString()
            });
        }

        public Task<string[]> GetVirtualMachineNamesAsync()
        {
            throw new NotImplementedException();
        } 
    }
}

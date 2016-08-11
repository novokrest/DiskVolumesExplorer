using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiskVolumesExplorer.Client.Extensions;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IHypervisor
    {
        Task ConnectAsync(ISecureConnectionConfig connectionConfig);
        Task<IReadOnlyCollection<string>> GetVirtualMachineNamesAsync();
    }

    internal class Hypervisor : IHypervisor
    {
        private HypervisorServiceClient _hypervisorProxy;
        private ISecureConnectionConfig _connectionConfig;

        public Task ConnectAsync(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
            return Task.Run((Action) ConnectToService);
        }

        private void ConnectToService()
        {
            _hypervisorProxy = new HypervisorServiceClient();
            _hypervisorProxy.Connect(new ConnectionConfig
            {
                ServerAddress = _connectionConfig.ServerAddress,
                User = _connectionConfig.User,
                Password = _connectionConfig.Password.ConvertToString()
            });
        }

        public Task<IReadOnlyCollection<string>> GetVirtualMachineNamesAsync()
        {
            return Task.Run((Func<IReadOnlyCollection<string>>) GetVirtualMachineNames);
        }

        private IReadOnlyCollection<string> GetVirtualMachineNames()
        {
            return _hypervisorProxy.GetVirtualMachineNames();
        } 
    }
}

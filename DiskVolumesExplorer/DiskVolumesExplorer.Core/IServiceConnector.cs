using System.Threading.Tasks;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Core
{
    public interface IHypervisorServiceConnector
    {
        Task<bool> ConnectAsync(ISecureConnectionConfig connectionConfig);
        Task CancelConnectingAsync();
    }
}

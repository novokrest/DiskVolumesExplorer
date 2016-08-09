using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core
{
    public interface IHypervisorServiceConnector
    {
        Task<bool> ConnectAsync(IConnectionConfig connectionConfig);
        Task CancelConnectingAsync();
    }
}

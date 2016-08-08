using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core
{
    public interface IServerServiceConnector
    {
        Task<bool> ConnectAsync();
    }
}

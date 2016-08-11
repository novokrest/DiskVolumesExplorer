using System.Threading.Tasks;
using DiskVolumesExplorer.Core.Configs;

namespace DiskVolumesExplorer.Core.Mocks
{
    public class MockHypervisorServiceConnector : IHypervisorServiceConnector
    {
        private bool _result = true;

        public Task<bool> ConnectAsync(ISecureConnectionConfig connectionConfig)
        {
            _result = !_result;
            return Task.Delay(3000).ContinueWith(task => _result);
        }

        public Task CancelConnectingAsync()
        {
            return Task.Delay(3000);
        }
    }
}

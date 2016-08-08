using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core.Mocks
{
    public class MockServerServiceConnector : IServerServiceConnector
    {
        private bool _result = true;

        public Task<bool> ConnectAsync()
        {
            _result = !_result;
            return Task.Delay(3000).ContinueWith(task => _result);
        }
    }
}

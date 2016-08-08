using System.Security;

namespace DiskVolumesExplorer.Core
{
    public interface IConnectionConfig
    {
        string ServerAddress { get; }
        string UserName { get; }
        SecureString Password { get; }
    }

    public class ConnectionConfig : IConnectionConfig
    {
        public string ServerAddress { get; set; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
    }
}

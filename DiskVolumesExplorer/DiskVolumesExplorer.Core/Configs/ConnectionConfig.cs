using System.Security;

namespace DiskVolumesExplorer.Core.Configs
{
    public interface ISecureConnectionConfig
    {
        string ServerAddress { get; }
        string User { get; }
        SecureString Password { get; }
    }

    public class SecureConnectionConfig : ISecureConnectionConfig
    {
        public string ServerAddress { get; set; }
        public string User { get; set; }
        public SecureString Password { get; set; }
    }
}

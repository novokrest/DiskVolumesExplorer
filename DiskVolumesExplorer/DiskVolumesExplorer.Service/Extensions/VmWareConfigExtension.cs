using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Core.Extensions;
using DiskVolumesExplorer.Service.Core.Configs;

namespace DiskVolumesExplorer.Service.Extensions
{
    internal static class VmWareConfigExtension
    {
        public static ISecureConnectionConfig ToSecureConnectionConfig(this IVmWareConnectionConfig vmWareConfig)
        {
            return new SecureConnectionConfig
            {
                ServerAddress = vmWareConfig.Server,
                User = vmWareConfig.User,
                Password = vmWareConfig.Password.ConvertToSecureString()
            };
        }
    }
}

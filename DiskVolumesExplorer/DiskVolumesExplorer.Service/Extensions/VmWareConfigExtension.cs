using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Core.Extensions;
using DiskVolumesExplorer.Service.Configs.VmWare;

namespace DiskVolumesExplorer.Service.Extensions
{
    internal static class VmWareConfigExtension
    {
        public static ISecureConnectionConfig ToSecureConnectionConfig(this IVmWareConnectionConfig vmWareConfig)
        {
            return new SecureConnectionConfig
            {
                ServerAddress = vmWareConfig.Url,
                User = vmWareConfig.User,
                Password = vmWareConfig.Password.ConvertToSecureString()
            };
        }
    }
}

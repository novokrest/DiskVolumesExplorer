using DiskVolumesExplorer.Service.Core.Configs;
using System.Configuration;

namespace DiskVolumesExplorer.Service.Configs.VmWare
{
    internal interface IVmWareConfigLoader
    {
        IVmWareConnectionConfig LoadVmWareConfig();
    }

    internal class VmWareConfigLoader : IVmWareConfigLoader
    {
        public IVmWareConnectionConfig LoadVmWareConfig()
        {
            return (VmWareConfigSection)ConfigurationManager.GetSection(VmWareConfigSection.SectionName);
        }
    }
}

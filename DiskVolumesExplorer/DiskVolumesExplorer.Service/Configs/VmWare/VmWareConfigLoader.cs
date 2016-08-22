using System.Configuration;

namespace DiskVolumesExplorer.Service.Configs.VmWare
{
    internal interface IVmWareConfigLoader
    {
        IVmWareConfig LoadVmWareConfig();
    }

    internal class VmWareConfigLoader : IVmWareConfigLoader
    {
        public IVmWareConfig LoadVmWareConfig()
        {
            return (VmWareConfigSection)ConfigurationManager.GetSection(VmWareConfigSection.SectionName);
        }
    }
}

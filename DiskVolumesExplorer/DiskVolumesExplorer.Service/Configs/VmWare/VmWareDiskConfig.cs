using DiskVolumesExplorer.Service.Core.Configs;

namespace DiskVolumesExplorer.Service.Configs.VmWare
{
    internal class VmWareDiskConfig : IVmWareDiskConfig
    {
        public string Datacenter { get; set; }

        public string Datastore { get; set; }

        public string VirtualDiskFilePath { get; set; }

        public string VirtualMachineConfigFilePath { get; set; }
    }
}

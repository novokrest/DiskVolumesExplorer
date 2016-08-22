using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core.Configs
{
    public interface IVmWareDiskConfig
    {
        string Datacenter { get; }
        string Datastore { get; }
        string VirtualMachineConfigFilePath { get; }
        string VirtualDiskFilePath { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IHypervisorService
    {
        IReadOnlyCollection<string> GetVirtualMachineNames();

    }
}

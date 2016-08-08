using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core
{
    public interface IVirtualMachine
    {
        string Name { get; }
        //DiskCollection Disks { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Service.VmWare
{
    internal interface IVmWareConfig
    {
        string Url { get; }
        string User { get; }
        string Password { get; }
    }
}

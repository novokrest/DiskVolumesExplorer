using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core
{
    public interface IServerService
    {
        IReadOnlyCollection<IVirtualMachine> GetVirtualMachines();
    }

    class ServerService
    {
        public IReadOnlyCollection<IVirtualMachine> GetVirtualMachines()
        {
            throw new NotImplementedException();
        }

        public void LoadVirtualMachines()
        {

        }
    }
}

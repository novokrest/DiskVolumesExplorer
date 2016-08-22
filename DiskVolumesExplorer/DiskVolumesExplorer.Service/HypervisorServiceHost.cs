using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Service
{
    internal class HypervisorServiceHost : ServiceHost
    {

        public HypervisorServiceHost(Type serviceType)
            : base(serviceType)
        {

        }
    }
}

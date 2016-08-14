using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Core.Configs
{
    public interface IConnectionConfigObserver
    {
        void SetConnectionConfig(ISecureConnectionConfig connectionConfig);
    }
}

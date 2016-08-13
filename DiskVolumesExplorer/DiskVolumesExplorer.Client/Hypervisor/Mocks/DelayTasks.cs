using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor.Mocks
{
    public static class DelayTasks
    {
        public static Task<T> WithResult<T>(int milliseconds, T result = default(T))
        {
            return Task.Delay(milliseconds).ContinueWith(task => result);
        }
    }
}

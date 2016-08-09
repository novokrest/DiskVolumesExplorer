using System.ServiceModel;
using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Service
{
    [ServiceContract]
    public interface IHypervisorService
    {
        [OperationContract]
        string[] GetVirtualMachineNames();

        [OperationContract]
        IVirtualMachine GetVirtualMachine(string name);
    }
}

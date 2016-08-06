using System.ServiceModel;

namespace DiskVolumesExplorer.Service
{
    [ServiceContract(Name = "Hypervisor", Namespace = "VmWareServices")]
    public interface IHypervisor
    {
        [OperationContract]
        bool IsVirtualMachineExist(string virtualMachineName);
    }
}

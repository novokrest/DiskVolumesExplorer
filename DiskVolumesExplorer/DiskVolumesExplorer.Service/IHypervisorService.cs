using DiskVolumesExplorer.Service.Core.Data;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DiskVolumesExplorer.Service
{
    [ServiceContract]
    internal interface IHypervisorService
    {
        [OperationContract]
        [WebGet(UriTemplate = "vms", ResponseFormat = WebMessageFormat.Json)]
        string[] GetVirtualMachines();

        [OperationContract]
        [WebGet(UriTemplate = "disks/{virtualMachineName}", ResponseFormat = WebMessageFormat.Json)]
        DiskData[] GetDisks(string virtualMachineName);
    }
}

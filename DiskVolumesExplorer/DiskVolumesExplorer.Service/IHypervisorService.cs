using DiskVolumesExplorer.Service.Data;
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
        [WebGet(UriTemplate = "drives/{virtualMachineName}", ResponseFormat = WebMessageFormat.Json)]
        DriveData[] GetDrives(string virtualMachineName);
    }
}

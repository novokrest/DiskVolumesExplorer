using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DiskVolumesExplorer.Service
{
    [ServiceContract]
    internal interface IHypervisorService
    {
        [OperationContract]
        [WebGet(UriTemplate = "vm", ResponseFormat = WebMessageFormat.Json)]
        string[] GetVirtualMachines();

        [OperationContract]
        [WebGet(UriTemplate = "vm/{vmName}", ResponseFormat = WebMessageFormat.Json)]
        DriveData[] GetDrives(string vmName);
    }

    [DataContract]
    internal class DriveData
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public ulong SizeInBytes { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}

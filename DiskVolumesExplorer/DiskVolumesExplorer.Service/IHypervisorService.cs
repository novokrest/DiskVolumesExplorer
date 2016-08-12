using System.Runtime.Serialization;
using System.ServiceModel;
using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Service
{
    [ServiceContract]
    internal interface IHypervisorService
    {
        [OperationContract]
        void Connect(ConnectionConfig connectionConfig);

        [OperationContract]
        void Disconnect();

        [OperationContract]
        string[] GetVirtualMachineNames();

        [OperationContract]
        IVirtualMachine GetVirtualMachine(string name);
    }

    [DataContract]
    internal class ConnectionConfig
    {
        [DataMember]
        public string ServerAddress { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}

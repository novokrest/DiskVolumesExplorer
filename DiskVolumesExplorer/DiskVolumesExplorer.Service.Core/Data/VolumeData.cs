using System.Runtime.Serialization;

namespace DiskVolumesExplorer.Service.Core.Data
{
    [DataContract]
    public class VolumeData
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public ulong CapacityInBytes { get; set; }

        [DataMember]
        public ulong FreeSpaceInBytes { get; set; }

        [DataMember]
        public string FileSystem { get; set; }

        [DataMember]
        public string Status { get; set; }        
    }
}

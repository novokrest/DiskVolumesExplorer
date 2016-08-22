using System.Runtime.Serialization;

namespace DiskVolumesExplorer.Service.Core.Data
{
    [DataContract]
    public class DiskData
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public ulong SizeInBytes { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public VolumeData[] Volumes { get; set; }
    }
}

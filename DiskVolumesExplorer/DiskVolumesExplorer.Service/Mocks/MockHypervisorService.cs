using DiskVolumesExplorer.Service.Core.Data;

namespace DiskVolumesExplorer.Service.Mocks
{
    internal class MockHypervisorService : IHypervisorService
    {
        public string[] GetVirtualMachines()
        {
            return new[] 
            {
                "Virtual Machine #1",
                "Virtual Machine #2",
                "Virtual Machine #3",
                "Virtual Machine #4",
                "Virtual Machine #5",
            };
        }

        public DiskData[] GetDisks(string virtualMachineName)
        {
            return new[] 
            {
                new DiskData
                {
                    Title = "Disk 0", Type = "Basic", SizeInBytes = (ulong)471101*1024*1024, Status = "Online",
                    Volumes = new VolumeData[] 
                    {
                        new VolumeData { Title = "", CapacityInBytes = (ulong)200*1024*1024, FreeSpaceInBytes = (ulong)200*1024*1024, FileSystem = "HFS", Status = "Healthy (GPR Protective Partition)" },
                        new VolumeData { Title = "D:", CapacityInBytes = (ulong)620*1024*1024, FreeSpaceInBytes = (ulong)620*1024*1024, FileSystem = "HFS", Status = "Healthy (Primary Partition)" },
                        new VolumeData { Title = "C:", CapacityInBytes = (ulong)139233*1024*1024, FreeSpaceInBytes = (ulong)99873*1024*1024, FileSystem = "NTFS", Status = "Healthy (System, Boot)" },
                        new VolumeData { Title = "DATA (E:)", CapacityInBytes = (ulong)95221*1024*1024, FreeSpaceInBytes = (ulong)47032*1024*1024, FileSystem = "FAT32", Status = "Healthy (Primary Partition)" }
                    }
                },
                new DiskData
                {
                    Title = "CD-ROM 0", Type = "DVD", SizeInBytes = 0, Status = "No Media",
                    Volumes = new VolumeData[0]
                },
                new DiskData
                {
                    Title = "Disk 1", Type = "Optimal", SizeInBytes = (ulong)300110*1024*1024, Status = "Online",
                    Volumes = new VolumeData[]
                    {
                        new VolumeData { Title = "E:", CapacityInBytes = (ulong)200105*1024*1024, FreeSpaceInBytes = (ulong)34337*1024*1024, FileSystem = "NTFS", Status = "Healthy (Primary Partition)" },
                        new VolumeData { Title = "G:", CapacityInBytes = (ulong)100005*1024*1024, FreeSpaceInBytes = (ulong)33684*1024*1024, FileSystem = "NTFS", Status = "Healthy (Primary Partition)" },
                        new VolumeData { Title = "System Reserved", CapacityInBytes = (ulong)100*1024*1024, FreeSpaceInBytes = (ulong)72*1024*1024, FileSystem = "NTFS", Status = "Healthy (System, Active)" }
                    }
                }
            };
        }
    }
}

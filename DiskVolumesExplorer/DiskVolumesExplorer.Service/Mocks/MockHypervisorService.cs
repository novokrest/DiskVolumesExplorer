using DiskVolumesExplorer.Service.Data;

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

        public DriveData[] GetDrives(string virtualMachineName)
        {
            return new[] 
            {
                new DriveData
                {
                    Title = "Drive #1", Type = "Basic", SizeInBytes = 1024*1024*1024, Status = "Online",
                    Volumes = new VolumeData[] 
                    {
                        new VolumeData { Title = "Volume#1", CapacityInBytes = 1024*1024, FreeSpaceInBytes = 1024, FileSystem = "NTFS", Status = "Healthy" },
                        new VolumeData { Title = "Volume#2", CapacityInBytes = 1024*1024, FreeSpaceInBytes = 1024, FileSystem = "NTFS", Status = "Healthy" }
                    }
                },
                new DriveData
                {
                    Title = "Drive #2", Type = "Extended", SizeInBytes = ((ulong)3)*1024*1024*1024, Status = "Offline",
                    Volumes = new VolumeData[]
                    {
                        new VolumeData { Title = "Volume#1", CapacityInBytes = 1024*1024, FreeSpaceInBytes = 1024, FileSystem = "NTFS", Status = "Healthy" },
                        new VolumeData { Title = "Volume#2", CapacityInBytes = 1024*1024, FreeSpaceInBytes = 1024, FileSystem = "NTFS", Status = "Healthy" }
                    }
                },
                new DriveData
                {
                    Title = "Drive #3", Type = "Optimal", SizeInBytes = 500*1024*1024, Status = "Online",
                    Volumes = new VolumeData[]
                    {
                        new VolumeData { Title = "Volume#1", CapacityInBytes = 1024*1024, FreeSpaceInBytes = 1024, FileSystem = "NTFS", Status = "Healthy" },
                        new VolumeData { Title = "Volume#2", CapacityInBytes = 1024*1024, FreeSpaceInBytes = 1024, FileSystem = "NTFS", Status = "Healthy" }
                    }
                }
            };
        }
    }
}

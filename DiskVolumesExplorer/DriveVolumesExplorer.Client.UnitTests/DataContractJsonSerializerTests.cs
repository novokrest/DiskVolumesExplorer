using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using NUnit.Framework;
using DiskVolumesExplorer.Client.Services;

namespace DriveVolumesExplorer.Client.UnitTests
{
    [TestFixture]
    public class DataContractJsonSerializerTests
    {
        [Test]
        public void Test()
        {
            var aArray = new A[]
            {
                new A
            {
                Property1 = "A.Property1",
                Property2 = new B[]
                {
                    new B { Property1 = "B.Property1" },
                    new B { Property1 = "B.Property1" }
                }
            },
                new A
            {
                Property1 = "A.Property1",
                Property2 = new B[]
                {
                    new B { Property1 = "B.Property1" },
                    new B { Property1 = "B.Property1" }
                }
            }
            };

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(A[]));

            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, aArray);
                memoryStream.Position = 0;
                var streamReader = new StreamReader(memoryStream);
                string serializedString = streamReader.ReadToEnd();
                memoryStream.Position = 0;
                var aDeserialized = (A[])serializer.ReadObject(memoryStream);

                Assert.IsTrue(aDeserialized.Length > 0 && aDeserialized[0].Property2.Length > 0);
            }
            
        }

        [Test]
        public void Test2()
        {
            var drives = new[]
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

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DriveData[]));

            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, drives);
                memoryStream.Position = 0;
                var streamReader = new StreamReader(memoryStream);
                string serializedString = streamReader.ReadToEnd();
                memoryStream.Position = 0;
                var aDeserialized = (DriveData[])serializer.ReadObject(memoryStream);

                Assert.IsTrue(aDeserialized.Length > 0);
            }
        }
    }

    [DataContract]
    internal class A
    {
        [DataMember]
        public string Property1 { get; set; }

        [DataMember]
        public B[] Property2 { get; set; }
    }

    [DataContract]
    internal class B
    {
        [DataMember]
        public string Property1 { get; set; }
    }
}

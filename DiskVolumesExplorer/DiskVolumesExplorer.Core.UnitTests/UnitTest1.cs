using DiskVolumesExplorer.Core.Extensions;
using DiskVolumesExplorer.Core.VmWare;
using NUnit.Framework;

namespace DiskVolumesExplorer.Core.UnitTests
{
    [TestFixture]
    public class VmWareApiTests
    {
        [Test]
        public void Util()
        {
            string str1 = @"C:\VeeamFLR\Estonia - Valga _Windows Server 2012, SQL 2016__1bca323b\Volume1\Program Files\Microsoft SQL Server\MSSQL13.SQL2016E\MSSQL\DATA\FilestreamFilegroup1_File1\f2d4ac09-78a9-4632-b49c-07d54e322da4\a876ce1e-5a09-46ec-957e-6f00a801f137\00000029-000002a9-0002";
            string str2 = @"C:\VeeamFLR\Estonia - Valga _Windows Server 2012, SQL 2016__1bca323b\Volume1\Program Files\Microsoft SQL Server\MSSQL13.SQL2016E\MSSQL\DATA\FilestreamFilegroup1_File1\f79cb4b7-a6d9-42c6-8ece-cb469b44610a\f951b1e9-ae13-4e07-a4f6-89bd70ef6f69\00000029-000001d8-0002";
            string str3 = @"C:\VeeamFLR\Estonia - Valga _Windows Server 2012, SQL 2016__1bca323b\Volume1\Program Files\Microsoft SQL Server\MSSQL13.SQL2016E\MSSQL\DATA\FilestreamFilegroup1_File1\$FSLOG\ffffffd6-fffff744-fffe.00000029-000008bb-0001.aa8a76c7-fa96-4244-a4cc-3d59c07ab14c.0-0";
            string str4 = @"f79cb4b7-a6d9-42c6-8ece-cb469b44610a\f951b1e9-ae13-4e07-a4f6-89bd70ef6f69\00000029-000001d8-0002";
            Assert.True(str1.Length > 0 && str2.Length > 0 && str3.Length > 0 && str4.Length > 0);
        }

        [Test]
        public void GetVirtualMachinesTest()
        {
            var connectionConfig = CreateConnectionConfig();
            var hypervisor = new VmWareHypervisor(connectionConfig);
            hypervisor.Connect();

            var virtualMachineNames = hypervisor.GetVirtualMachineNames();

            Assert.IsTrue(virtualMachineNames.Count > 0);
        }

        [Test]
        public void ConnectToHypervisor()
        {
            var connectionConfig = CreateConnectionConfig();
            var hypervisor = new VmWareHypervisor(connectionConfig);

            hypervisor.Connect();

        }

        private IConnectionConfig CreateConnectionConfig()
        {
            return new ConnectionConfig
            {
                ServerAddress = @"https://esx24.dev.amust.local/sdk",
                User = @"root",
                Password = @"123qweASD".ConvertToSecureString()
            };
        }
    }
}

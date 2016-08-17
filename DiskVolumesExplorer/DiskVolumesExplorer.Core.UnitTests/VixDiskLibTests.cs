using DiskVolumesExplorer.Core.Native;
using NUnit.Framework;
using System;

namespace DiskVolumesExplorer.Core.UnitTests
{
    [TestFixture]
    public class VixDiskLibTests
    {
        [Test]
        public void TestCreateDiskVolumesManager()
        {
            DiskVolumesManager diskVolumesManager = null;
            try
            {
                diskVolumesManager = new DiskVolumesManager();
            }
            catch (Exception exception)
            {
                Verifiers.Verify(!string.IsNullOrEmpty(exception.Message));
            }
            finally
            {
                diskVolumesManager?.Dispose();
            }
        }

        [Test]
        public void TestDiskVolumesManagerConnect()
        {
            using (var diskVolumesManager = new DiskVolumesManager())
            {
                diskVolumesManager.Connect();
            }
        }
    }
}

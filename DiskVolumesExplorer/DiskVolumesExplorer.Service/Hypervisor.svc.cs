namespace DiskVolumesExplorer.Service
{
    public class Hypervisor : IHypervisor
    {
        public bool IsVirtualMachineExist(string virtualMachineName)
        {
            return virtualMachineName == "secret";
        }
    }
}

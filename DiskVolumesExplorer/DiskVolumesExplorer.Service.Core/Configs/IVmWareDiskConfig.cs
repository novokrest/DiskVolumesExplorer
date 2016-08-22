namespace DiskVolumesExplorer.Service.Core.Configs
{
    public interface IVmWareDiskConfig
    {
        string Datacenter { get; }
        string Datastore { get; }
        string VirtualMachineConfigFilePath { get; }
        string VirtualDiskFilePath { get; }
    }
}

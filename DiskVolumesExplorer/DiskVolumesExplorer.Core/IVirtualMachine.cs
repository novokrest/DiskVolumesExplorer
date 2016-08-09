namespace DiskVolumesExplorer.Core
{
    public interface IVirtualMachine
    {
        string Name { get; }
        IDiskCollection Disks { get; }
    }
}

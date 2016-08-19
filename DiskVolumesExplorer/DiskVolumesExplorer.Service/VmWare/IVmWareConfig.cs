namespace DiskVolumesExplorer.Service.VmWare
{
    internal interface IVmWareConfig
    {
        string Url { get; }
        string User { get; }
        string Password { get; }
    }
}

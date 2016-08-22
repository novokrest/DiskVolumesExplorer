namespace DiskVolumesExplorer.Core.Configs
{
    public interface IVmWareConnectionConfig
    {
        string Server { get; }
        string User { get; }
        string Password { get; }
        string ThumbPrint { get; }
    }
}

using System.Threading;

namespace DiskVolumesExplorer.Client
{
    internal class VolumeViewModel
    {
        private static int _counter;

        public VolumeViewModel()
        {
            int number = Interlocked.Increment(ref _counter);
            Name = $"Volume #{number}";
            Layout = "Simple";
            Icon = IconType.VolumeIcon;
        }

        public string Name { get; }
        public string Layout { get; }
        public IconType Icon { get; }

    }
}

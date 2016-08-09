using System.Threading;

namespace DiskVolumesExplorer.Core.Mocks
{
    abstract class CountableInstance
    {
        private static int _counter;

        protected CountableInstance()
        {
            Number = Interlocked.Increment(ref _counter);
        }

        protected int Number { get; }
    }
}

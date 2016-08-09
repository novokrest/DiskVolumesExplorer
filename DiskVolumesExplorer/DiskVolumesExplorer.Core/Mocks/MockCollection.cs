using System.Collections.Generic;

namespace DiskVolumesExplorer.Core.Mocks
{
    internal abstract class MockCollection<T> where T: new()
    {
        protected MockCollection(int elementsCount)
        {
            Elements = CreateElements(elementsCount);
        }

        private static List<T> CreateElements(int elements)
        {
            var volumes = new List<T>();
            for (int i = 0; i < elements; i++)
            {
                volumes.Add(new T());
            }

            return volumes;
        }

        protected IReadOnlyCollection<T> Elements { get; } 
    }
}

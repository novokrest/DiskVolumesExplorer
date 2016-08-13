using System;
using System.Collections;
using System.Collections.Generic;

namespace DiskVolumesExplorer.Core.Mocks
{
    internal abstract class MockCollection<T, IT> : IEnumerable<IT> where T: IT, new()
    {
        protected MockCollection(int elementsCount)
        {
            Elements = CreateElements(elementsCount);
        }

        private static List<IT> CreateElements(int elementsCount)
        {
            var elements = new List<IT>();
            for (int i = 0; i < elementsCount; i++)
            {
                elements.Add(new T());
            }

            return elements;
        }

        public IEnumerator<IT> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        protected IReadOnlyList<IT> Elements { get; } 
    }
}

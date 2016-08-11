namespace DiskVolumesExplorer.Core.UnitTests.Extensions
{
    public static class ArrayExtension
    {
        public static bool EqualsTo<T>(this T[] a1, T[] a2)
        {
            if (a1 == a2) return true;
            if (a1.Length != a2.Length) return false;

            for (int i = 0, count = a1.Length; i < count; i++)
            {
                if (!a1[i].Equals(a2[i])) return false;
            }

            return true;
        }
    }
}

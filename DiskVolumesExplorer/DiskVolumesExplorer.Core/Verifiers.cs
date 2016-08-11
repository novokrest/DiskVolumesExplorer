using System;

namespace DiskVolumesExplorer.Core
{
    public static class Verifiers
    {
        public static void Verify(bool b)
        {
            if (!b)
            {
                throw new Exception();
            }
        }

        public static void Verify(bool b, string message, params object[] args)
        {
            if (!b)
            {
                throw new Exception(string.Format(message, args));
            }
        }
    }
}

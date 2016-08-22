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

        public static void VerifyNotNull(object obj, string message, params object[] messageArgs)
        {
            if (obj == null)
            {
                throw new Exception(string.Format(message, messageArgs));
            }
        }

        public static void Verify(bool b, string message, params object[] args)
        {
            if (!b)
            {
                throw new Exception(string.Format(message, args));
            }
        }

        public static void ArgNullVerify(object arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        public static void ArgNullVerify(object arg, string argName, string message, params object[] messageArgs)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName, string.Format(message, messageArgs));
            }
        }
    }
}

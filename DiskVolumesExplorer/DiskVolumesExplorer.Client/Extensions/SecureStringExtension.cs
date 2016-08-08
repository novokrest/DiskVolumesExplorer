using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DiskVolumesExplorer.Client.Extensions
{
    internal static class SecureStringExtension
    {
        public static string ConvertToString(this SecureString secureString)
        {
            if (secureString == null) return string.Empty;

            IntPtr secureStringPtr = IntPtr.Zero;
            try
            {
                secureStringPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(secureStringPtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(secureStringPtr);
            }
        }
    }
}

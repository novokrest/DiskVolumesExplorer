using System.Security;

namespace DiskVolumesExplorer.Core.Extensions
{
    public static class StringExtension
    {
        public static SecureString ConvertToSecureString(this string str)
        {
            SecureString temp = null;
            try
            {
                temp = new SecureString();
                foreach (char c in str)
                {
                    temp.AppendChar(c);
                }

                SecureString result = temp;
                temp = null;

                return result;
            }
            finally
            {
                temp?.Dispose();
            }
        }
    }
}

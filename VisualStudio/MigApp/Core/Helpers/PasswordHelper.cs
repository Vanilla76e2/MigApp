using System.Security;

namespace MigApp.Helpers
{
    public static class PasswordHelper
    {
        public static string ConvertPasswordToString(SecureString? password)
        {
            if (password == null || password.Length == 0)
                return string.Empty;

            var ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }

        public static SecureString ConvertPasswordToSecureString(string? password)
        {
            if (string.IsNullOrEmpty(password)) return new SecureString();

            SecureString result = new SecureString();
            foreach (char c in password ?? string.Empty)
            {
                result.AppendChar(c);
            }
            return result;
        }
    }
}

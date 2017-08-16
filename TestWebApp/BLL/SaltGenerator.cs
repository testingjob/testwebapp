using System.Security.Cryptography;
using System.Text;

namespace TestWebApp.BLL
{
    public static class SaltGenerator
    {
        private static readonly RNGCryptoServiceProvider _cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        static SaltGenerator() {
            _cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString() {
            var saltBytes = new byte[SALT_SIZE];

            _cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            var saltString = Encoding.Unicode.GetString(saltBytes);

            return saltString;
        }
    }
}
using System.Security.Cryptography;
using System.Text;

namespace TestWebApp.BLL
{
    public class HashComputer
    {
        public string GetPasswordHashAndSalt(string message) {
            SHA256 sha = new SHA256CryptoServiceProvider();
            var dataBytes = Encoding.Unicode.GetBytes(message);
            var resultBytes = sha.ComputeHash(dataBytes);

            return Encoding.Unicode.GetString(resultBytes);
        }
    }
}
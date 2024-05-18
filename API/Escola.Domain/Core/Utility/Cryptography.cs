using System.Security.Cryptography;
using System.Text;

namespace Escola.Domain.Core.Utility
{
    public static class Cryptography
    {
        public static string GetSha256(string value)
        {
            var sb = new StringBuilder();

            using var sha256 = SHA256.Create();
            var enc = Encoding.UTF8;
            var result = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

            foreach (var b in result) sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}

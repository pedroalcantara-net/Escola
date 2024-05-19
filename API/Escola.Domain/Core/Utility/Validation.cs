using System.Text.RegularExpressions;

namespace Escola.Domain.Core.Utility
{
    public static class Validation
    {
        private static readonly string PasswordPattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*(),.?\":{}|<>])[A-Za-z0-9!@#$%^&*(),.?\":{}|<>]{8,}$";

        public static bool PasswordIsValid(string password) => Regex.IsMatch(password, PasswordPattern);
    }
}

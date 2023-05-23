using System;
using System.Security.Cryptography;
using System.Text;

namespace tabulator.Core
{
    public sealed class PasswordManager
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(salt);
            }
            return Encoding.UTF8.GetString(salt);
        }

        public static string HashPassword(string password, string storedSalt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] storedSaltBytes = Encoding.UTF8.GetBytes(storedSalt);
                byte[] combinedBytes = new byte[passwordBytes.Length + storedSaltBytes.Length];

                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(storedSaltBytes, 0, combinedBytes, passwordBytes.Length, storedSaltBytes.Length);

                byte[] hash = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string enteredHash = HashPassword(enteredPassword, storedSalt);
            return enteredHash.Equals(storedHash);
        }

    }
}

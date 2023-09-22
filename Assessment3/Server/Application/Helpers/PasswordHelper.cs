using System.Security.Cryptography;
using System.Text;

namespace Assessment3.Server.Application.Helpers;

public static class PasswordHelper
{
    public static (byte[], byte[]) HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Concatenate the salt with the plain-text password
        string saltedPassword = Convert.ToBase64String(salt) + password;

        // Hash the salted password
        byte[] hashedPassword;
        using (var sha256 = SHA256.Create())
        {
            hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        }

        return (salt, hashedPassword);
    }
    public static bool VerifyPassword(string password, byte[] salt, byte[] hashedPassword)
    {
        // Concatenate the stored salt with the input password
        string saltedPassword = Convert.ToBase64String(salt) + password;

        // Hash the salted password
        byte[] computedHashedPassword;
        using (var sha256 = SHA256.Create())
        {
            computedHashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        }

        // Compare the hashed password with the stored hashed password
        return computedHashedPassword.SequenceEqual(hashedPassword);
    }
}
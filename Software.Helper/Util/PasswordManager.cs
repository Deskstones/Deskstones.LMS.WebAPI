namespace Software.Helper.Util
{
    using System.Security.Cryptography;

    public static class PasswordManager
    {
        public static string HashPassword(string password)
        {
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] array = new byte[16];
            randomNumberGenerator.GetBytes(array);
            using Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, array, 10000, HashAlgorithmName.SHA256);
            byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
            string value = 10000.ToString();
            string value2 = Convert.ToBase64String(array);
            string value3 = Convert.ToBase64String(bytes);
            return $"{value}.{value2}.{value3}";
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            string[] array = hashedPassword.Split('.');
            if (array.Length != 3)
            {
                return false;
            }

            if (!int.TryParse(array[0], out var result))
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(array[1]);
            byte[] array2 = Convert.FromBase64String(array[2]);
            using Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(providedPassword, salt, result, HashAlgorithmName.SHA256);
            byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(array2, bytes);
        }
    }
}

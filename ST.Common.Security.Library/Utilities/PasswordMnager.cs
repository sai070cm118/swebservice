using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Utilities
{
    public static class PasswordMnager
    {

        public static byte[] generateSalt(int size)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[size]);
            return salt;
        }

        public static string hashPassword(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            return Convert.ToBase64String(hash);
        }
    }
}

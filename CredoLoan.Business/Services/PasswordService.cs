using System;
using System.Security.Cryptography;

namespace CredoLoan.Business {
    class PasswordService : IPasswordService {
        public string ComputeHash(string password) {
            byte[] salt = new byte[16];
            using (RNGCryptoServiceProvider rngProvider = new RNGCryptoServiceProvider()) {
                rngProvider.GetBytes(salt);
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000)) {
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    return Convert.ToBase64String(hashBytes);
                }  
            }
        }

        public bool Match(string password, string hash) {
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000)) {
                byte[] hs = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hs[i])
                        return false;
            }
            return true;
        }
    }
}

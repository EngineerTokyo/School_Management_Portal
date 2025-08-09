using System;
using System.Security.Cryptography;
using System.Text;

namespace StudentRecordApp
{
    public static class CryptoHelper
    {
        // Compute SHA256(salt + password) to compare with SQL HASHBYTES('SHA2_256', salt + password)
        public static byte[] ComputeHash(byte[] salt, string password)
        {
            if (salt == null) throw new ArgumentNullException(nameof(salt));
            if (password == null) password = string.Empty;

            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] combined = new byte[salt.Length + pwdBytes.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(pwdBytes, 0, combined, salt.Length, pwdBytes.Length);

            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(combined);
            }
        }

        // Convert varbinary from DB (object) to byte[]
        public static byte[] ToBytes(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value) return null;
            return (byte[])dbValue;
        }

        // Compare two byte arrays safely
        public static bool CompareBytes(byte[] a, byte[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;
            bool equal = true;
            for (int i = 0; i < a.Length; i++)
                equal &= (a[i] == b[i]);
            return equal;
        }
    }
}

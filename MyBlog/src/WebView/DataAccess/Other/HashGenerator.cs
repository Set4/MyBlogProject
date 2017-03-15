using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Other
{
    public static class HashGenerator
    {
        public static string GetSha256(string password)
        {
            using (var crypt = System.Security.Cryptography.SHA256.Create())
            {
                byte[] crypto = crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return System.Text.Encoding.UTF8.GetString(crypto, 0, crypto.Length);
            }
        }
    }
}

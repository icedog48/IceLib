using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Security.Cryptography
{
    public static class Encryption
    {
        public static string GenerateMD5Hash(string encryptionKey, string text)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(text);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);

                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return text;
        }

        public static string GenerateSHA1Hash(string signature) 
        {
            SHA1 hasher = SHA1.Create();

            ASCIIEncoding encoding = new ASCIIEncoding();

            var array = encoding.GetBytes(signature);
                array = hasher.ComputeHash(array);

            StringBuilder strHexa = new StringBuilder();

            foreach (byte item in array)
            {
                // Convertendo  para Hexadecimal
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString(); 
        }
    }
}

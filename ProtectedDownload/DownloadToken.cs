using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace ProtectedDownload
{
    public class DownloadToken
    {
        public static string generate(string email){
            byte[] dateTimeBytes = BitConverter.GetBytes(DateTime.Now.ToBinary()); // 8 bytes
           
            string password = "password";

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] emailBytes = Encoding.ASCII.GetBytes(email);

            int size = Math.Max(passwordBytes.Length + emailBytes.Length + dateTimeBytes.Length, 16);
            
            byte[] buffer = new byte[size];  

            Array.Copy(passwordBytes, 0, buffer, 0, passwordBytes.Length);
            Array.Copy(emailBytes, 0, buffer, passwordBytes.Length, emailBytes.Length);
            Array.Copy(dateTimeBytes, 0, buffer, passwordBytes.Length + emailBytes.Length, dateTimeBytes.Length);
            byte[] encryptedBuffer = new byte[256];
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                int count = sha1.TransformBlock(buffer, 0, buffer.Length, encryptedBuffer, 0);
                return Convert.ToBase64String(encryptedBuffer, 0, count);
            }

        }

        public static string decode(string id) {
            byte[] bytes = new byte[256];

            bytes = Convert.FromBase64String(id);

            string str = Convert.ToString(bytes);
            return str;
        }
    }
}
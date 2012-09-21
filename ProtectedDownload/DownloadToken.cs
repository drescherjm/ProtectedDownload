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
            return Guid.NewGuid().ToString("n");
        }

        public static string decode(string id) {
            byte[] bytes = new byte[256];

            bytes = Convert.FromBase64String(id);

            string str = Convert.ToString(bytes);
            return str;
        }
    }
}
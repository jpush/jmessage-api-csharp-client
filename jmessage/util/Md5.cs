using System;
using System.Text;
using System.Security.Cryptography;

namespace jmessage.util
{
    class Md5
    {
        public static String getMD5Hash(String str)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(str);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in hs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

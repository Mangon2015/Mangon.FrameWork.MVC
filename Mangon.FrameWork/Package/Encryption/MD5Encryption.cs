using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Encryption
{
    public class MD5Encryption
    {

        /// <summary>
        /// 获取md5值
        /// </summary>
        /// <param name="source">源文件</param>
        /// <param name="type">获取md5中的长度</param>
        /// <returns></returns>
        public static string GetMd5(string source, CodeOption type = CodeOption.Char32)
        {
            string hash = "";
            if (string.IsNullOrWhiteSpace(source))
            {
                return hash;
            }
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            }
            switch (type)
            {
                case CodeOption.Char8:
                    return hash.Substring(12, 8);
                case CodeOption.Char16:
                    return hash.Substring(8, 16);
                case CodeOption.Char32:
                default:
                    return hash;
            }

        }
        /// <summary>
        /// 获取md5值
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        /// <summary>
        ///  验证面md5值是否先等
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 验证内容的md5是否相等
        /// </summary>
        /// <param name="source">源文件</param>
        /// <param name="hash">md5值</param>
        /// <returns></returns>
        public static bool VerifyMd5(string source, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return VerifyMd5Hash(md5Hash, source, hash);
            }
        }
    }
    /// <summary>
    /// 加密位数
    /// </summary>
    public enum CodeOption
    {
        /// <summary>
        /// 8位
        /// </summary>
        Char8,
        /// <summary>
        /// 16位
        /// </summary>
        Char16,
        /// <summary>
        /// 32位
        /// </summary>
        Char32
    }

}

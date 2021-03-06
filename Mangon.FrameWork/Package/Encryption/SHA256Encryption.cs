﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Encryption
{
   public class SHA256Encryption
    {
        /// <summary>
        /// 获取md5值
        /// </summary>
        /// <param name="source">源文件</param>
        /// <param name="type">获取md5中的长度</param>
        /// <returns></returns>
        public static string GetSHA256(string source)
        {
            string hashstr = "";
            if (string.IsNullOrWhiteSpace(source))
            {
                return hashstr;
            }
            using (SHA256 hash = SHA256.Create())
            {
                hashstr = GetSHA256Hash(hash, source);
            }
            return hashstr;
        }
        /// <summary>
        /// 获取md5值
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetSHA256Hash(SHA256 hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

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
        private static bool VerifySHA256Hash(SHA256 SHA1Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetSHA256Hash(SHA1Hash, input);

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
        public static bool VerifySHA256(string source, string hash)
        {
            using (SHA256 md5Hash = SHA256.Create())
            {
                return VerifySHA256Hash(md5Hash, source, hash);
            }
        }
    }
}

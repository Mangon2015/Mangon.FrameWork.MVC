using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
 * Unicode和汉字编码小知识
　　将汉字进行UNICODE编码，如：“王”编码后就成了“\王”，UNICODE字符以\u开始，后面有4个数字或者字母，所有字符都是16进制的数字，每两位表示的256以内的一个数字。而一个汉字是由两个字符组成，于是就很容易理解了，“738b”是两个字符，分别是“73”“8b”。但是在将 UNICODE字符编码的内容转换为汉字的时候，字符是从后面向前处理的，所以，需要把字符按照顺序“8b”“73”进行组合得到汉字。
 * */
namespace Mangon.FrameWork.Package.Encode
{
   public class Unicode
    {
       /// <summary>
       /// 获取字符串的Unicode编码
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public static string ToUnicode(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2)
            {
                r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');

            }
            return r;
        }
       /// <summary>
       /// 根据Unicode编码获取字符串
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public static string ToStringFromUnicode(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }
       /// <summary>
       /// 字符串转二进制
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public static string StringToBinary(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str);
            StringBuilder sb = new StringBuilder(data.Length * 8);
            foreach (byte item in data)
            {
                sb.Append(Convert.ToString(item, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
       /// <summary>
       /// 二进制转字符串
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public static string BinaryToString(string input)
        {
            StringBuilder sb = new StringBuilder();
            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; i++)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }
            return Encoding.Unicode.GetString(bytes);
        }

    }
}

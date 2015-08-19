using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
   public static  class Mangon_FrameWorkSystemExtension_String
    {

        public static int ToInt(this string str)
        {
            return str.ToInt(0);
        }
        public static int ToInt(this string str, int defaultValue)
        {
            int d;
            if (int.TryParse(str, out d))
            {
                return d;
            }
            return defaultValue;
        }
        /// <summary>
        /// 转义到int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Bool"></param>
        /// <returns></returns>
        public static int ToInt(this string str, out bool Bool)
        {
            int i = 0;
            Bool = int.TryParse(str, out i);
            return i;
        }

        public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct
        {

            T tenum;
            Enum.TryParse<T>(value, ignoreCase, out tenum);
            return tenum;

        }
        public static bool IsEmpty(this string str)
        {
            return IsEmpty(str, true);
        }
        public static bool IsEmpty(this string str, bool WhiteSpace)
        {
            if (WhiteSpace)
            {
                return string.IsNullOrWhiteSpace(str);
            }
            else
            {
                return string.IsNullOrEmpty(str);
            }
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="begin">开始位/或者倒数位</param>
        /// <returns></returns>
        public static string Substr(this string str, int index)
        {

            if (index > 0)
            {
                index = str.Length >= index ? index : str.Length;
                return substr_Begin(str, index);
            }
            else if (index < 0)
            {
                index = str.Length >= Math.Abs(index) ? Math.Abs(index) : str.Length;
                return substr_End(str, 0 - index);
            }
            else
            {
                return str;
            }

        }

        /// <summary>
        /// 正序
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string substr_Begin(string str, int index, int Long)
        {
            return str.Substring(index, Long);
        }
        /// <summary>
        /// 倒叙
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string substr_End(string str, int index, int End)
        {



            return str.Substring(index, str.Length - index + End);
        }


        /// <summary>
        /// 正序
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string substr_Begin(string str, int index)
        {
            return str.Substring(index);
        }
        /// <summary>
        /// 倒叙
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string substr_End(string str, int index)
        {
            return str.Substring(0, str.Length + index);
        }


        public static string Substr(this string str, int begin, int longOrEnd)
        {
            if (str == null || begin < 0 || str.Length == 0 || begin >= str.Length) return "";
            if (longOrEnd > 0)
            {
                if (longOrEnd > str.Length - begin)//如果长度超长
                {
                    longOrEnd = str.Length - begin;//限制为最小
                }


                return substr_Begin(str, begin, longOrEnd);
            }
            else if (longOrEnd < 0)
            {

                if ((str.Length - begin + longOrEnd) < 0) return "";//计算非法
                return substr_End(str, begin, longOrEnd);
            }
            else
            {
                return str.Substring(begin);
            }
        }
        /// <summary>
        /// 附加结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <param name="Append"></param>
        /// <returns></returns>
        public static string Substr(this string str, int index, string Append)
        {
            return Substr(str, index, Append, true);
        }
        public static string Substr(this string str, int begin, int longOrEnd, string Append)
        {
            string newstr = str.Substr(begin, longOrEnd);
            if (newstr != "" && str.Length > Math.Abs(longOrEnd))
            {
                return newstr + Append;
            }
            else
            {
                return newstr;
            }
        }

        public static string Substr(this string str, int index, string Append, bool AppendInSub)
        {
            string newstr = str.Substr(index);

            if (AppendInSub && str.Length < Math.Abs(index))
            {
                return newstr + Append;
            }
            else
            {
                return newstr;
            }
        }
        public static string Short(this string str, int Long, string pan)
        {
            if (pan == null) pan = "";
            if (str == null) str = "";
            if (str.Length <= Long) return str;
            else
            {
                str = str.Substr(0, Long);
                return str + pan;
            }
        }
        public static string[] Cut(this string str, int Long)
        {
            if (string.IsNullOrEmpty(str)) return new string[0];

            if (str.Length <= Long) return new string[1] { str };
            int Count = str.Length / Long;
            List<string> temp = new List<string>(Count);

            for (int i = 0; i < Count; i++)
            {

                temp.Add(str.Substr(i * Long, Long));
                // i = i + Long;
            }

            return temp.ToArray();
        }

        public static string JoinIn(this string str, int Long, string pan)
        {
            if (string.IsNullOrEmpty(str)) return "";
            if (str.Length <= Long) return str + pan;

            string[] data = str.Cut(Long);
            StringBuilder sb = new StringBuilder();
            foreach (var s in data)
            {
                sb.AppendFormat("{0}{1}", s, pan);
            }
            return sb.ToString();
        }

        public static string Md5(this string str)
        {
            return Mangon.FrameWork.Package.Encryption.MD5Encryption.GetMd5(str);
        }
    }
}

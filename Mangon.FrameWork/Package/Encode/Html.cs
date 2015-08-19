using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mangon.FrameWork.Package.Encode
{
    public class Html
    {
        public enum HtmlEncode
        { 
            /// <summary>
            /// 全部编码
            /// </summary>
            All,
            /// <summary>
            /// 默认设置
            /// </summary>
            Normal,
            /// <summary>
            /// 只编码js
            /// </summary>
            JavaScript,
            /// <summary>
            /// 把一般文本转义成分行显示,主要只转义  \n 为 br
            /// </summary>
            View,
            Remove
        }

        public static string Remove(string html)
        {

            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decode(string text)
        {
            return HttpUtility.HtmlDecode(text);
        }
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Encode(string text, HtmlEncode encode)
        {
            switch (encode)
            {
                case HtmlEncode.All:
                    return All(text);
                case HtmlEncode.JavaScript:
                    return Javascript(text);
                    
                case HtmlEncode.View:
                   return View(text);
                    
                case HtmlEncode.Remove:
                   return Remove(text);
                case HtmlEncode.Normal:
                default:
                   return Normal(text);
            }
        }
        /// <summary>
        /// 默认转义
        /// </summary>
        /// <param name="text">正文</param>
        /// <returns>编码结果</returns>
        private static string Normal(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }
        /// <summary>
        /// 全部转义,逐一字符转义
        /// </summary>
        /// <param name="text">正文</param>
        /// <returns></returns>
        private static string All(string text)
        {
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            StringBuilder sb = new StringBuilder(text.Length + (int)(text.Length * 0.1));
            foreach (var item in chars)
            {
                int value = Convert.ToInt32(item);
                if (value>127)
                {
                    sb.AppendFormat("&#{0};", value);
                }
                else
                {
                    sb.Append(value);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 转义成js匹配字符,除去  a~z  0~9 其他转义以\uxxxx格式字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Javascript(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            StringBuilder sb = null;
            int startIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')))
                {
                    //非字母或数字，则直接转换为\uxxxx格式字符
                    if (sb == null)
                    {
                        sb = new StringBuilder(text.Length + 6);
                    }
                    if (i > startIndex)
                    {
                        sb.Append(text, startIndex, i - startIndex);
                    }
                    startIndex = i + 1;

                    sb.Append(@"\u");
                    sb.Append(((int)c).ToString("x4", CultureInfo.InvariantCulture));
                }
                else if (sb != null)
                {
                    sb.Append(c);
                    startIndex = i + 1;
                }
            }
            if (sb == null) return text;
            return sb.ToString();
        }
        /// <summary>
        /// 把一般文本转义成分行显示,主要只转义  \n 为 br
        /// </summary>
        /// <param name="text">正文</param>
        /// <returns>数据结果</returns>
        private static string View(string text)
        {
            return HttpUtility.HtmlEncode(text).Replace("\n", "<br />");
        }
    }
}

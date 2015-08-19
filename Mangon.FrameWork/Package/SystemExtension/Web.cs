using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.SystemExtension
{
    public static class Web
    {
        public enum UrlEncodeType { HtmlEncode, UrlEncode, NoEncode };
        /// <summary>
        /// 生成UrlParameters
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public static string ToUrl(this System.Collections.Specialized.NameValueCollection Collection, UrlEncodeType encodeType, ICollection<string> UnUseList, Encoding encoding)
        {
            UnUseList = (ICollection<string>)UnUseList.Tolower();

            if (Collection.Count == 0) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("?");

            foreach (var ckey in Collection.AllKeys)
            {
                var Key = ckey.ToLower();
                if (UnUseList.Contains(Key)) continue;
                if (Collection[Key] != null)
                {
                    switch (encodeType)
                    {
                        case UrlEncodeType.HtmlEncode:
                            sb.AppendFormat("{0}={1}&", Key, System.Web.HttpUtility.HtmlEncode(Collection[Key].ToString()));
                            break;
                        case UrlEncodeType.UrlEncode:
                            if (encoding != null)
                            {
                                sb.AppendFormat("{0}={1}&", Key, System.Web.HttpUtility.UrlEncode(Collection[Key].ToString(), encoding));
                            }
                            else
                            {
                                sb.AppendFormat("{0}={1}&", Key, System.Web.HttpUtility.UrlEncode(Collection[Key].ToString()));
                            }
                            break;
                        case UrlEncodeType.NoEncode:
                            sb.AppendFormat("{0}={1}&", Key, Collection[Key]);
                            break;
                    }


                }
            }
            string output = sb.ToString();

            return output;
        }

        public static string ToUrl(this System.Collections.Specialized.NameValueCollection Collection)
        {
            return ToUrl(Collection, UrlEncodeType.UrlEncode, null, null);
        }

        public static string ToUrl(this System.Collections.Specialized.NameValueCollection Collection, UrlEncodeType encodeType)
        {
            return ToUrl(Collection, encodeType, null, null);
        }



        public static string ToUrlFormat(this System.Collections.Specialized.NameValueCollection Collection, IDictionary<string, string> FormatList, UrlEncodeType encodeType, Encoding encoding, bool KeepEmpty, out Dictionary<string, string> OverDict)
        {

            if (FormatList == null) FormatList = new Dictionary<string, string>();
            //首先获取全部Key          
            IEnumerable<string> FormatKey = FormatList.Keys.Tolower();
            OverDict = new Dictionary<string, string>();
            //把输入字典传进过滤字典里面,重复的不叠加
            foreach (var ckey in Collection.AllKeys.Reverse())
            {
                if (ckey == null) continue;
                if (!FormatKey.Contains(ckey.ToLower()))
                {
                    FormatList.Add(ckey, Collection[ckey]);
                }
                else
                {
                    OverDict.Add(ckey, Collection[ckey]);
                }
            }

            //倒转序列
            var temp = FormatList.Reverse();


            StringBuilder sb = new StringBuilder();
            sb.Append("?");
            if (temp.Count() == 0) return "";
            foreach (var item in temp)
            {
                if (item.Value == null)
                {
                    if (!KeepEmpty)
                    {
                        sb.AppendFormat("{0}=&", item.Key);
                    }
                    continue;
                }
                if (item.Value.IndexOf("{0}") >= 0)
                {
                    sb.AppendFormat("{0}={1}&", item.Key, item.Value.ToString()); continue;
                }
                switch (encodeType)
                {
                    case UrlEncodeType.HtmlEncode:
                        sb.AppendFormat("{0}={1}&", item.Key, System.Web.HttpUtility.HtmlEncode(item.Value.ToString()));
                        break;
                    case UrlEncodeType.UrlEncode:
                        if (encoding != null)
                        {
                            sb.AppendFormat("{0}={1}&", item.Key, System.Web.HttpUtility.UrlEncode(item.Value.ToString(), encoding));
                        }
                        else
                        {
                            sb.AppendFormat("{0}={1}&", item.Key, System.Web.HttpUtility.UrlEncode(item.Value.ToString()));
                        }
                        break;
                    case UrlEncodeType.NoEncode:
                        sb.AppendFormat("{0}={1}&", item.Key, item.Value.ToString());
                        break;

                }
            }
            return sb.ToString();

        }
        public static string ToUrlFormat(this System.Collections.Specialized.NameValueCollection Collection, IDictionary<string, string> FormatList, UrlEncodeType encodeType, Encoding encoding)
        {
            Dictionary<string, string> outs = null;
            return Collection.ToUrlFormat(FormatList, encodeType, encoding, false, out outs);
        }
        public static string ToUrlFormat(this System.Collections.Specialized.NameValueCollection Collection, IDictionary<string, string> FormatList, UrlEncodeType encodeType)
        {
            return Collection.ToUrlFormat(FormatList, encodeType, Encoding.UTF8);
        }
        public static string ToUrlFormat(this System.Collections.Specialized.NameValueCollection Collection, IDictionary<string, string> FormatList)
        {
            return Collection.ToUrlFormat(FormatList, UrlEncodeType.UrlEncode);
        }
    }
}

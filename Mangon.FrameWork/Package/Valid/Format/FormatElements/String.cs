using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Mangon.FrameWork.Result;


namespace  Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 
    /// </summary>
   public class String:FormatElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            string charlst = Params.Length > 0 ? Params[0] : "@_'.";
            bool WhiteList = Params.Length > 1 ? Boolean.Parse(Params[1]) : true;
            bool keepChinese = Params.Length > 2 ? Boolean.Parse(Params[2]) : true;


            string input = Value.ToString();
            if (!keepChinese)
                input = Regex.Replace(input, @"[^\x00-\xff]", "");//过滤中文
            char[] source = input.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {

                if (
                    Regex.IsMatch(source[i].ToString(), @"^[a-zA-Z0-9]+$") == true
                    ||
                    Regex.IsMatch(source[i].ToString(), @"^[\u4e00-\u9fa5]+$") == true
                   )
                {
                    sb.Append(source[i]);//合法则填入
                }
                else
                {
                    if (charlst.IndexOf(source[i]) > -1)
                    {
                        if (WhiteList) sb.Append(source[i]);//如果找到列表中的字符,白名单加入
                    }
                    else if (!WhiteList) sb.Append(source[i]);//如果不是黑名单的字符 加入
                }
            }
            return Result<object>.GetResult(true, (object)sb.ToString());
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "String"; }
        }
       /// <summary>
       /// 
       /// </summary>
        public override Type ResultType
        {
            get { return typeof(string); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using Mangon.FrameWork.Result;

namespace Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTime:FormatElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            string format = Params == null || Params.Length < 1 ? "y/m/d/h/i/s" : Params[1];
            string[] fm = format.ToUpper().Split('/');


            int[] tmp = new int[6] { 0, 1, 1, 0, 0, 0 };

            string[] strs = StrToDateAry(Value.ToString());
            int lg = fm.Length > strs.Length ? strs.Length : fm.Length;
            for (int i = 0; i < lg; i++)
            {
                if (string.IsNullOrEmpty(strs[i]))
                {
                    strs[i] = "0";
                }
                switch (fm[i])
                {
                    case "Y":
                        tmp[0] = int.Parse(strs[i]);
                        break;
                    case "M":
                        tmp[1] = int.Parse(strs[i]);
                        break;
                    case "D":
                        tmp[2] = int.Parse(strs[i]);
                        break;
                    case "H":
                        tmp[3] = int.Parse(strs[i]);
                        break;
                    case "I":
                        tmp[4] = int.Parse(strs[i]);
                        break;
                    case "S":
                        tmp[5] = int.Parse(strs[i]);
                        break;
                }
            }

            return Result<object>.GetResult(true, (object)new System.DateTime(tmp[0], tmp[1], tmp[2], tmp[3], tmp[4], tmp[5]));
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return "DateTime"; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Type ResultType
        {
            get { return typeof(DateTime); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string[] StrToDateAry(string source)
        {
            source = Regex.Replace(source, @"[^\x00-\xff]", "_");//过滤中文
            char[] ca = source.ToCharArray();
            string tmp = "";
            for (int i = 0; i < ca.Length; i++)
            {
                if (char.IsNumber(ca[i]))//如果是数字
                    tmp = tmp + ca[i]; //追尾
                else //如果不为数字
                {
                    if (i == 0 || !char.IsNumber(ca[i - 1]))//且首位或者 前一位也不为数字
                        continue; //跳过
                    else
                        if (i > 0) tmp = tmp + ' ';//否则置换成空格
                }
            }
            return tmp.Trim().Split(' ');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value匹配一个正则表达式，返回 true。 
    /// </summary>
   public class IsRegex:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            if (Params.Length > 0 && !string.IsNullOrEmpty(Params[0]))
            {
                try
                {
                    return System.Text.RegularExpressions.Regex.IsMatch(Value.ToString(), @Params[0], RegexOptions.IgnoreCase);
                }
                catch { return false; }
            }
            else return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Regex"; }
        }
    }
}

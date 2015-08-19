using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 验证是不是正常字符 字母，数字，下划线的组合
    /// </summary>
   public class IsSafeChar:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return Regex.IsMatch(Value.ToString(), @"^[a-zA-Z0-9_\.\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "SafeChar"; }
        }
    }
}

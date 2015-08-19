using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 验证手机号
    /// </summary>
   public class IsMobile:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return Regex.IsMatch(Value.ToString(), @"^1\d{10}$", RegexOptions.IgnoreCase);
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Mobile"; }
        }
    }
}

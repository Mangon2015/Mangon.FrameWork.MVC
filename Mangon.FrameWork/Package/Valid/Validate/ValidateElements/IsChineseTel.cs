using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 是不是中国电话，格式010-85849685
    /// </summary>
   public class IsChineseTel:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return Regex.IsMatch(Value.ToString(), @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Tel"; }
        }
    }
}

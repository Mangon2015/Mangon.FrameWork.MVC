using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 邮政编码 6个数字
    /// </summary>
   public class IsPostCode:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return Regex.IsMatch(Value.ToString(), @"^\d{6}$", RegexOptions.IgnoreCase);
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "PostCode"; }
        }
    }
}

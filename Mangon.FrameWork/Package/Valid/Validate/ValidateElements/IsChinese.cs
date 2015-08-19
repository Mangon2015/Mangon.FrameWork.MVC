using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 中文
    /// </summary>
  public  class IsChinese:ValidateElement
    {

      /// <summary>
      /// 
      /// </summary>
      /// <param name="Value"></param>
      /// <param name="Params"></param>
      /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return Regex.IsMatch(Value.ToString(), @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

      /// <summary>
      /// 
      /// </summary>
        public override string Name
        {
            get { return "Chinese"; }
        }
    }
}

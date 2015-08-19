using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 
    /// </summary>
   public class IsHex:ValidateElement
   { 
       /// <summary>
       /// todo
       ///  当且仅当$value只包含十六进制的数字字符，返回 true。
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
           
            return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "IsHex"; }
        }
    }
}

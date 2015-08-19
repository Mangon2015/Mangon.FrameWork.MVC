using System;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value只包含数字字符，返回 true。 
    /// </summary>
   public class IsNumber:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            if (Value == null || string.IsNullOrWhiteSpace(Value.ToString()))
            {
                return false;
            }
            Regex regex = new Regex(@"^[-]?\d+[.]?\d+$");
            if (regex.Match(Value.ToString().Trim()).Success)
                return true;
            else
                return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Number"; }
        }
    }
}

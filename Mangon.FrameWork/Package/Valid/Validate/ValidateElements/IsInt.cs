using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value是一个有效的整数，返回 true。 
    /// </summary>
    public class IsInt:ValidateElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            Regex regex = new Regex(@"^[-]?\d+$");
            if (regex.Match(Value.ToString().Trim()).Success)
            {
                if ((long.Parse(Value.ToString()) > 0x7fffffffL) || (long.Parse(Value.ToString()) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return "Int"; }
        }
    }
}

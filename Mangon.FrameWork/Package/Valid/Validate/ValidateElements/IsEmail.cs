using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 允许你校验一个email地址。
    /// </summary>
   public class IsEmail:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return Regex.IsMatch(Value.ToString(),
                @"^[A-Za-z0-9\!\#\$\%\&\'\*\+/\=\?\^\`\{\|\}\~_\.\-](([\!\#\$\%\&\'\*\+/\=\?\^\`\{\|\}\~_\.\-]?[A-Za-z0-9\!\#\$\%\&\'\*\+/\=\?\^\`\{\|\}\~_\.\-]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", 
                RegexOptions.IgnoreCase);
       
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Email"; }
        }
    }
}

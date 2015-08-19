using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
    /// </summary>
   public class StringLength:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            int max;
            int min;
            if (Params[0] == null || !int.TryParse(Params[0], out min))
                min = 0;
            if (Params[1] == null || !int.TryParse(Params[1], out max))
                max = 65535;

            int length = Regex.Replace(Value.ToString(), @"[^\x00-\xff]", "OK").Length;



            if (length > min && length < max) return true;
            return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "StringLength"; }
        }
    }
}

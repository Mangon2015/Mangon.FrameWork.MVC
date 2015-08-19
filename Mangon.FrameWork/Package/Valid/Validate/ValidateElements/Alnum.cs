using Mangon.FrameWork.Package.Valid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value只包含字母和数字字符，返回 true
    /// </summary>
   public class Alnum:ValidateElement
    {
        
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Alnum"; }
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            foreach (char i in Value.ToString().Trim())
            {

                if (
                    !char.IsLetterOrDigit(i)
                   )
                {
                    return false;
                }
            }
            return true;
        }
    }
}

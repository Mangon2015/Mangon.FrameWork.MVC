using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value只包含字母字符，返回 true。这个校验器包括一个考虑空白字符是否有效的选项。 
    /// </summary>
   public class Alpha:ValidateElement
    {
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
                    !char.IsLetter(i)
                   )
                {
                    return false;
                }
            }
            return true;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Alpha"; }
        }
    }
}

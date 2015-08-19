using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 
    /// </summary>
   public class Array:ValidateElement
    {
       /// <summary>
       /// todo:
        /// 当且仅当一个"needle"$value包含在一个"haystack"数组，返回 true。如果精确选项是true，那么$value的类型也被检查。 
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
            get { return "Array"; }
        }
    }
}

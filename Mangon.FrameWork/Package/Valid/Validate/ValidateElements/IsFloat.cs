using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 允许你校验一个Float
    /// </summary>
   public class IsFloat:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            float c;
            if (float.TryParse(Value.ToString(), out c))
                return true;
            return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Float"; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value小于最大值，返回 true。  
    /// </summary>
   public class LessThan:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            double c;
            double s;
            if (Params.Length > 0 &&
                double.TryParse(Params[0], out c)
                && double.TryParse(Value.ToString(), out s)
                && s < c)
                return true;
            return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "LessThan"; }
        }
    }
}

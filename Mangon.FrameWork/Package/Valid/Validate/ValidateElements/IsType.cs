using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 中文
    /// </summary>
  public  class IsType:ValidateElement
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="Value"></param>
      /// <param name="Params"></param>
      /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {

            if (Params.Length < 1) return false;

            try
            {
                Type t = Type.GetType(Params[0]);
                if (Value.GetType().Equals(t))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
          
        }
      /// <summary>
      /// 
      /// </summary>
        public override string Name
        {
            get { return "Chinese"; }
        }
    }
}

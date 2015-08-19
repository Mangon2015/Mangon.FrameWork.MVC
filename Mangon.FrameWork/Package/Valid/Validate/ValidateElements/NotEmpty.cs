using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value非空，返回 true。 
    /// </summary>
  public  class NotEmpty:ValidateElement
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="Value"></param>
      /// <param name="Params"></param>
      /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            return !string.IsNullOrEmpty(Value.ToString());
        }
      /// <summary>
      /// 
      /// </summary>
        public override string Name
        {
            get { return "NotEmpty"; }
        }
    }
}

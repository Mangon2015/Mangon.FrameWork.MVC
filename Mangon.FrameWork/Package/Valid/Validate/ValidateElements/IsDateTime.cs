using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    ///  当$value是一个有效日期，返回true 。
    /// </summary>
  public  class IsDateTime:ValidateElement
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="Value"></param>
      /// <param name="Params"></param>
      /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            System. DateTime dt;
            if (System.DateTime.TryParse(Value.ToString(), out dt))
                return true;
            return false;
        }
      /// <summary>
      /// 
      /// </summary>
        public override string Name
        {
            get { return "DateTime"; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mangon.FrameWork.Result;


namespace Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 
    /// </summary>
  public  class Bool:FormatElement
    {


      /// <summary>
      /// 
      /// </summary>
      /// <param name="Value"></param>
      /// <param name="Params"></param>
      /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            string input = Value.ToString().ToLower();
            bool ft;
            if (bool.TryParse(input, out ft))
                return Result<object>.GetResult(true, (object)ft);
            return Result<object>.GetResult(false, false);
        }

      /// <summary>
      /// 
      /// </summary>
        public override string Name
        {
            get { return "Bool"; }
        }
    /// <summary>
    /// 
    /// </summary>
public override Type  ResultType
{
    get { return typeof(bool); }
}
}
}

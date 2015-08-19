using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using Mangon.FrameWork.Result;


namespace Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 
    /// </summary>
   public class HtmlEncode:FormatElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            Value = HttpUtility.HtmlEncode(Value.ToString());

            return Result<object>.GetResult(true, (object)Value);
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "HtmlEncode"; }
        }
       /// <summary>
       /// 
       /// </summary>
        public override Type ResultType
        {
            get { return typeof(string); }
        }
    }
}

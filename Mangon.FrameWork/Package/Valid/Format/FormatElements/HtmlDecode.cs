using System;

using System.Web;
using Mangon.FrameWork.Result;

namespace Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 
    /// </summary>
   public class HtmlDecode:FormatElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            Value = HttpUtility.HtmlDecode(Value.ToString());

            return Result<object>.GetResult(true, (object)Value);
        }

       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "HtmlDecode"; }
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

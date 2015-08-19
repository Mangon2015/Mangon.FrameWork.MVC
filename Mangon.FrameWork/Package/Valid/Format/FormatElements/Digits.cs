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
   public class Digits:FormatElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char i in Value.ToString())
            {

                if (
                    char.IsDigit(i)
                   )
                {
                    sb.Append(i);//合法则填入
                }
            }
            return Result<object>.GetResult(true, (object)sb.ToString());
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Digits"; }
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

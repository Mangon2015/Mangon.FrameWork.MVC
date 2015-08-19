using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mangon.FrameWork.Result;



namespace Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 按照第一个参数的数据类型来转换,相对是一个校验器了
    /// </summary>
    public class ToType : FormatElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
          

            if (Params.Length < 1) return Result<object>.GetResult(false, "Param Empty");

            try
            {
                Type t = Type.GetType(Params[0]);
                if (Value.GetType().Equals(t))
                {
                    return Result<object>.GetResult(true, Value);
                }
                else
                {
                    return Result<object>.GetResult(false, "Type False");
                }
            }
            catch (Exception e)
            {
                return Result<object>.GetResult(false, e);
            }
          
           
        }
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return "ToType"; }
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

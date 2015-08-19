using System;
using Mangon.FrameWork.Result;

namespace Mangon.FrameWork.Package.Valid.Format.FormatElements
{
    /// <summary>
    /// 
    /// </summary>
   public class MD5:FormatElement
    {
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "MD5"; }
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            if (Params.Length > 0)
            {
                switch (Params[0])
                {
                    case "16":
                        return Result<object>.GetResult(true, (object)Package.Encryption.MD5Encryption.GetMd5(Value.ToString(), Encryption.CodeOption.Char16));
                    case "8":
                        return Result<object>.GetResult(true, (object)Package.Encryption.MD5Encryption.GetMd5(Value.ToString(), Encryption.CodeOption.Char8));
                    case "32":
                        default:
                        return Result<object>.GetResult(true, (object)Package.Encryption.MD5Encryption.GetMd5(Value.ToString(), Encryption.CodeOption.Char32));
                   
                }
            }
            return Result<object>.GetResult(true, (object)Package.Encryption.MD5Encryption.GetMd5(Value.ToString(), Encryption.CodeOption.Char32));
   
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

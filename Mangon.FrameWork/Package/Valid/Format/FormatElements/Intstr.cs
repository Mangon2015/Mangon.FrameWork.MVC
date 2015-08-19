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
   public class Intstr:FormatElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override Result<object> doFormat(object Value, string[] Params)
        {
            char abs = '+';
            string input = Value.ToString().Trim();
            if (input[0] == '-')
                abs = '-';
            StringBuilder output = new StringBuilder();
            output.Append(abs);
            char[] ca = input.ToCharArray();
            foreach (char c in ca)
            {
                if (char.IsNumber(c))
                    output.Append(c);
            }
            int flt;
            if (int.TryParse(output.ToString(), out flt))
                return Result<object>.GetResult(true, (object)output);
            return Result<object>.GetResult(false, (object)0);
        }
       /// <summary>
       /// 
       /// </summary>
        public override Type ResultType
        {
            get
            {
                return typeof(int);
            }
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Int"; }
        }
    }
}

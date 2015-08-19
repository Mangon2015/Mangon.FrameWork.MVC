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
   public class FLoat:FormatElement
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
            {
                abs = '-';
            }
            StringBuilder output = new StringBuilder();
            output.Append(abs);
            int la = input.LastIndexOf('.');
            if (la > 0)
            {
                string head = input.Substring(0, la);//小数点前
                string last = input.Substring(la);//小数点后
                input = head.Replace(".", "") + last;//去掉全部小数点
            }
            char[] ca = input.ToCharArray();
            foreach (char c in ca)
            {
                if (char.IsNumber(c) || c == '.')//只保留数字与小数点
                    output.Append(c);
            }

            float flt;
            if (float.TryParse(output.ToString(), out flt))
                return Result<object>.GetResult(true, (object)flt);
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
            get { return "Float"; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    ///  验证时间
    /// </summary>
   public class IsTime:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            bool flag = false;
            //  TimeSpan start=new TimeSpan(0,0,0);
            TimeSpan time = new TimeSpan();
            if (TimeSpan.TryParse(Value.ToString(), out time))
            {
                if (time < new TimeSpan(0, 0, 0) || time > new TimeSpan(23, 59, 59))
                    flag = false;
                else
                    flag = true;
            }
            return flag;

        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "IsTime"; }
        }
    }
}

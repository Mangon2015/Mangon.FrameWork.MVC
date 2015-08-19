using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangon.FrameWork.Package.Valid.Validate.ValidateElements
{
    /// <summary>
    /// 当且仅当$value在最小值和最大值之间，返回true。缺省地，比较包含边界值（$value可以等于边界值），尽管为了做精确地比较这个可以被覆盖。所谓精确地比较，就是$value必须大于最小值和小于最大值。 
    /// </summary>
   public class Between:ValidateElement
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Value"></param>
       /// <param name="Params"></param>
       /// <returns></returns>
        public override bool doValidate(object Value, string[] Params)
        {
            double value;
            double max;
            double min;

            if (Params.Length < 2) return false;

            if (!double.TryParse(Value.ToString().Trim(), out value))
                return false;
            if (Params[1] == null || !double.TryParse(Params[1], out max))
                max = double.MaxValue;
            if (Params[0] == null || !double.TryParse(Params[0], out min))
                min = double.MinValue;

            if (value <= max && value >= min) return true;
            return false;
        }
       /// <summary>
       /// 
       /// </summary>
        public override string Name
        {
            get { return "Between"; }
        }
    }
}

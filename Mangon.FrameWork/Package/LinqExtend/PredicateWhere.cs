using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.LinqExtend
{
    /// <summary>
    /// 拼接模式
    /// </summary>
    public enum WhereMode
    { 
        AND,
        OR
    }
    /// <summary>
    /// 拼接参数
    /// </summary>
    public class PredicateWhere<T>:PredicateBase
    {

        public Expression<Func<T, bool>> Condition { get; private set; }

        public WhereMode PanMode { get; private set; }

        public PredicateWhere(Expression<Func<T, bool>> condition, WhereMode mode):base(typeof(T))
        {
            Condition = condition;
            PanMode = mode;
        }
    }
}

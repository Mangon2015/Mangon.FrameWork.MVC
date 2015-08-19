using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.LinqExtend
{
    /// <summary>
    /// 表达式基类
    /// </summary>
   public abstract class PredicateBase
    {
       protected Type _selfType;
       protected static Dictionary<Type, PropertyInfo[]> TypesPropertyInfo = new Dictionary<Type, PropertyInfo[]>();
       public PredicateBase(Type type)
       {
           if (type==null)
           {
               return;
           }
           _selfType = type;
           if (!TypesPropertyInfo.ContainsKey(_selfType))
           {
               TypesPropertyInfo.Add(_selfType, _selfType.GetProperties());
           }
       }
    }
}

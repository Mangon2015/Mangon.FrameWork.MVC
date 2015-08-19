using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.LinqExtend
{
     public enum OrderType
       { 
        ASC,
           DESC
       }
   public class PredicateOrder<T>:PredicateBase
    {

       public string Field { get; protected set; }
       public PredicateOrder() : base(typeof(T)) { }
       public OrderType panModel { get; protected set; }
       public PredicateOrder(string field, OrderType type)
           : base(typeof(T))
       {
           if (TypesPropertyInfo[_selfType].Count(p=>p.Name==field)>0)
           {
               Field = field;
           }
           else
           {
               throw new ArgumentNullException("Field not found");
           }
           panModel = type;
       }

      

    }

   public class PredicateOrder<T, TType> : PredicateOrder<T>
   {
       public PredicateOrder(Expression<Func<T, TType>> orderField, OrderType type)
       {
           Field = ExpressionHelper.GetExpressionText(orderField);
           panModel = type;
       }
   }
}

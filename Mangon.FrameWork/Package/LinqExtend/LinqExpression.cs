using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.LinqExtend
{
    /// <summary>
    /// Linq 扩展
    /// </summary>
    public static class LinqExpression
    {
        /// <summary>
        /// 排序
        /// </summary>
        public enum Sort
        {
            /// <summary>
            /// 顺序
            /// </summary>
            ASC,
            /// <summary>
            /// 倒序
            /// </summary>
            DESC
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string field, Sort sort)
        {
            var exp = CreatrOrderBy<T>(query.Expression, field, sort);
            return query.Provider.CreateQuery<T>(exp);
        }

        public static Expression CreatrOrderBy<T>(Expression exp, string field, Sort sort)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), field);
            PropertyInfo pi = typeof(T).GetProperty(field);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            string sortwith;
            if (sort==Sort.DESC)
            {
                sortwith = "OrderByDescending";
            }
            else
            {
                sortwith = "OrderBy";
            }
            return Expression.Call(typeof(Queryable),sortwith,types,exp,Expression.Lambda(Expression.Property(param,field),param));
        }
    }

   
}

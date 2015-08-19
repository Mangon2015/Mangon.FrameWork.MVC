using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mangon.FrameWork.Package.LinqExtend
{
    /// <summary>
    /// 表达式拼装
    /// </summary>
    public static class ExpressionHelper
    {
        public static IQueryable<T> OrderAsc<T>(this IQueryable<T> qt, Expression<Func<T, dynamic>> order)
        {
            string propertyName = GetExperssionName(order);
            var param = Expression.Parameter(typeof(T));
            var body = Expression.Property(param, propertyName);
            dynamic keySelector = Expression.Lambda(body, param);
            return Queryable.OrderBy(qt, keySelector);
        }

        public static IQueryable<T> OrderDesc<T>(this IQueryable<T> qt, Expression<Func<T, dynamic>> order)
        {
            string propertyName = GetExperssionName(order);
            var param = Expression.Parameter(typeof(T));
            var body = Expression.Property(param, propertyName);
            dynamic keyselector = Expression.Lambda(body, param);
            return Queryable.OrderByDescending(qt, keyselector);
        }

        public static string GetExperssionName<T>(Expression<Func<T, dynamic>> Expression)
        {
            string name = string.Empty;
            if (Expression.Body.GetType()==typeof(System.Linq.Expressions.UnaryExpression))
            {
                var per = ((System.Linq.Expressions.UnaryExpression)Expression.Body).Operand;
                name = ((MemberExpression)per).Member.Name;
            }
            else
            {
                name = ((MemberExpression)Expression.Body).Member.Name;
            }
            return name;
        }

        public static Type GetExperssionType<T>(Expression<Func<T, dynamic>> expression, out string name)
        {
            Type type;
            if (expression.Body.GetType()==typeof(System.Linq.Expressions.UnaryExpression))
            {
                Type t = ((System.Linq.Expressions.UnaryExpression)expression.Body).Operand.Type;
                name = ((System.Linq.Expressions.UnaryExpression)expression.Body).Operand.ToString();
                if (t==typeof(Nullable<>)||t.GetGenericArguments().Length>0)
                {
                    type = t.GetGenericArguments()[0];
                }
                else
                {
                    type = t;
                }
            }
            else
            {
                type = expression.Body.Type;
                name = expression.ToString();
            }
            return type;
        }
        /// <summary>
        /// 读取表达式的文本
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetExpressionText(string expression)
        {
            return string.Equals(expression, "model", StringComparison.OrdinalIgnoreCase) ? string.Empty : expression;
        }

        public static string GetExpressionText(LambdaExpression expression)
        {
            Stack<string> nameParts = new Stack<string>();
            Expression part = expression.Body;
            while (part!=null)
            {
                if (part.NodeType==ExpressionType.Call)
                {
                    MethodCallExpression methodExpression = (MethodCallExpression)part;
                    if (!IsSingleArgumentIndexer(methodExpression))
                    {
                        break;
                    }
                    nameParts.Push(GetIndexerInvocation(methodExpression.Arguments.Single(), expression.Parameters.ToArray()));
                    part = methodExpression.Object;
                }
                else if (part.NodeType==ExpressionType.ArrayIndex)
                {
                    BinaryExpression binaryExpression = (BinaryExpression)part;
                    nameParts.Push(
                        GetIndexerInvocation(binaryExpression.Right, expression.Parameters.ToArray())
                        );
                    part = binaryExpression.Left;
                }
                else if (part.NodeType==ExpressionType.MemberAccess)
                {
                    MemberExpression memberExpressionPart = (MemberExpression)part;
                    nameParts.Push("." + memberExpressionPart.Member.Name);
                    part = memberExpressionPart.Expression;
                }
                else if (part.NodeType==ExpressionType.Parameter)
                {
                    nameParts.Push(string.Empty);
                    part = null;
                }
                else
                {
                    break;
                }
            }

            if (nameParts.Count>0&&string.Equals(nameParts.Peek(),".model",StringComparison.OrdinalIgnoreCase))
            {
                nameParts.Pop();
            }
            if (nameParts.Count>0)
            {
                return nameParts.Aggregate((left, right) => left + right).TrimStart('.');
            }
            return string.Empty;

        }

        private static string GetIndexerInvocation(Expression expression, ParameterExpression[] parameters)
        {
            Expression converten = Expression.Convert(expression, typeof(object));
            ParameterExpression fakeParameter = Expression.Parameter(typeof(object), null);
            Expression<Func<object, object>> lambda = Expression.Lambda<Func<object, object>>(converten, fakeParameter);
            Func<object, object> func;
            try
            {
                func = System.Web.Mvc.ExpressionUtil.CachedExpressionCompiler.Process(lambda);
            }
            catch
            {
                throw;
            }

            return "[" + Convert.ToString(func(null), CultureInfo.InvariantCulture) + "]";
        }

        internal static bool IsSingleArgumentIndexer(Expression expression)
        {
            MethodCallExpression methodExpression = expression as MethodCallExpression;
            if (methodExpression==null||methodExpression.Arguments.Count!=1)
            {
                return false;
            }
            return methodExpression.Method.DeclaringType.GetDefaultMembers().OfType<PropertyInfo>().Any(p => p.GetGetMethod() == methodExpression.Method);
        }

    }
}

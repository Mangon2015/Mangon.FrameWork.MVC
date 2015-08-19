using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Core
{
    /// <summary>
    /// 公共扩展类
    /// </summary>
    [DebuggerStepThrough]
    public static class CommonExtend
    {
        /// <summary>
        /// 对source的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (var item in source)
            {
                action(item, index++);
            }
        }
        /// <summary>
        /// 获取去除可空类型的类型
        /// </summary>
        /// <param name="type">当前类型的实例对象</param>
        /// <returns>去掉可空类型的类型</returns>
        public static Type GetNoneNullableType(this Type type)
        {
            if (IsNullable(type))
            {
                return Nullable.GetUnderlyingType(type);
            }
            return type;
        }
        /// <summary>
        /// 获取可空类型的类型
        /// </summary>
        /// <param name="type">当前类型的实例对象</param>
        /// <returns>可空类型的类型</returns>
        public static Type GetNullableType(this Type type)
        {
            if (!IsNullable(type)&&type.IsValueType)
            {
                return typeof(Nullable<>).MakeGenericType(type);
            }
            return type;
        }
        /// <summary>
        /// 获取一个值，通过该值指示当前类型是否为可空类型
        /// </summary>
        /// <param name="type">当前类型的实例对象</param>
        /// <returns>表示为可空类型 否则返回false</returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}

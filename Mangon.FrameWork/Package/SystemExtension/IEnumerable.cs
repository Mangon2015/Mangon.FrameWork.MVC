using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 迭代器扩展
    /// </summary>
    public static class Mangon_FrameWorkSystemExtension_IEnumerable
    {
        public static IEnumerable<string> ToUpper(this IEnumerable<string> Enumerable)
        {
            Enumerable.Where(p => p != null).ForEach(e => e = e.ToUpper());
            return Enumerable;
        }

        public static IEnumerable<string> Tolower(this IEnumerable<string> Enumerable)
        {
            Enumerable.Where(p => p != null).ForEach(e => e = e.ToLower());
            return Enumerable;
        }


        public static String Merge<T>(this IEnumerable<T> Enumerable, string word)
        {
            return Merge(Enumerable, "", "", word.ToString());
        }
        /// <summary>
        /// 合并成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Enumerable"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static String Merge<T>(this IEnumerable<T> Enumerable, char word)
        {

            return Merge(Enumerable, "", "", word.ToString());
        }

        public static void For<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.For(enumerable.Count(), action);
        }
        public static void For<T>(this IEnumerable<T> enumerable, int Begin, Action<T> action)
        {
            enumerable.For(Begin, 1, action);
        }
        public static void For<T>(this IEnumerable<T> enumerable, int Begin, int Last, Action<T> action)
        {
            enumerable.For(Begin, Last, 1, action);
        }
        public static void For<T>(this IEnumerable<T> enumerable, int Begin, int Last, int Step, Action<T> action)
        {
            for (int i = Begin; i < Last; i = i + Step)
            {
                action(enumerable.ElementAt(i));
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {


            foreach (T t in enumerable)
            {
                action(t);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {


            int i = 0;
            foreach (T t in enumerable)
            {
                action(t, i++);
            }
        }

        /// <summary>
        /// 合并成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string Merge<T>(this IEnumerable<T> enumerable, string BeginChar, string EndChar, string AppendChar)
        {
            return Merge<T>(enumerable, BeginChar, EndChar, AppendChar, e => e);
        }

        /// <summary>
        /// 合并成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="BeginChar"></param>
        /// <param name="EndChar"></param>
        /// <param name="AppendChar"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static string Merge<T>(this IEnumerable<T> enumerable, string BeginChar, string EndChar, string AppendChar, Func<T, object> converter)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(BeginChar);
            enumerable.ForEach((e, i) => sb.AppendFormat("{0}{1}", i == 0 ? string.Empty : AppendChar, converter(e)));
            sb.Append(EndChar);
            return sb.ToString();
        }
        /// <summary>
        /// 重复
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="generateResultFunc"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> Repeat<T>(this Func<T> generateResultFunc, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return generateResultFunc();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
   public static class Mangon_FrameWorkSystenExtension_Dictionary
    {
        /// <summary>
        /// 合并字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
        {

            foreach (var kvp in right)
            {
                left[kvp.Key] = kvp.Value;
            }
        }
    }
}

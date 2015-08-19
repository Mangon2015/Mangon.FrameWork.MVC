using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Mangon_FrameWorkSystemExtension_Enums
    {
        public static IEnumerable<T> GetEnumerator<T>(this Enum enums)
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                yield return item;
            }
        }

        public static int Value(this Enum enums)
        {
            return enums.GetHashCode();
        }
        public static String Name(this Enum enums)
        {
            return Enum.GetName(enums.GetType(), enums.Value());
        }

        public static string GetName<T>(int value)
        {
            string result = "";
            foreach (string item in Enum.GetNames(typeof(T)))
            {
                if (value==(int)Enum.Parse(typeof(T),item))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
    }

    /// <summary>
    /// T 类型枚举列表
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>

    public class Enum<T> : IEnumerable<T>
    {
        public static IEnumerable<T> AsEnumerable()
        {
            return new Enum<T>();
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

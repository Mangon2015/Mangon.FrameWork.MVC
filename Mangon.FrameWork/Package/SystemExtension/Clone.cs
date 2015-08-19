using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 对象扩展
    /// </summary>
    public static class Mangon_FrameWorkSystemExtension_Clone
    {
        public static Dictionary<Type, FieldInfo[]> PropertyDict = new Dictionary<Type, FieldInfo[]>();
        public static Dictionary<Type, FieldInfo[]> FieldInfoDict = new Dictionary<Type, FieldInfo[]>();

        /// <summary>
        /// 实例化并克隆,
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TOut TryClone<TOut,TIn>(this TIn self)where TOut:class ,new()
            where TIn : class
            {
                if (self==null)
                {
                    throw new ArgumentNullException();
                }
                TOut outItem = new TOut();
                outItem.TryClone(self);
                return outItem;
            }
        /// <summary>
        /// 尝试尽量赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="obj"></param>
        public static void TryClone<T>(this T self, object obj)
        {
            TryClone<T>(self, obj, null, true);
        }

        public static T TryClone<T>(this T self,object obj,IEnumerable<string> list,bool isBlock)
        {
            try
            {
                if (self==null)
                {
                    throw new ArgumentNullException();
                }
                if (list==null)
                {
                    list = new string[0];
                }
                var baseType = typeof(T);//基准类型 以此类型为赋值基准
                var targetType = obj.GetType();//目标的类型
                var tp = targetType.GetProperties();//获取目标全部属性
                var Propertiess = baseType.GetProperties();//获取基准全部属性
                for (int i = 0; i < Propertiess.Length; i++)
                {
                    PropertyInfo p = Propertiess[i];
                    //如果黑名单并存
                    if (isBlock&&list.Contains(p.Name))
                    {
                        continue;
                    } //如果是白名单，但不存在
                    else if (!isBlock && !list.Contains(p.Name))
                    {
                        continue;
                    }

                    if (p.CanRead&&p.CanWrite) //如果允许读写
                    {
                        var targerP = tp.Where(t => t.Name == p.Name && t.PropertyType == p.PropertyType && t.CanWrite && t.CanRead).FirstOrDefault();
                        if (targerP!=null) //如果目标具有相同属性
                        {
                            var value=targerP.GetValue(obj);
                            p.SetValue(self, value); //赋值
                        }
                    }
                }//for end

                var fields = baseType.GetFields();
                var tf = targetType.GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    FieldInfo f = fields[i];
                    if (isBlock&&list.Contains(f.Name))
                    {
                        continue;
                    }
                    else if (!isBlock&&!list.Contains(f.Name))
                    {
                        continue;
                    }
                    var targetF = tf.FirstOrDefault(p => p.FieldType == f.FieldType && p.Name == f.Name);
                    if (targetF!=null) //存在且不是只读
                    {
                        f.SetValue(self, targetF.GetValue(f));
                    }
                }//for end
                return self;
            }
            catch (Exception e)
            {
                throw new ArgumentException("赋值失败", e);
            }
        }
        /// <summary>
        /// 把自己的值赋值到type上 并返回type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T CloneTo<T>(this T self, T type) where T : class ,new()
        {
            return CloneToList<T>(self, type, null, true);
        }
        /// <summary>
        /// 把自己的值赋值到type上 并返回type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <param name="list"></param>
        /// <param name="isBlock"></param>
        /// <returns></returns>
        public static T CloneToList<T>(this T self, T type, IEnumerable<string> list, bool isBlock) where T : class ,new()
        {
            if (self==null)
            {
                throw new ArgumentNullException();
            }
            if (type==null)
            {
                return self;
            }
            return self.CloneToList<T>(type, list, isBlock);
        }
        /// <summary>
        /// 把type 的公有值赋值到自己上,并返回自己
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T CloneIn<T>(this T self, T type) where T : class
        {
            return CloneInList<T>(self, type, null, true);
        }
        /// <summary>
        /// 把type 的公有值赋值到自己上,并返回自己,并带黑名单
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <param name="list"></param>
        /// <param name="isBlock"></param>
        /// <returns></returns>
        public static T CloneInList<T>(this T self, T type, IEnumerable<string> list, bool isBlock) where T : class
        {
            try
            {
                if (self ==null||type==null)
                {
                    return self;
                }
                var baseType = typeof(T);
                if (list==null)
                {
                    list = new string[0];
                }
                List<string> _list = list.ToList();
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = _list[i].ToLower();
                }

                var propertiess = baseType.GetProperties();
                for (int i = 0; i < propertiess.Length; i++)
                {
                    PropertyInfo p = propertiess[i];
                    if (isBlock&&_list.Contains(p.Name.ToLower()))
                    {
                        continue;
                    }
                    else if (!isBlock&&!_list.Contains(p.Name.ToLower()))
                    {
                        continue;
                    }
                    ///不在名单内就反射
                    if (p.CanRead&&p.CanWrite)
                    {
                        if (!(p.GetValue(self)==null&&p.GetValue(type)==null))
                        {
                            p.SetValue(self, p.GetValue(type));
                        }
                    }
                } //for end

                var fields = baseType.GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    FieldInfo f = fields[i];
                    if (isBlock&&_list.Contains(f.Name.ToLower()))
                    {
                        continue;
                    }
                    else if (!isBlock&&!_list.Contains(f.Name.ToLower()))
                    {
                        continue;   
                    }
                    f.SetValue(self, f.GetValue(type));
                }//for end
                return self;
            }
            catch (Exception e)
            {
                throw new ArgumentException("赋值失败", e);
            }
        }
        /// <summary>
        /// 反射赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static T Assignment<T>(this T self, IDictionary<string, object> dict) where T : class
        {
            try
            {
                if (self == null || dict == null) throw new ArgumentNullException();
                IEnumerable<string> List = dict.Keys;
                var baseType = typeof(T); //基准类型 以此类型为赋值基准        

                var Propertiess = baseType.GetProperties();//获取基准全部属性
                for (int i = 0; i < Propertiess.Length; i++)//循环基准
                {
                    PropertyInfo p = Propertiess[i];
                    //如果黑名并存在
                    if (!List.Contains(p.Name))
                    {
                        continue;
                    }
                    if (p.CanRead && p.CanWrite)//如果允许读写
                    {
                        p.SetValue(self, dict[p.Name]);//赋值                 
                    }
                }

                var Fields = baseType.GetFields();
                for (int i = 0; i < Fields.Length; i++)
                {
                    FieldInfo f = Fields[i];
                    if (!List.Contains(f.Name))
                    {
                        continue;
                    }
                    f.SetValue(self, dict[f.Name]);

                }
                return self;
            }
            catch (Exception e)
            {
                throw new ArgumentException("赋值类型不正确", e);
            }
        }
        /// <summary>
        /// 一般克隆
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Clone<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            Dictionary<TKey, TValue> dict = source as Dictionary<TKey, TValue>;
            if (dict!=null)
            {
                return new Dictionary<TKey, TValue>(source, dict.Comparer);
            }

            SortedDictionary<TKey,TValue> sortedDict=source as SortedDictionary<TKey,TValue>;
            if (sortedDict!=null)
            {
                return new SortedDictionary<TKey, TValue>(source, sortedDict.Comparer);
            }
            SortedList<TKey, TValue> sortedList = source as SortedList<TKey, TValue>;
            if (sortedList!=null)
            {
                return new SortedList<TKey, TValue>(source, sortedList.Comparer);
            }
            return new Dictionary<TKey, TValue>(source);
        }

        public static IList<T> Clone<T>(this IList<T> source)
        {
            return new List<T>(source);
        }

        public static Stack<T> Clone<T>(this Stack<T> source)
        {
            return new Stack<T>(source.Reverse());
        }
        public static Queue<T> Clone<T>(this Queue<T> source)
        {
            return new Queue<T>(source);
            }
    }
}

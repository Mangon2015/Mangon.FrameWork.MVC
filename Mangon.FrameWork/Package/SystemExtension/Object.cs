using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 通用对象扩展
    /// </summary>
   public static  class Mangon_FrameWorkSystemExtension_Object
    {

        /// <summary>
        /// 寻找对象下属值
        /// </summary>
        /// <param name="obj">对象本身</param>
        /// <param name="path">字符串化的属性</param>
        /// <returns></returns>
        public static object FindByPath(this object obj, string path)
        {
            return Find(obj, path);
        }
        /// <summary>
        /// 寻找属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="path">路径</param>
        /// <example>Find(obj,"a.b.c")</example>
        /// <returns>对象的属性的值</returns>
        public static object Find(object obj, string path)
        {
            //如果路径不能为空
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            foreach (string name in path.Split('.'))//拆分
            {


                if (obj == null || string.IsNullOrWhiteSpace(name)) break;
                bool exist;
                Match m = Regex.Match(name, @"^([\w]+)\[+([\w]+)+\]$", RegexOptions.Compiled);

                if (m != null && m.Groups != null && m.Groups.Count > 0 && m.Groups[0].Success)
                {
                    for (int i = 1; i < m.Groups.Count; i++)
                    {
                        obj = GetPropertyValue(obj, m.Groups[i].Value, out exist);//开始找
                    }
                }
                else
                {
                    obj = GetPropertyValue(obj, name, out exist);//开始找
                }
                // PropertyInfo fi = obj.GetType().GetProperty(name);
                //Type t=obj.GetType();



            }
            return obj;
        }
        /// <summary>
        /// 搜索单层值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">属性</param>
        /// <returns></returns>
        public static object FindSingle(object obj, string name)
        {

            if (obj == null) throw new ArgumentNullException("obj");
            PropertyInfo fi = obj.GetType().GetProperty(name);
            if (fi == null) return null;
            return fi.GetValue(obj, null);
        }

        internal static bool IsInteger(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value[0] != '-' && !char.IsDigit(value[0])) return false;
            int i;
            return int.TryParse(value, out i);
        }
        /// <summary>
        /// 获取某个属性的值
        /// </summary>
        /// <param name="container">数据源</param>
        /// <param name="propName">属性名</param>
        /// <param name="exist">是否存在此属性</param>
        /// <returns>属性值</returns>
        internal static object GetPropertyValue(object container, string propName, out bool exist)
        {
            exist = false;
            object value = null;
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            if (string.IsNullOrEmpty(propName))
            {
                throw new ArgumentNullException("propName");
            }
            if (IsInteger(propName))
            {
                #region 索引值部分
                //属性名只为数字.则取数组索引
                int index = 0;

                if (!int.TryParse(propName, out index))
                {
                    index = 0;
                }



                if (container is IList)
                {
                    IList iList = (IList)container;
                    if (iList.Count > index)
                    {
                        exist = true;
                        value = iList[index];
                    }
                }
                else if (container is ICollection)
                {
                    ICollection ic = (ICollection)container;
                    if (ic.Count > index)
                    {
                        exist = true;
                        IEnumerator ie = ic.GetEnumerator();
                        int i = 0;
                        while (i++ <= index) { ie.MoveNext(); }
                        value = ie.Current;
                    }
                }
                else
                {
                    //判断是否含有索引属性
                    PropertyInfo item = container.GetType().GetProperty("Item", new Type[] { typeof(int) });
                    if (item != null)
                    {
                        try
                        {
                            value = item.GetValue(container, new object[] { index });
                            exist = true;
                        }
                        catch
                        {
                            exist = false;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 字段/属性/键值
                //容器是类型.则查找静态属性或字段
                Type type = container is Type ? (Type)container : container.GetType();
                BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase;
                if (!(container is Type)) flags |= BindingFlags.Instance;

                //查找字段
                // FieldInfo field = type.GetField(propName, flags);
                //  if (field != null)
                //  {
                //     exist = true;
                //     value = field.GetValue(container);
                // }
                // else
                //{
                //查找属性
                PropertyInfo property = type.GetProperty(propName, flags, null, null, Type.EmptyTypes, new ParameterModifier[0]);
                if (property != null)
                {
                    exist = true;
                    value = property.GetValue(container, null);
                }
                else if (container is ICustomTypeDescriptor)
                {
                    //已实现ICustomTypeDescriptor接口
                    ICustomTypeDescriptor ictd = (ICustomTypeDescriptor)container;
                    PropertyDescriptor descriptor = ictd.GetProperties().Find(propName, true);
                    if (descriptor != null)
                    {
                        exist = true;
                        value = descriptor.GetValue(container);
                    }
                }
                else if (container is IDictionary)
                {
                    //是IDictionary集合
                    IDictionary idic = (IDictionary)container;
                    if (idic.Contains(propName))
                    {
                        exist = true;
                        value = idic[propName];
                    }
                }
                else if (container is NameObjectCollectionBase)
                {
                    //是NameObjectCollectionBase派生对象
                    NameObjectCollectionBase nob = (NameObjectCollectionBase)container;

                    //调用私有方法
                    MethodInfo method = nob.GetType().GetMethod("BaseGet", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string) }, new ParameterModifier[] { new ParameterModifier(1) });
                    if (method != null)
                    {
                        value = method.Invoke(container, new object[] { propName });
                        exist = value != null;
                    }
                }
                else
                {
                    //判断是否含有索引属性
                    PropertyInfo item = type.GetProperty("Item", new Type[] { typeof(string) });
                    if (item != null)
                    {
                        try
                        {
                            value = item.GetValue(container, new object[] { propName });
                            exist = true;
                        }
                        catch
                        {
                            exist = false;
                        }
                    }
                }
                // }//end if
                #endregion
            }
            return value;
        }
    }
}

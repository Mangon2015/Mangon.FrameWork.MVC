using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mangon.FrameWork.Package.Language
{
    /// <summary>
    ///翻译器
    /// </summary>
    public static class StringLang
    {

        /// <summary>
        /// 静态化的字典
        /// </summary>
        static System.Collections.Generic.Dictionary<string, Dictionary<string, string>> DictStr { get; set; }
        /// <summary>
        /// 静态化的字典
        /// </summary>
        static System.Collections.Generic.Dictionary<string, Dictionary<int, string>> DictInt { get; set; }
        public static void LoadXml(string path)
        {
            //转换是用的缓存
            int i;
            string name;
            //英文做索引,s的值和d的值 理论上是一样的
            Dictionary<string, string> s;
            //数字做索引
            Dictionary<int, string> d;
            //读入语言文件
            XElement xml = XElement.Load(path);
            //开始遍历文件
            foreach (var item in xml.Elements())
            {
                //获取节点名
                name = item.Name.LocalName;
                s = new Dictionary<string, string>();
                d = new Dictionary<int, string>();
                //如果有子节点
                if (item.HasElements)
                {
                    //查找自己的主节点
                    foreach (var e in item.Elements("item"))
                    {
                        //如果没有值 直接跳过
                        if (string.IsNullOrWhiteSpace(e.Value))
                        {
                            continue;
                        }
                        //int 这个属性有效 则赋值
                        if (int.TryParse(e.Attribute("int").Value, out i))
                        {
                            d.Add(i, e.Value);
                        }
                        //string 这个属性有效 则赋值
                        if (e.Attribute("string") != null && !string.IsNullOrWhiteSpace(e.Attribute("string").Value))
                        {
                            s.Add(e.Attribute("string").Value, e.Value);
                        }
                    }//end foreach
                    //开始遍历纯文本节点
                    foreach (var e in item.Elements("string"))
                    {
                        //没有 则跳过
                        if (string.IsNullOrWhiteSpace(e.Value))
                        {
                            continue;
                        }
                        //如果name这个属性有效 则赋值
                        if (e.Attribute("name") != null && !string.IsNullOrWhiteSpace(e.Attribute("name").Value))
                        {
                            s.Add(e.Attribute("name").Value, e.Value);
                        }
                    }//foreach end
                    //开始遍历数字节点
                    foreach (var e in item.Elements("int"))
                    {
                        if (string.IsNullOrWhiteSpace(e.Value))
                        {
                            continue;
                        }
                        //如果name这个属性有效 则赋值
                        if (e.Attribute("name") != null && !string.IsNullOrWhiteSpace(e.Attribute("name").Value))
                        {
                            s.Add(e.Attribute("name").Value, e.Value);
                        }
                    } // foreach end 
                    add(name, s);
                    add(name, d);
                } //if end

            } // foreach end

        }
        /// <summary>
        /// 添加数字字典
        /// </summary>
        /// <param name="CollectionName">字典名</param>
        /// <param name="strs">文字字典</param>
        /// <param name="ints">数字字典</param>
        public static void AddItems(string CollectionName, Dictionary<string, string> strs, Dictionary<int, string> ints)
        {
            add(CollectionName, strs);
            add(CollectionName, ints);
        }
        /// <summary>
        /// 增加转移字典
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="values">文字字典</param>
        public static void add<T>(Dictionary<string, string> values)
        {
            add(typeof(T).Name, values);
        }
        /// <summary>
        /// (合并)增加文字字典
        /// </summary>
        /// <param name="CollectionName">字典名</param>
        /// <param name="Values">数字字典</param>
        public static void add(string collectionName, Dictionary<string, string> values)
        {
            if (DictStr == null)
            {
                DictStr = new Dictionary<string, Dictionary<string, string>>(20);
            }
            foreach (KeyValuePair<string, string> item in values)
            {
                DictStr[collectionName].Add(item.Key.ToLower(), item.Value.ToLower());
            }
        }
        /// <summary>
        /// 增加转义数字字典
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="Values">数字字典</param>
        public static void add<T>(Dictionary<int, string> values)
        {
            add(typeof(T).Name, values);
        }
        /// <summary>
        /// 增加数字字典
        /// </summary>
        /// <param name="CollectionName">字典名</param>
        /// <param name="Values">数字字典</param>
        public static void add(string collectionName, Dictionary<int, string> values)
        {
            if (DictInt == null)
            {
                DictInt = new Dictionary<string, Dictionary<int, string>>(20);
            }
            foreach (KeyValuePair<int, string> item in values)
            {
                DictInt[collectionName].Add(item.Key, item.Value.ToLower());
            }
        }
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="str">需要翻译的字符</param>
        /// <param name="CollectionName">对应字典</param>
        /// <returns>对应值</returns>
        public static string Trans(this string str, string collectionName = "Global")
        {
            Dictionary<string, string> r;
            if (DictStr.TryGetValue(collectionName, out r))
            {
                string ir;
                if (r.TryGetValue(str.ToLower(), out ir))
                {
                    return ir;
                }
            }
            return str;
        }
        /// <summary>
        /// 翻译(转义)
        /// </summary>
        /// <param name="str">需要翻译的字符</param>    
        /// <returns>对应值</returns>
        public static string Trans<T>(this string str)
        {
            string collectionName = typeof(T).Name;
            return str.Trans(collectionName);
        }
        /// <summary>
        /// 数字字典翻译
        /// </summary>
        /// <param name="id">需要翻译的id</param>
        /// <param name="CollectionName">对应字典</param>
        /// <returns>对应值</returns>
        public static string Trans(this int id, string collectionName)
        {
            Dictionary<int, string> r;
            if (DictInt.TryGetValue(collectionName,out r))
            {
                string ir;
                if (r.TryGetValue(id,out ir))
                {
                    return ir;
                }
            }
            return "";
        }
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="id">需要翻译的字符</param>
        /// <typeparam name="T">对应字典</typeparam>
        /// <returns>对应值</returns>
        public static string Trans<T>(this short id)
        {
            string collectionName = typeof(T).Name;
            return ((int)id).Trans(collectionName);
        }

        /// <summary>
        /// 数字翻译
        /// </summary>
        /// <typeparam name="T">转义类型</typeparam>
        /// <param name="id">翻译键</param>
        /// <returns>值</returns>
        public static string Trans<T>(this int id)
        {

            string CollectionName = typeof(T).Name;
            return id.Trans(CollectionName);
        }
        /// <summary>
        /// 翻译....
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Translate(this string str)
        { 
            string[] list=str.ToLower().Trim().Split(' ');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                sb.Append(TransMap(list[i].Trim()));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 遍历,当字典太大的时候性能不太高
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>键</returns>
        public static string TransMap(string str)
        {
            string outstr;
            foreach (var i in DictStr.Values)
            {
                if (i.TryGetValue(str,out outstr))
                {
                    return outstr;
                }
            }
            return string.Empty;
        }

        public static string Trans(this string item, Type type)
        {
            return item.Trans(type.Name);
        }
        public static string Trans(this int item, Type type)
        {
            return item.Trans(type.Name);
        }

        public static string Trans(this Enum item)
        {
            return item.ToString().Trans(item.GetType().Name);
        }

        public static string Tans(this bool item)
        {
            return item.ToString().Trans("Bool");
        }
    }
}

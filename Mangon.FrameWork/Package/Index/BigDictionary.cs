using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Index
{
    ///// <summary>
    ///// 超大容量字典,设计构思以字符串头2个字母作为拆分 分成26x26个表 快速索引,未完全实现
    ///// 现在有问题的是 当数据分布不平衡时候 无法起到目的
    ///// </summary>
    ///// <typeparam name='TValue'></typeparam>
    //public class BigDictionary<TValue> : IDictionary<string, TValue>
    //{
    //    List<string> Key = new List<string>(600000);
    //    string[] KeyAry = new string[600000];
    //    Dictionary<char, Dictionary<char, Dictionary<string, TValue>>> Parent = new Dictionary<char, Dictionary<char, Dictionary<string, TValue>>>(26);
    //    char[] chars = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    //    /// <summary>
    //    /// 初始化 先占内存.默认是68w个对象容量
    //    /// </summary>
    //    public BigDictionary()
    //    {

    //        for (int i = 0; i < 26; i++)
    //        {
    //            Parent[chars[i]] = new Dictionary<char, Dictionary<string, TValue>>(26);
    //            for (int s = 0; s < 26; s++)
    //            {
    //                Parent[chars[i]][chars[2]] = new Dictionary<string, TValue>(1000);
    //            }
    //        }

    //    }


    //    /// <summary>
    //    /// 设置
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <param name="value"></param>
    //    public void Add(string key, TValue value)
    //    {
    //        char c = key[0];
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public bool ContainsKey(string key)
    //    {
    //        return false;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public ICollection<string> Keys
    //    {
    //        get { return null; }
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public bool Remove(string key)
    //    {
    //        return false;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public bool TryGetValue(string key, out TValue value)
    //    {
    //        value = default(TValue);
    //        return false;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public ICollection<TValue> Values
    //    {
    //        get { return null; }
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public TValue this[string key]
    //    {
    //        get
    //        {
    //            return default(TValue);
    //        }
    //        set
    //        {

    //        }
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="item"></param>
    //    public void Add(KeyValuePair<string, TValue> item)
    //    {

    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public void Clear()
    //    {

    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    public bool Contains(KeyValuePair<string, TValue> item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="array"></param>
    //    /// <param name="arrayIndex"></param>
    //    public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int Count
    //    {
    //        get { return 0; }
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public bool IsReadOnly
    //    {
    //        get { return false; }
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    public bool Remove(KeyValuePair<string, TValue> item)
    //    {
    //        return false;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
    //    {
    //        return null;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return null;
    //    }
    //}
}

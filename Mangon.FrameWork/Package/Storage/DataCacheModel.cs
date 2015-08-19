using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Storage
{
    public class DataCacheModel<TKey, TValue>
    {
        private ConcurrentDictionary<TKey, DataModel<TValue>> dic = new ConcurrentDictionary<TKey, DataModel<TValue>>();

        private double _timeOut = 43200;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="second">过期时间 单位以秒为单位</param>
        public DataCacheModel(double second = 21600)
        {
            _timeOut = second;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hour">小时</param>
        /// <param name="minute">分钟</param>
        /// <param name="second">秒</param>
        public DataCacheModel(int hour, int minute, int second)
            : this(hour * 60, second)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minute">分钟</param>
        /// <param name="second">秒</param>
        public DataCacheModel(int minute, int second)
            : this(minute * 60 + second)
        {
        }


        public void Add(TKey key, TValue value)
        {
            if (!dic.ContainsKey(key))
            {
                dic[key] = new DataModel<TValue>(value, _timeOut);
            }
        }
        /// <summary>
        /// 是否存在没有过期的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasKeyForNotTimeOut(TKey key)
        {
            if (dic.ContainsKey(key))
            {
                DataModel<TValue> data;
                if (dic[key].IsTimeOut)
                {
                    dic.TryRemove(key, out data);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public TValue GetValue(TKey key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key].Value;
            }
            throw new KeyNotFoundException();
        }
        public IEnumerable<TKey> Keys
        {
            get
            {
                return dic.Where(p => p.Value.IsTimeOut == false).Select(p => p.Key);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return dic.Values.Where(p => p.IsTimeOut == false).Select(p => p.Value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return dic.ContainsKey(key);
        }

        bool Remove(TKey key)
        {
            DataModel<TValue> model;
            return dic.TryRemove(key, out model);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            DataModel<TValue> model = null; ;
            bool success = dic.TryGetValue(key, out model);
            value = model.Value;
            return success;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            DataModel<TValue> model = new DataModel<TValue>(item.Value, _timeOut);
            dic.TryAdd(item.Key, model);
        }

        public void Clear()
        {
            dic.Clear();
        }

        public int Count
        {
            get { return dic.Values.Where(p => !p.IsTimeOut).Count(); }
        }
    }

    public class DataModel<T>
    {
        private DateTime _Time;
        public T Value { get; private set; }
        public DataModel(T t)
            : this(t, 43200)
        { }

        public DataModel(T t, double time)
        {
            Value = t;
            _Time = DateTime.Now.AddSeconds(time);
        }
        /// <summary>
        /// 是否超时
        /// </summary>
        public bool IsTimeOut
        {
            get
            {
                return DateTime.Now > _Time;
            }
        }
    }
}

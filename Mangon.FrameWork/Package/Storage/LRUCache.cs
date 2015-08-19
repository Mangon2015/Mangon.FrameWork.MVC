using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Storage
{
    public class LRUCache<TValue> : IEnumerable<KeyValuePair<string, TValue>>
    {
        private long ageToDiscard = 0;  //淘汰的年龄起点
        private long currentAge = 0;        //当前缓存最新年龄
        private int maxSize = 0;          //缓存最大容量
        private readonly ConcurrentDictionary<string, TrackValue> cache;

        private TimeSpan maxTime;
        public LRUCache(int maxKeySize, TimeSpan maxExpireTime)
            : this(maxKeySize, maxExpireTime, 10)
        {
        }

        public LRUCache(int maxKeySize, TimeSpan maxExpireTime, int maxAge)
        {
            maxSize = maxKeySize;
            maxTime = maxExpireTime;
            cache = new ConcurrentDictionary<string, TrackValue>();
        }


        public LRUCache(int maxKeySize)
            : this(maxKeySize, new TimeSpan(1, 0, 0))
        {
        }


        public void Add(string key, TValue value)
        {
            Adjust(key);
            var result = new TrackValue(this, value);
            cache.AddOrUpdate(key, result, (k, o) => result);
        }
        public class TrackValue
        {
            //TrackValue增加创建时间和过期时间
            public readonly DateTime CreateTime;
            public readonly TimeSpan ExpireTime;

            public readonly TValue Value;
            public long Age;
            public TrackValue(LRUCache<TValue> lv, TValue tv)
            {
                Age = Interlocked.Increment(ref lv.currentAge);
                Value = tv;
                ExpireTime = lv.maxTime;
                CreateTime = DateTime.Now;
            }
        }

        /// <summary>
        ///当缓存到达最大容量时 移除缓存年龄最大的
        /// </summary>
        /// <param name="key"></param>
        private void Adjust(string key)
        {
            while (cache.Count >= maxSize)
            {
                long ageToDelete = Interlocked.Increment(ref ageToDiscard);
                var toDiscard = cache.FirstOrDefault(p => p.Value.Age == ageToDelete);
                if (toDiscard.Key == null)
                    continue;
                TrackValue old;
                cache.TryRemove(toDiscard.Key, out old);
            }
        }




        /// <summary>
        /// 移除过期元素，并返回二元组
        /// </summary>
        /// <param name="key"></param>
        /// <returns> </returns>
        public Tuple<TrackValue, bool> CheckExpire(string key)
        {
            TrackValue result;
            if (cache.TryGetValue(key, out result))
            {
                var age = DateTime.Now.Subtract(result.CreateTime);
                if (age >= maxTime || age >= result.ExpireTime)
                {
                    TrackValue old;
                    cache.TryRemove(key, out old);
                    return Tuple.Create(default(TrackValue), false);
                }
            }
            return Tuple.Create(result, true);
        }
        /// <summary>
        /// 检查
        /// </summary>
        public void Inspection()
        {
            foreach (var item in this)
            {
                CheckExpire(item.Key);
            }
        }

        public TValue GetValue(string key)
        {
            if (cache.ContainsKey(key))
            {
                return cache[key].Value;
            }
            return default(TValue);
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();

        }
    }
}

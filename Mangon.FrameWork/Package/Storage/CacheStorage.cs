using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Mangon.FrameWork.Result;
using System.Web.Caching;
using System.Threading;

namespace Mangon.FrameWork.Package.Storage
{
    /// <summary>
    /// HttpRuntime.Cache的封装
    /// </summary>
    public class CacheStorage:IStorage
    {
        private Dictionary<string, object> Store = new Dictionary<string, object>();
        private string place;
        private double expires;

        public Result.Result<object> Get(string name)
        {
            if (Store.ContainsKey(name))
            {
                return Result<object>.GetResult(true, Store[name]);
            }
            return Result<object>.GetResult(false);
        }

        public bool Set(string name, object data)
        {
            if (Store.ContainsKey(name))
            {
                Store[name] = data;
            }
            else
            {
                Store.Add(name, data);
            }
            try
            {
                HttpRuntime.Cache.Insert(place, Store, null, DateTime.Now.AddSeconds(expires), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.AboveNormal, null);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public bool Kill()
        {
            HttpRuntime.Cache.Remove(place);
            return true;
        }


        public CacheStorage(string name)
            : this(name, 3600)
        { }

        public CacheStorage(string name, double iExpires)
        {
            place = name;
            expires = iExpires;
            var data = HttpRuntime.Cache[name];
            if (data==null)
            {
                HttpRuntime.Cache.Insert(name, Store, null, DateTime.Now.AddSeconds(iExpires), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            else
            {
                Store = data as Dictionary<string, object>;
            }
        }


        public object GetMenberSigninDays(string cacheKey, Func<object, object, int> data)
        {
            const int cacheTime = 5;
            string cacheSign = cacheKey + "_Sign";
            var sign = CacheStorage.GetValue(cacheSign);
            var cacheValue = CacheStorage.GetValue(cacheKey);
            if (sign != null)
            {
                return cacheValue;
            }

            lock (cacheSign)
            {
                sign = CacheStorage.GetValue(cacheSign);
                if (sign != null)
                {
                    return cacheValue;
                }
                CacheStorage.Add(cacheSign, "1", cacheTime);//设置缓存
                ThreadPool.QueueUserWorkItem((arg) =>
                {
                    cacheValue = data(1, 2); //这里是获取数据的方法
                    CacheStorage.Add(cacheKey, cacheValue, cacheTime * 2);//日期设置缓存时间的两倍，用于读取脏数据
                });
            }

            return cacheValue;
        }

        private static object GetValue(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }
        public static void Add(string cacheKey, object obj, int cacheMinute)
        {
            HttpRuntime.Cache.Insert(cacheKey, obj, null, DateTime.Now.AddMinutes(cacheMinute),
                Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
    }
}

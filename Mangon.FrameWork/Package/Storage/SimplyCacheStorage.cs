using Mangon.FrameWork.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mangon.FrameWork.Package.Storage
{
    public class SimplyCacheStorage:IStorage
    {
        private string place;
        private double expires;

        public SimplyCacheStorage(string name)
            : this(name, 3600)
        { }
        public SimplyCacheStorage(string name, double iExpires)
        {
            place = name;
            expires = iExpires;
        }

        protected string GetKey(string name)
        {
            return string.Format("SimplyCacheStorage:{0}:{1}", place, name);
        }

        public Result.Result<object> Get(string name)
        {
            var obj = HttpRuntime.Cache.Get(GetKey(name));
            if (obj == null)
            {
                return Result<object>.False(null, "No this key");
            }
            else
            {
                return Result<object>.True(obj);
            }
        }

        public bool Set(string name, object data)
        {
            try
            {
                if (Get(name).Bool == true)
                {
                    HttpRuntime.Cache.Remove(GetKey(name));
                }
                HttpRuntime.Cache.Insert(GetKey(name), data, null, DateTime.Now.AddSeconds(expires), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.BelowNormal, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Kill()
        {
            return false;
        }

        /// <summary>
        /// 依赖缓存
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        /// <param name="dep"></param>
        /// <returns></returns>
        public bool Set(string name, object data, System.Web.Caching.CacheDependency dep)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                GetKey(name),
                data,
                dep,
                System.Web.Caching.Cache.NoAbsoluteExpiration, //从不过期
                System.Web.Caching.Cache.NoSlidingExpiration, //禁用可调过期
                System.Web.Caching.CacheItemPriority.Default,
                null);
            return true;
        }
    }
}

using Mangon.FrameWork.Elements;
using Mangon.FrameWork.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mangon.FrameWork.Package.Storage
{
    public class CookiesStorage : IStorage
    {
        private SerializableDictionary<string, string> Store = new SerializableDictionary<string, string>();

        private string place;
        private double expires;
        public Result<object> Get(string name)
        {
            if (Store.ContainsKey(name))
            {
                return Result<object>.GetResult(true, Store[name], null);
            }
            return Result<object>.GetResult(false, null, null);
        }


        public bool Set(string name, object data)
        {
            HttpCookie cookie = new HttpCookie(place);
            foreach (var item in Store)
            {
                cookie[item.Key] = item.Value;
            }
            if (expires > 0)
            {
                if (expires == 1)
                {
                    cookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddSeconds(expires);
                }
            }
            HttpContext.Current.Response.Cookies.Set(cookie);
            return true;
        }

        public bool Kill()
        {
            HttpContext.Current.Response.Cookies.Remove(place);
            return true;
        }



        public CookiesStorage(string name):this(name,3600)
        {
        }
        public CookiesStorage(string name, double expires)
        {
            Load(name, expires);
        }

        private void Load(string name, double iexpires)
        {
            place = name;
            expires = iexpires;
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie==null)
            {
                HttpCookie hcookie = new HttpCookie(place);
                hcookie.Expires = DateTime.Now.AddSeconds(iexpires);
                HttpContext.Current.Response.Cookies.Set(hcookie);
            }
            else
            {
                foreach (var key in cookie.Values.AllKeys)
                {
                    Store.Add(key, cookie[key]);
                }
            }
        }
    }
}

using Mangon.FrameWork.Result;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;

namespace Mangon.FrameWork.Package.Http
{
    public abstract class abApiReader
    {

        public static ConcurrentDictionary<string, int> autoList = new ConcurrentDictionary<string, int>();
        CacheItemRemovedCallback onRemove = null;

        public abApiReader()
        {
            onRemove = new CacheItemRemovedCallback(RemovedCallback);
        }
        public void RemovedCallback(String k, Object v, CacheItemRemovedReason r)
        {


            HttpRuntime.Cache.Remove(k);
            if (autoList.ContainsKey(k))//如果循环继续缓存
            {
                var res = ReadbyRes(k);//如果成功

                if (res.Bool) SetCacheOneTime(k, res.Data, autoList[k]);
                else
                {//3分钟重新缓存
                    SetCacheOneTime(k, v, 60 * 3);
                }
            }

        }

        protected void SetCacheForEvery(string key, Object value, int During)
        {
            if (!autoList.ContainsKey(key))
            {
                autoList.AddOrUpdate(key, During, (k, oldValue) => During);
            }
            HttpRuntime.Cache.Add(key, value, null, DateTime.Now.AddSeconds(During), Cache.NoSlidingExpiration, CacheItemPriority.High, onRemove);

        }

        protected void SetCacheOneTime(string key, Object value, int During)
        {
            HttpRuntime.Cache.Add(key, value, null, DateTime.Now.AddSeconds(During), Cache.NoSlidingExpiration, CacheItemPriority.High, onRemove);

        }

        internal string Now_T
        {
            get
            {
                return DateTime.Now.ToString(@"yyyy-MM-dd\THH:ii:ss");
            }
        }
        internal string Now
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:ii:ss");
            }
        }
        public string Read(string url)
        {
            //  object data = HttpRuntime.Cache.Get(url);
            //  if (data == null)
            // {
            return ReadbyRes(url).Data;
            //  }
        }

        public string Read(object data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);
        }

        public string _ReadOnlyCahce(string url, int During)
        {
            object data = HttpRuntime.Cache.Get(url);
            if (data == null)
            {
                try
                {
                    var res = ReadbyRes(url);
                    if (res.Bool) SetCacheForEvery(url, res.Data, During);
                    data = res.Data;
                }
                catch { };
            }

            return data.ToString();
        }

        public string _ReadOverCahce(string url, int During)
        {
            object data = HttpRuntime.Cache.Get(url);
            if (data == null)
            {
                try
                {
                    var res = ReadbyRes(url);
                    if (res.Bool) SetCacheOneTime(url, res.Data, During);
                    data = res.Data;
                }
                catch { }
            }

            if (data == null) return null;
            return data.ToString();
        }



        protected Result<string> ReadbyRes(string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                url = wc.DownloadString(url);
            }
            return Result<string>.True(url);
        }
        protected Result<string> SendbyRes(string url, string data)
        {
            url = Post(url, data);
            return Result<string>.True(url);
        }
        public string Post(string url, string strPost)
        {
            string result = "";

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            //objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/json";//提交xml 
            //objRequest.ContentType = "application/x-www-form-urlencoded";//提交表单
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                //return e.Message;
                throw e;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}

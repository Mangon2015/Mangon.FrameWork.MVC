using Mangon.FrameWork.Package.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mangon.FrameWork.Package.Language;

namespace Mangon.FrameWork.MVC.MVC
{
    public static class HtmlHelperExtension
    {



        /// <summary>
        /// 需要用本地路径进行文件依赖缓存
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string IncludeFile(this HtmlHelper helper, string Path, bool fromHttp = true)
        {
            string data = "";
            try
            {
                var st = (SimplyCacheStorage)Mangon.FrameWork.Package.Storage.StorageFactory.GetInstance("simplycache", "Include");
                var r = st.Get(Path);
                if (!r.Bool)
                {
                    String file = HttpContext.Current.Server.MapPath(Path);
                    if (!File.Exists(file))
                    {
                        return "";
                    }

                    WebClient wc = new WebClient();
                    if (fromHttp)
                    {
                        String Header = helper.ViewContext.RequestContext.HttpContext.Request.Url.Scheme + "://" + helper.ViewContext.RequestContext.HttpContext.Request.Url.Host;
                        String Url = "";
                        if (Path.StartsWith("~"))//处理地址
                        {
                            Url = Path.Substr(1);
                        }
                        else
                        {
                            Url = Path;
                        }
                        data = wc.DownloadString(Header + Url);
                    }
                    else
                    {
                        data = wc.DownloadString(file);
                    }
                    st.Set(Path, data);
                }//end if.r.bool
                else
                {
                    data = r.Data.ToString();
                }//end if r.bool
            }//end try
            catch (Exception e) { data = e.Message; }
            finally { }
            return data;
        }
        public static string Include(this HtmlHelper helper, string Path)
        {
            string data = "";
            try
            {
                var st = Mangon.FrameWork.Package.Storage.StorageFactory.GetInstance("cache", "Include");
                var r = st.Get(Path);
                if (!r.Bool)
                {
                    WebClient wc = new WebClient();
                    data = wc.DownloadString(helper.ViewContext.RequestContext.HttpContext.Request.Url.Scheme + "://" + helper.ViewContext.RequestContext.HttpContext.Request.Url.Host + Path);
                    st.Set(Path, data);
                }//end if.r.bool
                else
                {
                    data = r.Data.ToString();
                }//end if r.bool
            }//end try
            catch (Exception e) { data = e.Message; }
            finally { }
            return data;
        }

        public static string Include(this HtmlHelper helper, string Path, TimeSpan cacheTimer, bool IsAbsolute)
        {
            string key = "FamilyDoctor_Include+" + Mangon.FrameWork.Package.Encryption.MD5Encryption.GetMd5(Path);
            var rs = helper.ViewContext.HttpContext.Cache.Get(key);
            if (rs != null)
                return rs.ToString();

            rs = helper.Include(Path);
            if (IsAbsolute)
                helper.ViewContext.HttpContext.Cache.Add(key, rs, null, DateTime.Now.Add(cacheTimer), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Low, null);
            else
                helper.ViewContext.HttpContext.Cache.Add(key, rs, null, System.Web.Caching.Cache.NoAbsoluteExpiration, cacheTimer, System.Web.Caching.CacheItemPriority.Low, null);
            return rs.ToString();
        }
    }


    public interface IDataHelper
    {
    }

    /// <summary>
    /// 扩展自htmlhelper 的数据助手
    /// </summary>
    public class DataHelper : HtmlHelper, IDataHelper
    {
        /// <summary>
        /// 扩展自htmlhelper 的数据助手 重载
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="viewDataContainer"></param>
        /// <param name="routeCollection"></param>      
        public DataHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, System.Web.Routing.RouteCollection routeCollection)
            : base(viewContext, viewDataContainer, routeCollection)
        {

        }
        /// <summary>
        /// 扩展自htmlhelper 的数据助手 重载
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="viewDataContainer"></param>
        public DataHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : base(viewContext, viewDataContainer
          )
        {
        }
    }

    /// <summary>
    /// DataHelpers
    /// </summary>
    public static partial class DataHelpers
    {
        private static SelectListItem sliNull()
        {
            SelectListItem sli = new SelectListItem();
            sli.Text = "----";
            sli.Value = null;
            return sli;
        }
        public static IEnumerable<SelectListItem> SelectList<T>(this IDataHelper helper, IEnumerable<KeyValuePair<object, object>> datas, bool hasNull = false)
        {
            List<SelectListItem> l = new List<SelectListItem>();
            if (hasNull) l.Add(sliNull());

            foreach (var i in datas)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = i.Key != null ? i.Key.ToString() : "";
                sli.Value = i.Value != null ? i.Value.ToString() : "";
                l.Add(sli);
            }
            return l;
        }
        public static IEnumerable<SelectListItem> SelectList<T>(this IDataHelper helper, IEnumerable<KeyValuePair<string, string>> datas, bool hasNull = false)
        {
            List<SelectListItem> l = new List<SelectListItem>();
            if (hasNull) l.Add(sliNull());

            foreach (var i in datas)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = i.Key;
                sli.Value = i.Value;
                l.Add(sli);
            }
            return l;
        }

        public static IEnumerable<SelectListItem> SelectList<T>(this IDataHelper helper, bool hasNull = false) where T : struct
        {

            

            List<SelectListItem> l = new List<SelectListItem>();
            if (hasNull) l.Add(sliNull());
            foreach (T i in Enum<T>.AsEnumerable())
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = (string)i.ToString();
                sli.Value = Convert.ToInt32(Enum.Parse(typeof(T), sli.Text)).ToString();
                l.Add(sli);
            }
            return l;
        }

        public static string Url(this IDataHelper helper, String ActionName)
        {
            return Url(helper, ActionName, DataHelpers.ControllerName(helper));
        }
        public static string Url(this IDataHelper helper, String ActionName, String ControllerName)
        {
            string format = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/{0}/{1}/";
            return Url(helper, ActionName, ControllerName, format);
        }
        public static string Url(this IDataHelper helper, String ActionName, String ControllerName, String Format)
        {
            return string.Format(Format, ControllerName, ActionName);
        }


        /// <summary>
        /// 获取当前动作名
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string ActionName(this IDataHelper helper)
        {
            return ((DataHelper)helper).ViewContext.RouteData.Values["action"].ToString().ToLower();
        }
        /// <summary>
        /// 当前控制器名
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string ControllerName(this IDataHelper helper)
        {
            return ((DataHelper)helper).ViewContext.RouteData.Values["controller"].ToString().ToLower();
        }
        /// <summary>
        /// 当前视图名
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string ViewName(this IDataHelper helper)
        {
            var view = ((DataHelper)helper).ViewContext.View;
            if (view is BuildManagerCompiledView)
            {
                string viewName = ((BuildManagerCompiledView)view).ViewPath;
                viewName = viewName.Substring(viewName.LastIndexOf('/'));
                viewName = Path.GetFileNameWithoutExtension(viewName);
                return viewName.ToLower();
            }
            return string.Empty;
        }



        /// <summary>
        /// 按照id翻译中文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Trans<T>(this IDataHelper helper, short id)
        {
            return ((int)id).Trans<T>();
        }
        /// <summary>
        /// 按照id翻译中文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Trans<T>(this IDataHelper helper, int id)
        {
            return id.Trans<T>();
        }
        /// <summary>
        /// 按照文本翻译
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Trans<T>(this IDataHelper helper, string str)
        {
            return str.Trans<T>();
        }
        /// <summary>
        /// 由文字反推数字
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Trans(this IDataHelper helper, string Name, int id)
        {
            return id.Trans(Name);
        }

        public static string Trans(this IDataHelper helper, string Name, short id)
        {
            return ((int)id).Trans(Name);
        }
        /// <summary>
        /// 由文字反推文本
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Name"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Trans(this IDataHelper helper, string Name, string str)
        {
            return str.Trans(Name);
        }
        /// <summary>
        /// 搜索默认字典
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Trans(this IDataHelper helper, string str)
        {
            return str.Trans("Global");
        }
        /// <summary>
        /// 搜索默认字典
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Trans(this IDataHelper helper, int id)
        {
            return id.Trans("Global");
        }
        /// <summary>
        /// 翻译,由空格分词
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Translate(this IDataHelper helper, string str)
        {
            return str.Translate();


        }

        public static String Host
        {
            get
            {
#if DEBUG
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/";
#else

              return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
#endif


            }
        }
        public static int Port
        {
            get
            {
                return HttpContext.Current.Request.Url.Port;
            }
        }


    }
}

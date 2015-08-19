using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Mangon.FrameWork.MVC.MVC
{
    /// <summary>
    /// 导航栏
    /// </summary>
    public class NavigationBar
    {
        private WebViewPage vb;
        public NavigationBar(WebViewPage vb)
        {
            this.vb = vb;
        }
        public void Set(string Name)
        {
            Set(Name, "");
        }
        public void Set(string Name, String Url, string Class = "", string Id = "", string Param = "", string OnClick = "")
        {

            Dictionary<string, NavigationBarDict> dict = vb.ViewBag.NavigationBar;
            if (dict == null)
            {
                dict = new Dictionary<string, NavigationBarDict>();
            }

            if (!dict.ContainsKey(Name))
            {
                dict.Add(Name, new NavigationBarDict() { Name = Name, Url = Url, Class = Class, Id = Id, Param = Param, OnClick = OnClick });

            }
            vb.ViewBag.NavigationBar = dict;
        }
        public List<NavigationBarDict> GetDict()
        {
            Dictionary<string, NavigationBarDict> dict = vb.ViewBag.NavigationBar;
            if (dict == null)
            {
                dict = new Dictionary<string, NavigationBarDict>();
                vb.ViewBag.NavigationBar = dict;
            }

            List<NavigationBarDict> res = dict.Values.ToList();
            for (int i = 0; i < res.Count; i++)
            {
                res[i].Index = i;
                if (i == 0)
                {
                    res[i].IsFist = true;
                }

                if (i == res.Count)
                {
                    res[i].IsLast = true;
                }
            }

            return res;
        }
        public MvcHtmlString Render(HtmlHelper helper)
        {
            return helper.Partial("NavigationBar");
        }
        public MvcHtmlString Render(HtmlHelper helper, string Name)
        {
            return helper.Partial(Name);
        }
        public MvcHtmlString Render(HtmlHelper helper, object Model)
        {
            return helper.Partial("NavigationBar", Model);
        }
        public MvcHtmlString Render(HtmlHelper helper, string Name, object Model)
        {
            return helper.Partial(Name, Model);
        }
    }

    public class NavigationBarDict
    {
        public String Name { get; set; }
        public String Url { get; set; }
        public String Class { get; set; }
        public String Id { get; set; }
        public String Param { get; set; }
        public string OnClick { get; set; }
        public int Index = 0;
        public bool IsFist = false;
        public bool IsLast = false;
    }
}

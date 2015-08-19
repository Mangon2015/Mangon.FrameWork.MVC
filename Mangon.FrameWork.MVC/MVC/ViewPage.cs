using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC.MVC
{
    /// <summary>
    /// 自定义的视图页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewPage<T> : WebViewPage<T>
    {
        /// <summary>
        /// 重载 自定义的视图页
        /// </summary>
        public ViewPage()
            : base()
        {


        }
        /// <summary>
        /// 自定义的DataHelp
        /// </summary>
        private DataHelper data { get; set; }
        /// <summary>
        /// 自定义的DataHelp
        /// </summary>
        public IDataHelper Data
        {
            get
            {
                if (data == null)
                {
                    data = new DataHelper(Html.ViewContext, Html.ViewDataContainer, Html.RouteCollection);
                }
                return data;

            }
        }
        /// <summary>
        /// 全局的导航栏
        /// </summary>
        public NavigationBar navigationBar
        {
            get
            {
                if (_navigationBar == null)
                {
                    _navigationBar = new NavigationBar(this);
                }
                return _navigationBar;
            }
        }
        private NavigationBar _navigationBar = null;

    }
}

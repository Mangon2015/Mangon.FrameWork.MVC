using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC.MVC
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ExceptionErrorAttribute : FilterAttribute, IExceptionFilter
    {

        private Type _exceptionType = typeof(Exception);
        private string _master;
        private readonly object _typeId = new object();
        private string _view;
        private const string DefaultView = "404";

        public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (!filterContext.IsChildAction && (!filterContext.ExceptionHandled && filterContext.HttpContext.IsCustomErrorEnabled))
            {
                Exception innerException = filterContext.Exception;
                if ((new HttpException(null, innerException).GetHttpCode() == 404))
                {
                    string controllerName = (string)filterContext.RouteData.Values["controller"];
                    string actionName = (string)filterContext.RouteData.Values["action"];
                    HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                    ViewResult result = new ViewResult
                    {
                        ViewName = this.View,
                        MasterName = this.Master,
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                        TempData = filterContext.Controller.TempData
                    };
                    filterContext.Result = result;
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 404;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                    filterContext.HttpContext.Response.ClearHeaders();
                }
            }
        }

        public string Master
        {
            get
            {
                return (this._master ?? string.Empty);
            }
            set
            {
                this._master = value;
            }
        }

        public override object TypeId
        {
            get
            {
                return this._typeId;
            }
        }

        public string View
        {
            get
            {
                if (string.IsNullOrEmpty(this._view))
                {
                    return "Error";
                }
                return this._view;
            }
            set
            {
                this._view = value;
            }
        }
    }
}

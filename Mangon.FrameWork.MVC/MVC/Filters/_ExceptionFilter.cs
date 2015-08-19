using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mangon.FrameWork.Result;

namespace Mangon.FrameWork.MVC.MVC.Filters
{
    /// <summary>
    /// 全局拦截异常用
    /// </summary>
    public class _ExceptionFilter : IExceptionFilter
    {
        protected string ControllerName
        {
            get
            {
                try
                {
                    return _filterContext.RouteData.Values["controller"] as string;
                }
                catch { return null; }
            }
        }
        protected string ActionName
        {
            get
            {
                try
                { return _filterContext.RouteData.Values["action"] as string; }
                catch { return null; }
            }
        }
        protected Type ControllerType
        {
            get
            {
                try
                {
                    return _filterContext.Controller.GetType();
                }
                catch { return null; }
            }
        }
        protected Type ResultType
        {
            get
            {
                try
                {
                    if (ControllerType != null)
                    {
                        return ControllerType.GetMethod(ActionName).ReturnType;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch { return null; }
            }
        }
        /// <summary>
        /// 是否成功处理异常
        /// </summary>
        protected virtual bool IsExceptionFinish { get { return true; } }
        /// <summary>
        /// 其他异常
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void OnUnknowResult(ExceptionContext filterContext)
        {
            OnActionResult(filterContext);
        }
        /// <summary>
        /// 视图异常,默认跳去Error
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void OnActionResult(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewData.Add("Exception", filterContext.Exception);
            filterContext.Result = new ViewResult()//设置异常结果  
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData
            };
        }
        /// <summary>
        /// JSON异常,默认返回Result的JSON值 data内是 controller,action
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void OnJsonResult(ExceptionContext filterContext)
        {
            dynamic data = new
            {
                controller = filterContext.RouteData.Values["controller"] as string,
                action = filterContext.RouteData.Values["action"] as string
            };

            // filterContext.Controller.ViewData.Add("Exception", filterContext.Exception);
            Mangon.FrameWork.Result.Result r = Mangon.FrameWork.Result.Result.GetResult(false, filterContext, filterContext.Exception);
            filterContext.Result = new JsonResult()//设置异常结果  
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected virtual void BeforeOnException(ExceptionContext filterContext) { }
        protected virtual void AfterOnException(ExceptionContext filterContext) { }
        protected ExceptionContext _filterContext { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void OnException(ExceptionContext filterContext)
        {
            BeforeOnException(filterContext);
            _filterContext = filterContext;
            string Rtn = "";
            if (ResultType != null)
            {
                Rtn = ResultType.Name;
            }

            switch (Rtn)
            {
                case "JsonResult":
                    OnJsonResult(filterContext);
                    break;
                case "ActionResult":
                    OnActionResult(filterContext);
                    break;
                default:
                    OnUnknowResult(filterContext);
                    break;

            }

            filterContext.ExceptionHandled = IsExceptionFinish;
            AfterOnException(filterContext);
        }
    }
}

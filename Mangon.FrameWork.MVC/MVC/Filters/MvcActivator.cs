using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC.MVC.Filters
{
    /// <summary>
    /// 注入式的控制器生成工厂 
    /// TODO:未完成,用处就是用于自定义注册控制器,
    /// </summary>
   public class MvcActivator : IControllerActivator
    {
        public IController Create(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {

            if (controllerType == null)
            {
                throw new HttpException(0x194, string.Format(CultureInfo.CurrentCulture, "NoControllerFound", new object[] { requestContext.HttpContext.Request.Path }));
            }
            if (!typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "TypeDoesNotSubclassControllerBase", new object[] { controllerType }), "controllerType");
            }

            return DependencyResolver.Current
              .GetService(controllerType) as IController;
        }
    }
}

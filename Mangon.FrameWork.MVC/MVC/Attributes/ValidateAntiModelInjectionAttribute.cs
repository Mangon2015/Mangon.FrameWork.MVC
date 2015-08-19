using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Mangon.FrameWork.MVC
{
    /// <summary>
    /// 用于与Html.AntiModelInjection结合（O => o.PropertyId）写一个加密的属性，而把验证它的控制器。这是用来帮助防止参数从用户篡改。用户可以例如更改命名表用户ID的隐藏价值改变的用户是不是“他们”。
    /// </summary>
   public class ValidateAntiModelInjectionAttribute:ActionFilterAttribute
    {
       /// <summary>
        /// The name of the property we are generating a hash for.
       /// </summary>
       private readonly string _propertyName;
       public ValidateAntiModelInjectionAttribute(string propertyName)
       {
           _propertyName = propertyName;
           if (string.IsNullOrEmpty(propertyName))
           {
               throw new ArgumentException("propertyName 不能为空！");
           }
       }
       public override void OnActionExecuting(ActionExecutingContext filterContext)
       {

           string encryptedPropertyName = string.Format("_{0}Token", _propertyName);
           string hashToken = filterContext.HttpContext.Request.Form[encryptedPropertyName];

           if (string.IsNullOrEmpty(hashToken))
           {
                throw new MissingFieldException(string.Format("The hidden form field named value {0} was missing. This is created by the Html.AntiModelInjection methods. Ensure the name used on your [ValidateAntiModelInjectionAttribute(\"!HERE!\")] matches the field name used in Html.AntiModelInjection method. If this attribute is used on a controller method that is meant for HttpGet, then the form value would not yet exist. This attribute is meant to be used on controller methods accessed via HttpPost.", encryptedPropertyName));
           }

           string formValue = filterContext.HttpContext.Request.Form[_propertyName];
           if (string.IsNullOrEmpty(formValue))
           {
                throw new MissingFieldException(string.Format("The form value {0} was missing. If this attribute is used on a controller method that is meant for HttpGet, then the form value would not yet exist. This attribute is meant to be used on controller methods accessed via HttpPost.", _propertyName));
           }
           string hashedFormValue = FormsAuthentication.HashPasswordForStoringInConfigFile(formValue, "SHA1");

            

           //And compare
           if (string.Compare(hashedFormValue, hashToken, false, CultureInfo.InvariantCulture) != 0)
           {
               throw new Exception(string.Format("Failed security validation for {0}. It is possible the data was tampered with as the original value used to create the form field does not match the current property value for this field.", _propertyName));
               
           }

           filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
               filterContext.ActionDescriptor.ActionName);
           base.OnActionExecuting(filterContext);
       }
    }
}

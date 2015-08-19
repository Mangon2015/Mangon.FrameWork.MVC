using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Configuration;

namespace Mangon.FrameWork.MVC.MVC.Attributes
{
    public class ReCaptchaValidationAttribute : ActionFilterAttribute
    {
        private const string ChallengeFieldKey = "recaptcha_challenge_field";
        private const string ResponseFieldKey = "recaptcha_response_field";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[ChallengeFieldKey];
            var captchResponseValue = filterContext.HttpContext.Request.Form[ResponseFieldKey];

            base.OnActionExecuting(filterContext);
        }
    }
}

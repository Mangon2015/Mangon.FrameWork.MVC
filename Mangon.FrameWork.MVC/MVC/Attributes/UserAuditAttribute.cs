using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC
{
    public class UserAuditAttribute : ActionFilterAttribute
    {
        public UserAuditAttribute()
        { }

        public override bool AllowMultiple
        {
            get{ return false;}
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override Task OnActionExecutingAsync(ActionExecutedContext filterContext,
         CancellationToken cancellationToken)
        {
           
          //  var userName = _userSession.Username;
            string userName="";
            return Task.Run(() => AuditCurrentUser(userName), cancellationToken);
        }
        public void AuditCurrentUser(string username)
        {
            // Simulate long auditing process
          //  _log.InfoFormat("Action being executed by user={0}", username);
            Thread.Sleep(3000);
        }
    }
}

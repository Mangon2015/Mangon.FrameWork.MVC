using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC.MVC.Filters
{
    /// <summary>
    /// 登录与权限验证模块 ,使用ACL
    /// </summary>
    public class _AuthorizationFilter : System.Web.Mvc.IAuthorizationFilter
    {

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            #region 验证登录Ip
            //需要检查ip
            if (AuthorizationIpList == null || AuthorizationIpList.Count > 0)
            {
                var remoteaddr = filterContext.RequestContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
                if (AuthorizationIpList.Contains(remoteaddr))
                {
                    JumptoError(filterContext);
                    return;
                }

            }
            #endregion


            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string action = filterContext.ActionDescriptor.ActionName.ToLower();
            string Path = controller + "_" + action;

            #region 放行名单
            //如果遇到白名单,放行
            if (NotAuthorizationControllerList != null && NotAuthorizationControllerList.Contains(controller))
            {

                return;
            }

            //如果遇到白名单,放行
            if (NotAuthorizationControllerActionList != null && NotAuthorizationControllerActionList.Any(p => p.ToLower() == Path))
            {

                return;
            }



            #endregion


            //#region 认证登录
            ////是否登陆
            //var getUser = UserService.getUser();
            //if (getUser == null || getUser.UserID <= 0)
            //{
            //    JumptoLogin(filterContext);
            //    return;
            //}
            //var _client = new Client();


            ////审核角色


            //// if (UserService.CheckPermission(getUser.Role.Select(p => p.RoleID).ToArray(), Path))
            //// foreach(var c in getUser.Role){
            ////if(c.Permission.Count(p=>p.RuleValue==Path)>0)
            //int[] roleids = UserService.getUser().Role.Select(p => p.RoleID).ToArray();
            //if (UserService.CheckPermission(roleids, Path))
            //{

            //    return;
            //}


            ////}
            //JumptoError(filterContext);
            //#endregion

        }




        private static bool _AuthorizationThrowException = false;
        private static bool _LoginThrowException = false;
        private static Encoding _JsonEncoding = Encoding.UTF8;
        private static string _JsonContentType = "application/json";
        private static string _AuthorizationFalsMessage = "你没有权限执行该操作";
        private static string _loginMessage = "请登录";
        private static string _AuthorizationFalsepage = null;
        private static string _loginpage = null;

        public static void SetThrowException(bool Authorization, bool Login)
        {
            _AuthorizationThrowException = Authorization;
            _LoginThrowException = Login;
        }
        public static void SetJsonStatus(Encoding Encoding, string ContentType)
        {
            _JsonEncoding = Encoding;
            _JsonContentType = ContentType;
        }
        /// <summary>
        /// 设置关键页面
        /// </summary>
        /// <param name="LoginUrl">登录地址</param>
        /// <param name="FalsePage">失败地址</param>
        public static void SetPage(string LoginUrl, string FalsePage)
        {
            _loginpage = LoginUrl;
            if (string.IsNullOrWhiteSpace(FalsePage)) FalsePage = LoginUrl;
            _AuthorizationFalsepage = FalsePage;
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="LoginMessage"></param>
        /// <param name="FalseMessage"></param>
        public static void SetMsg(string LoginMessage, string FalseMessage)
        {

            if (!string.IsNullOrWhiteSpace(LoginMessage))
            {
                _loginMessage = LoginMessage;
            }
            if (!string.IsNullOrWhiteSpace(FalseMessage))
            {
                _AuthorizationFalsMessage = FalseMessage;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Appkey">系统密匙</param>
        /// <param name="LoginPage">登录页面</param>
        /// <param name="ErroPage">异常页面</param>
        /// <param name="ControllerList">不用检查的白名单列表</param>
        /// <param name="IpList">允许访问的ip列表,为空则不检查</param>
        public static void Init(string Appkey, string LoginPage, string ErroPage, List<string> ControllerList, List<string> IpList)
        {
            _AuthorizationFilter.Appkey = Appkey;
            SetPage(LoginPage, ErroPage);
            NotAuthorizationControllerList = ControllerList;
            AuthorizationIpList = IpList;
        }
        /// <summary>
        /// 系统密匙
        /// </summary>
        public static string Appkey = null;
        /// <summary>
        /// 白名单,不用检查的控制器列表
        /// </summary>
        public static List<string> NotAuthorizationControllerList = new List<string>();
        /// <summary>
        /// 白名单,不用检查的控制器方法列表
        /// </summary>
        public static List<string> NotAuthorizationControllerActionList = new List<string>();
        /// <summary>
        /// Ip白名单,指允许访问的ip,默认为空
        /// </summary>
        public static List<string> AuthorizationIpList = new List<string>();
        /// <summary>
        /// 跳去登录页(ActionVIew)
        /// </summary>
        /// <param name="filterContext"></param>
        private void JumptoLogin(System.Web.Mvc.AuthorizationContext filterContext)
        {

            var result = getActionResultType(filterContext);
            ActionResult ar = null;
            switch (result.Name)
            {
                case "JavaScriptResult":
                case "JsonResult":
                    ar = JsontoLogin(filterContext);
                    break;
                case "PartialViewResult":
                case "ActionResult":
                case "RedirectToRouteResult":
                case "RedirectResult":
                case "ViewResult":
                default:
                    ar = ViewtoLogin(filterContext);
                    break;
            }
            filterContext.Result = ar;


        }

        /// <summary>
        /// 跳去出错页面(ActionView)
        /// </summary>
        /// <param name="filterContext"></param>
        private void JumptoError(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var result = getActionResultType(filterContext);
            ActionResult ar = null;
            switch (result.Name)
            {
                case "JavaScriptResult":
                case "JsonResult":
                    ar = JsontoError(filterContext);
                    break;
                case "PartialViewResult":
                case "ActionResult":
                case "RedirectToRouteResult":
                case "RedirectResult":
                case "ViewResult":
                case "FileResult":
                default:
                    ar = ViewtoError(filterContext);
                    break;
            }
            var v = new ContentResult();

            filterContext.Result = ar;

        }

        /// <summary>
        /// 跳去登录页(ActionVIew)
        /// </summary>
        /// <param name="filterContext"></param>
        private ActionResult ViewtoLogin(System.Web.Mvc.AuthorizationContext filterContext)
        {
            UrlHelper url = new UrlHelper(filterContext.RequestContext);
            string requestUrl = filterContext.RequestContext.HttpContext.Request.Url.ToString();
            return new RedirectResult(_loginpage);


        }



        private ActionResult ViewtoError(System.Web.Mvc.AuthorizationContext filterContext)
        {
            UrlHelper url = new UrlHelper(filterContext.RequestContext);
            string requestUrl = filterContext.RequestContext.HttpContext.Request.Url.ToString();
            return new RedirectResult(_AuthorizationFalsepage);

        }
        /// <summary>
        /// 跳去出错页面(ActionView)
        /// </summary>
        /// <param name="filterContext"></param>
        private ActionResult JsontoError(System.Web.Mvc.AuthorizationContext filterContext)
        {

            var jr = new JsonResult();
            jr.ContentEncoding = _JsonEncoding;
            jr.ContentType = _JsonContentType;
            jr.Data = Result.Result.getResult(false, _AuthorizationFalsMessage);
            return jr;
        }
        /// <summary>
        /// 跳去出错页面(ActionView)
        /// </summary>
        /// <param name="filterContext"></param>
        private ActionResult JsontoLogin(System.Web.Mvc.AuthorizationContext filterContext)
        {

            var jr = new JsonResult();
            jr.ContentEncoding = _JsonEncoding;
            jr.ContentType = _JsonContentType;

            jr.Data = Result.Result.GetResult(false, _loginMessage, _loginpage);
            return jr;
        }

        protected BaseResult getActionResultType(System.Web.Mvc.AuthorizationContext filterContext)
        {
            BaseResult br = new BaseResult();
            Type controllertype = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType;
            var Methods = controllertype.GetMethods();
            // Methods.FirstOrDefault(p=>p.Name.ToLower()==
            MethodInfo Method = null;
            foreach (var m in Methods)
            {
                if (filterContext.ActionDescriptor.ActionName.ToLower() == m.Name.ToLower())
                {
                    Method = m;
                    break;
                }
            }
            //.GetMethod(filterContext.ActionDescriptor.ActionName);
            br.type = Method.ReturnType;
            if (br.type == typeof(ActionResult))
            {
                br.result = new ViewResult();
            }
            else
            {

                br.result = br.type.Assembly.CreateInstance(br.type.FullName) as ActionResult;
            }
            return br;

        }


        protected class BaseResult
        {
            public Type type;
            public ActionResult result;
            public string Name
            {
                get
                {
                    return type.Name;
                }
            }
        }
    }
}

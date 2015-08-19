using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace Mangon.FrameWork.MVC
{
    /// <summary>
    /// 是否更改
    /// </summary>
    public class IsNotModifiedAttribute:ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            var request = filterContext.HttpContext.Request;
            if ((IsSourceModified(request,response)==false))
            {
                response.SuppressContent = true;
                response.StatusCode = 304;
                response.StatusDescription = "Not Modified";
                //明确设置Content-Length头，所以客户端不会等待内容，但保持连接打开其他请求
                response.AddHeader("Content-Length", "0");
            }
        }
        private static bool IsSourceModified(HttpRequestBase request, HttpResponseBase response)
        {
            bool dateModified = false;
            bool eTagModified = false;
            string requestETagHeader = request.Headers["If-None-Match"] ?? string.Empty;
            string requestIfModifiedSinceHeader = request.Headers["If-Modified-Since"] ?? string.Empty;
            DateTime requestIfModifiedSince;
            DateTime.TryParse(requestIfModifiedSinceHeader, out requestIfModifiedSince);
            string responseETagHeader = response.Headers["ETag"] ?? string.Empty;
            string responseLastModifiedHeader = response.Headers["Last-Modified"] ?? string.Empty;
            DateTime responseLastModified;
            DateTime.TryParse(responseLastModifiedHeader, out responseLastModified);
            if (requestIfModifiedSince!=DateTime.MinValue&&responseLastModified!=DateTime.MinValue)
            {
                if (responseLastModified>requestIfModifiedSince)
                {
                    TimeSpan diff = responseLastModified - requestIfModifiedSince;
                    if (diff>TimeSpan.FromSeconds(1))
                    {
                        dateModified = true;
                    }
                }
            }
            else
            {
                dateModified = true;
            }
            ///保留默认的eTagModified= false，所以，如果我们 不从服务器获取一个ETag，我们将依靠fileDateModified唯一
            if (String.IsNullOrEmpty(responseETagHeader)==false)
            {
                eTagModified = responseETagHeader.Equals(requestETagHeader, StringComparison.Ordinal) == false;
            }
            return (dateModified||eTagModified);
        }
    }
}

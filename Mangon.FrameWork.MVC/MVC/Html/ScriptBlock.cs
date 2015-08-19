using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC
{
    public class ScriptBlock : IDisposable
    {

        private const string GLOBAL_SCRIPT_KEY = "__SCRIPTBLOCKS";
        public static IDictionary<string, string> PageScripts
        {
            get
            {
                if (HttpContext.Current.Items[GLOBAL_SCRIPT_KEY] == null)
                    HttpContext.Current.Items[GLOBAL_SCRIPT_KEY] = new Dictionary<string, string>();



                return (IDictionary<string, string>)HttpContext.Current.Items[GLOBAL_SCRIPT_KEY];
            }
        }

        public static void Register(string script, string key = null)
        {

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString();
            }

            if (!PageScripts.ContainsKey(key))
                PageScripts.Add(key, script);


        }

        private WebViewPage WebViewPage { get; set; }

        private string ScriptKey { get; set; }

        public ScriptBlock(WebViewPage viewPage, string key = null)
        {
            WebViewPage = viewPage;
            ScriptKey = key;
            WebViewPage.OutputStack.Push(new StringWriter());
        }

        public void Dispose()
        {
            var script = WebViewPage.OutputStack.Pop().ToString();
            Register(script, ScriptKey);


        }
    }
}

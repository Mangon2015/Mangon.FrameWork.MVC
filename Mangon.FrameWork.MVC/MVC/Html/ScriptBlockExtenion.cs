using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mangon.FrameWork.MVC
{
    public static class ScriptBlockExtenion
    {

        public static IDisposable BeginScriptBlock(this HtmlHelper helper, string scriptKey)
        {
            return new ScriptBlock((WebViewPage)helper.ViewDataContainer, scriptKey);
        }

        public static IDisposable BeginScriptBlock(this HtmlHelper helper)
        {
            return BeginScriptBlock(helper, null);
        }

        public static MvcHtmlString WriteScriptBlocks(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(string.Join(Environment.NewLine, ScriptBlock.PageScripts.Select(s => s.Value.ToString())));
        }


    }
}

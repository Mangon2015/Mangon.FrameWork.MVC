using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Paginator
{
    /// <summary>
    /// 设置分页
    /// </summary>
    public class PageSet : Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="Index"></param>
        /// <param name="Url"></param>
        public PageSet(int Start, int End, int Index, string Url)
        {
            this.StartIndex = Start;
            this.LastIndex = End;
            this.PageIndex = Index;
            this.Url = string.Format(Url, Index);
            // this.Html = string.Format(Html, Index);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="Index"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        //public static Page getPage(int Start, int End, int Index, string Url)
        //{
        //    var p = new PageSet(Start, End, Index, Url);
        //    var page = new Page();
        //    page.CloneIn<Page>(p);
        //    return page;
        //}

        public static Page getPage(int Index, int PageSize, int Total, string Url)
        {
            var start = Index;
            var end = (PageSize * Index) > Total ? Total : (PageSize * Index);
            var p = new PageSet(start, end, Index, Url);
            var page = new Page();
            page.CloneIn<Page>(p);
            return page;
        }
    }
}

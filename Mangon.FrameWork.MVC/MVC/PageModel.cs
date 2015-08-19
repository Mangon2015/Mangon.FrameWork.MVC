using Mangon.FrameWork.Package.SystemExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mangon.FrameWork.MVC.MVC
{
    public class PageModel
    {

        public int _Total = 0;
        private int _current = 0;
        public int _Current
        {
            get
            {
                if (_current < 1)
                {

                    string pagename = HttpContext.Current.Request.QueryString.AllKeys.Where(p => p != null).FirstOrDefault(p => p.ToLower() == _PageKey);
                    string pagevalue = pagename == null ? "1" : HttpContext.Current.Request.QueryString[pagename];
                    int.TryParse(pagevalue, out _current);

                }
                return _current;
            }

            set
            {
                _current = value;
            }
        }
        private int pagesize = 0;
        public int PageSize
        {
            get
            {
                if (pagesize == 0)
                {
                    return 20;
                }
                return pagesize;
            }
            set
            {
                pagesize = value;
            }
        }
        public int _Take
        {
            get
            {
                return PageSize;
            }
        }
        public int _Skip
        {
            get
            {
                _Current = _Current <= 0 ? 1 : _Current;
                return (_Current - 1) * PageSize;
            }
        }
        protected static string _PageKey = "page";
        /// <summary>
        /// 默认分页
        /// </summary>
        public static string PageKey
        {
            set
            {
                if (string.IsNullOrWhiteSpace(_PageKey)) throw new ArgumentNullException();
                _PageKey = value.ToLower();
            }

        }
        protected string _url = null;
        public string _Url
        {
            get
            {
                if (_url == null)
                {
                    _url = HttpContext.Current.Request.Path + HttpContext.Current.Request.QueryString.ToUrlFormat(new Dictionary<string, string>() { { _PageKey, "{0}" } }, Web.UrlEncodeType.UrlEncode, Encoding.UTF8);

                }
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public void Init()
        {

        }

        public Mangon.FrameWork.Package.Paginator.Paginator Paginator
        {
            get
            {
                Mangon.FrameWork.Package.Paginator.PaginatorSet ps = new Mangon.FrameWork.Package.Paginator.PaginatorSet(_Url, _Total, _Current, PageSize);
                return ps.Paginator();
            }
        }

        public void Init(int total, string Url, int? current)
        {

            _Total = total;
            _Current = current == null ? 1 : current.Value;
            _Url = Url;
        }


        public void Init(int total, string Url)
        {
            _Total = total;
            _Url = Url;
        }


        public void Init(int total)
        {

            _Total = total;

        }
        public void Init(int total, int? current)
        {
            _Current = current == null ? 1 : current.Value;

        }
    }
}

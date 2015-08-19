using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Paginator
{
    /// <summary>
    /// 一个分页
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 分页的html
        /// </summary>
        public string Html { get; protected set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; protected set; }
        /// <summary>
        /// 分页码
        /// </summary>
        public int PageIndex { get; protected set; }
        /// <summary>
        /// 开始坐标
        /// </summary>
        public int StartIndex { get; protected set; }
        /// <summary>
        /// 最终坐标
        /// </summary>
        public int LastIndex { get; protected set; }
        /// <summary>
        /// 条数
        /// </summary>
        public int Count
        {
            get
            {
                return LastIndex - StartIndex;
            }
        }
    }
}

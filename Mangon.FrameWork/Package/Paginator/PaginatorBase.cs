using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Paginator
{
    /// <summary>
    /// 分页
    /// </summary>
    public abstract class PaginatorBase
    {
        /// <summary>
        /// 索引前
        /// </summary>
        public int BeforeNum = 3;
        /// <summary>
        /// 索引后
        /// </summary>
        public int AfterNum = 6;

        public int ViewStartIndex { get; set; }
        public int ViewEndIndex { get; set; }

        public int ViewPanel
        {
            get
            {
                return BeforeNum + AfterNum + 1;
            }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentIndex { get; set; }
        /// <summary>
        /// 总数条数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Count { get; protected set; }
        /// <summary>
        /// 首页
        /// </summary>
        public int FirstIndex { get; protected set; }
        /// <summary>
        /// 尾页
        /// </summary>
        public int LastIndex { get; protected set; }
        /// <summary>
        /// 下一页
        /// </summary>
        public int NextIndex { get; protected set; }
        /// <summary>
        /// 上一页
        /// </summary>
        public int PrvIndex { get; protected set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 基础url
        /// </summary>
        public string BaseUrl { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Paginator
{
    /// <summary>
    /// 分页的实体
    /// </summary>
    public class Paginator : PaginatorBase
    {
        public Page First { get; set; }
        public Page Last { get; set; }
        public Page Prv { get; set; }
        public Page Next { get; set; }
        public List<Page> List { get; set; }
        public Page Current;
    }
}

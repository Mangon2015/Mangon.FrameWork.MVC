using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Result
{
    /// <summary>
    /// 通用返回类的非泛型基类
    /// </summary>
   public interface IResult : IResultError
    {
        /// <summary>
        /// 是否只读
        /// </summary>

        bool IsReadyOnly { get; }
        /// <summary>
        /// 成功或者失败
        /// </summary>

        Boolean Bool { get; set; }    
    }
}

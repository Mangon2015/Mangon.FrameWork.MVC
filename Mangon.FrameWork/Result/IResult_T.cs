using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Result
{
    /// <summary>
    /// 通用返回字段的泛型基类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>

    public interface IResult<T> : IResult, IResultError
    {

        /// <summary>
        /// 数据
        /// </summary>

        T Data { get; set; }


    }
}

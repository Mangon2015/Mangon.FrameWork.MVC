using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid
{
    /// <summary>
    /// 元素
    /// </summary>
    interface IValidElement
    {
        /// <summary>
        /// 元素名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 元素返回对象类型
        /// </summary>
        Type ResultType { get; }
        /// <summary>
        /// 是否允许空值
        /// </summary>
        bool HasEmpty { get; }
    }
}

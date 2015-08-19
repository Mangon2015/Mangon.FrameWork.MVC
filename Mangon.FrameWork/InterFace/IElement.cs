using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.InterFace
{
    /// <summary>
    /// 基本元素
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// 元素名称
        /// </summary>
        /// <returns></returns>
        string GetName();
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        IElement Clone();


    }
}

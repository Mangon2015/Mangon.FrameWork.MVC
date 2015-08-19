using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.InterFace
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public abstract class IFactory<T>
    {
        /// <summary>
        /// 本工厂的实例基类
        /// </summary>
        public abstract Type baseType { get; }
    }
}

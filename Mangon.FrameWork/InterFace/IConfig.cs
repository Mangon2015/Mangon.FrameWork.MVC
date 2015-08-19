using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.InterFace
{
    /// <summary>
    /// 配置接口 
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        void SetKeyValue(object key, object value);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Result
{
   public interface IResultError
    {
        /// <summary>
        /// 异常
        /// </summary>

        Exception Error { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>

        string ErrorMessage { get; set; }
    }
}

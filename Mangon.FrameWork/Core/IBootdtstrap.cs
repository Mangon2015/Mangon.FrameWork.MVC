using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Core
{
    /// <summary>
    /// 启动器接口
    /// </summary>
   public  interface IBootdtstrap
    {
       /// <summary>
       /// 实例化时候
       /// </summary>
       void OnInit();
       /// <summary>
       /// 启动后
       /// </summary>
       void OnAft();
       /// <summary>
       /// 启动前
       /// </summary>
       void OnPre();
       /// <summary>
       /// 设置事件
       /// </summary>
       void SetEvent();
    }
}

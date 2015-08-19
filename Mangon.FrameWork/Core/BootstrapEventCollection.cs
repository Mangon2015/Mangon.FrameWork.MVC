using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Core
{
    /// <summary>
    /// 系统基础架构启动器
    /// </summary>
   public  class BootstrapEventCollection : EventArgs
    {
       private IDictionary<string, string> _dict = new Dictionary<string, string>();
       /// <summary>
       /// 实例化时
       /// </summary>
       protected event BootstrapHandler Init;
       /// <summary>
       /// 当应用启动完时
       /// </summary>
       protected event BootstrapHandler aft_Application_Start;
       /// <summary>
       /// 启动前
       /// </summary>
       protected event BootstrapHandler pre_Application_Start;
       /// <summary>
       /// 初始化
       /// </summary>
       public void OnInit()
       {
           if (Init!=null)
           {
               Init.Invoke(this, this);
           }
       }
       /// <summary>
       /// 程序启动后
       /// </summary>
       public void OnAft_Application_Start()
       {
           if (aft_Application_Start!=null)
           {
               aft_Application_Start.Invoke(this, this);
           }
       }
       /// <summary>
       /// 程序启动前
       /// </summary>
       public void OnPre_Application_Start()
       {
           if (pre_Application_Start!=null)
           {
               pre_Application_Start.Invoke(this, this);
           }
       }
    }
}

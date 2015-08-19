using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Core
{
    /// <summary>
    /// 启动器事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">事件集合</param>
    public delegate void BootstrapHandler(object sender,BootstrapEventCollection e);
    /// <summary>
    /// 运行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RunEventHandler(object sender,EventArgs e);

}

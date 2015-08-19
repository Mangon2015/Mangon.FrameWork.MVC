using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue.Interface
{
    /// <summary>
    /// 一个任务
    /// </summary>
    public interface ITaskElement
    {
        int TryCount { get; }
        int TrySleep { get; }
        bool Try();
    }
}

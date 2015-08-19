using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue.Interface
{
    /// <summary>
    /// 执行器
    /// </summary>
    public interface IActuator<T>
    {
        KeyValuePair<bool, T> Run(T queue);
    }
}

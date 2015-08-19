using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue.Interface
{
    /// <summary>
    /// 一个队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueueServer<T> where T:ITaskElement
    {
        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <param name="item"></param>
        void PutTask(T item);
        /// <summary>
        /// 开始执行
        /// </summary>
        void Start();
        void Stop();
        int Count { get; }
        bool IsAlive { get; }
        IList<string> GetErrors();
    }
}

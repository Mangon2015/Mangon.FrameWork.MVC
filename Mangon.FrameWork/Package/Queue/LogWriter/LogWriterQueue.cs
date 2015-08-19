using Mangon.FrameWork.Package.Queue.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue.LogWriter
{
    /// <summary>
    /// 单线程单实例
    /// </summary>
    public class LogWriterQueue:IQueueServer<LogWriterTask>
    {
        private static LogWriterQueue _log = null;
        public static LogWriterQueue GetInstance()
        {
            if (_log==null)
            {
                _log = new LogWriterQueue();
            }
            return _log;
        }

        protected LogWriterActuator actuator = new LogWriterActuator();
        protected Thread run = null;
        protected object lockogj = new object();
        protected ConcurrentQueue<LogWriterTask> queue = null;
        protected List<string> _error = null;
        protected void SetError(Exception e)
        {
            if (_error.Count>=100)
            {
                lock (lockogj)
                {
                    _error.RemoveRange(50, _error.Count - 50);
                }
            }
            _error.Add(e.ToString());
        }
        private LogWriterQueue()
        {
            if (run==null)
            {
                lock (lockogj)
                {
                    run = new Thread(Run);
                    run.Start();
                }
                if (_error==null)
                {
                    lock (lockogj)
                    {
                        _error = new List<string>(100);
                    }
                }
                if (queue==null)
                {
                    lock (lockogj)
                    {
                        queue = new ConcurrentQueue<LogWriterTask>();
                    }
                }
            }
        }

        protected void Run()
        {
            while (!queue.IsEmpty)
            {
                try
                {
                    LogWriterTask task;
                    if (queue.TryDequeue(out task))
                    {
                        //执行
                        var result = actuator.Run(task);
                        if (task.Try())
                        {
                            //如果失败 从新进入队列
                            if (!result.Key)
                            {
                                queue.Enqueue(result.Value);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    SetError(e);
                }
                
            }
        }




        public void PutTask(LogWriterTask item)
        {
            queue.Enqueue(item);
            Start();
        }

        public void Start()
        {
            if (run==null||!run.IsAlive)
            {
                run = null;
                run = new Thread(Run);
                run.Start();
            }
        }

        public void Stop()
        {
            if (run.IsAlive)
            {
                run.Abort();
            }
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public bool IsAlive
        {
            get { return run.IsAlive; }
        }

        public IList<string> GetErrors()
        {
            return GetErrors();
        }
    }
    
}

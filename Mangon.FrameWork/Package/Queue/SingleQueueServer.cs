using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mangon.FrameWork.Package.Queue.Interface;
using System.Collections.Concurrent;
using System.Threading;

namespace Mangon.FrameWork.Package.Queue
{
   public class SingleQueueServer:IQueueServer<ITaskElement>,IDisposable
    {
       private IActuator<ITaskElement> _actuator = null;
       public IActuator<ITaskElement> Actuator
       {
           get { return _actuator; }
           set { _actuator = value; }
       }

       private object _lock = new object();
       private ConcurrentQueue<ITaskElement> _queue = null;
       private ConcurrentQueue<ITaskElement> Queue
       {
           get
           {
               if (_queue==null)
               {
                   lock (_lock)
                   {
                       _queue = new ConcurrentQueue<ITaskElement>();
                   }
               }
               return _queue;
           }
       }

       private Thread _thread = null;
       private Thread QueueThread
       {
           get {
               if (_thread == null)
               {
                   lock (_lock)
                   {
                       _thread = new Thread(Run);
                   }
               }
               return _thread;
           }
       }

       private List<string> _error = null;
       private List<string> Errors
       {
           get {
               if (_error==null)
               {
                   _error = new List<string>(100);
               }
               return _error;
           }
       }

       private void SetError(string msg)
       {
           lock (_lock)
           {
               while (Errors.Count>=100)
               {
                   Errors.RemoveAt(0);
               }
               Errors.Add(msg);
           }
       }
       private bool autorun = true;
       public bool AutoRun
       {
           get { return autorun; }
           set { autorun = value; }
       }

       private void Run() { }
       /// <summary>
       /// 推入任务
       /// </summary>
       /// <param name="item"></param>
        public void PutTask(ITaskElement item)
        {
            Queue.Enqueue(item);
            Start();
        }


       /// <summary>
       /// 启动
       /// </summary>
        public void Start()
        {
            if (!QueueThread.IsAlive)
            {
                QueueThread.Start();
            }
        }

        public void Stop()
        {
            if (QueueThread.IsAlive)
            {
                QueueThread.Abort();
            }
        }

        public int Count
        {
            get { return Queue.Count; }
        }

        public bool IsAlive
        {
            get { return QueueThread.IsAlive; }
        }

        public IList<string> GetErrors()
        {
            return GetErrors();
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_thread!=null)
                {
                    _thread.Join();
                    _thread.Abort();
                    _thread = null;
                    
                }
                if (_queue!=null)
                {
                    _queue = null;
                }
                if (_error!=null)
                {
                    _error = null;
                }
                _actuator = null;
            }
            GC.Collect();
        }
    }
}

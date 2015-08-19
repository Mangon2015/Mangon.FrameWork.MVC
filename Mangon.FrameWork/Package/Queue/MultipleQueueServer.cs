using Mangon.FrameWork.Package.Queue.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue
{
    /// <summary>
    /// 多线程执行同一个队列
    /// </summary>
    public class MultipleQueueServer : IQueueServer<ITaskElement>, IDisposable
    {
        private int _threadNum = 5;
        public int Threads
        {
            get { return _threadNum; }
            set
            {
                if (value>0)
                {
                    _threadNum = value;
                }
            }
        }

        private IActuator<ITaskElement> _actuator = null;
        public IActuator<ITaskElement> Actuator
        {
            get { return _actuator; }
            set{_actuator=value;}
        }
        private object _lock = new object();
        private ConcurrentQueue<ITaskElement> _queue = null;
        private ConcurrentQueue<ITaskElement> Queue
        {
            get {
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
        // 线程组
        private SynchronizedCollection<Thread> _thread = null;
        private SynchronizedCollection<Thread> QueueThread
        {
            get {
                if (_thread==null)
                {
                    lock (_lock)
                    {
                        _thread = new SynchronizedCollection<Thread>(_threadNum);
                        for (int i = 0; i < _threadNum; i++)
                        {
                            Thread t = new Thread(Run);
                            t.Start();
                            _thread.Add(t);
                        }
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
        private void Run()
        {
            while (AutoRun)
            {
                try
                {
                    if (Queue.IsEmpty)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    ITaskElement item;
                    if (Queue.TryDequeue(out item))
                    {
                        if (item.Try())
                        {
                            if (item.TryCount>0&&item.TrySleep>0)
                            {
                                Thread.Sleep(item.TrySleep);
                            }
                            var o = Actuator.Run(item);
                            if (!o.Key)
                            {
                                Queue.Enqueue(o.Value);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    SetError(e.ToString());
                }
            }
        }

        public void PutTask(ITaskElement item)
        {
            Queue.Enqueue(item);
            Start();
        }

        public void Start()
        {
            foreach (Thread item in QueueThread)
            {
                if (!item.IsAlive)
                {
                    item.Start();
                }
            }
        }

        public void Stop()
        {
            foreach (Thread item in QueueThread)
            {
                if (item.IsAlive)
                {
                    item.Abort();
                }
            }
        }

        public int Count
        {
            get { return Queue.Count; }
        }

        public bool IsAlive
        {
            get {

                foreach (Thread item in QueueThread)
                {
                    if (item.IsAlive)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public IList<string> GetErrors()
        {
            return _error;
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_thread != null)
                {
                    foreach (Thread q in QueueThread)
                    {
                        q.Join();
                        q.Abort();
                    }
                    _thread.Clear();
                    _thread = null;
                }
                if (_queue != null)
                {
                    _queue = null;
                }
                if (_error != null)
                {
                    _error = null;
                }
                _actuator = null;
            }
            GC.Collect();
        }
    }
}

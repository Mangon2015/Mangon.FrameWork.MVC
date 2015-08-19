using Mangon.FrameWork.Package.Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue
{
    public class ThreadPoolServer:IQueueServer<ITaskElement>,IDisposable
    {
        private IActuator<ITaskElement> _actuator = null;
        public IActuator<ITaskElement> Actuator
        {
            get { return _actuator;}
            set { _actuator = value; }
        }
        public void PutTask(ITaskElement item)
        {
            ThreadPool.QueueUserWorkItem(state => _actuator.Run(item), null);
        }

        public void Start()
        {
           
        }

        public void Stop()
        {
            
        }

        public int Count
        {
            get { return -1; }
        }

        public bool IsAlive
        {
            get { return true; }
        }

        public IList<string> GetErrors()
        {
            return null;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}

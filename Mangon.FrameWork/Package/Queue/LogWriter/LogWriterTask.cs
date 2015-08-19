using Mangon.FrameWork.Package.Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue.LogWriter
{
    public class LogWriterTask : ITaskElement
    {
        public string Path { get; set; }
        public string Message { get; set; }
        private int count = 0;

        public int TryCount
        {
            get { return 5; }
        }

        public int TrySleep
        {
            get { return 1000; }
        }

        public bool Try()
        {
            count++;
            //如果超过尝试次数，拒绝尝试
            if (count>TryCount)
            {
                return false;
            }
            return true;
        }
    }
}

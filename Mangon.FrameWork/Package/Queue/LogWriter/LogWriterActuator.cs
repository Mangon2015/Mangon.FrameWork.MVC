using Mangon.FrameWork.Package.Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Queue.LogWriter
{
    public class LogWriterActuator : IActuator<LogWriterTask>
    {
        public KeyValuePair<bool, LogWriterTask> Run(LogWriterTask task)
        {
            try
            {
                System.IO.File.AppendAllText(task.Path, string.Format("===={0}====\n{1}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), task.Message));
                return new KeyValuePair<bool, LogWriterTask>(true, null);
            }
            catch
            {

                return new KeyValuePair<bool, LogWriterTask>(false, task);
            }
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.ScheduledTasks
{
    /// <summary>
    /// 计划任务服务
    /// </summary>
   public class ScheduledService
    {
       /// <summary>
        /// 计划包
       /// </summary>
       private ConcurrentDictionary<string, TaskEvent> events = new ConcurrentDictionary<string, TaskEvent>();
       /// <summary>
       /// 执行器包
       /// </summary>
       private ConcurrentDictionary<string, Func<TaskEvent, KeyValuePair<bool, TaskEvent>>> Actuators = new ConcurrentDictionary<string, Func<TaskEvent, KeyValuePair<bool, TaskEvent>>>();

       private Thread mainThread = null;
       private static object l = new object();
       private static ScheduledService self = null;
       private ScheduledService()
       {
           mainThread = new Thread(Run);
       }
       /// <summary>
       /// 单例模式
       /// </summary>
       /// <returns></returns>
       public static ScheduledService GetInstance()
       {
           if (self==null)
           {
               lock (l)
               {
                   self = new ScheduledService();
               }
           }
           return self;
       }
       /// <summary>
       /// 持久化的路径
       /// </summary>
       public string XmlPath { get; set; }
       /// <summary>
       /// 重启/启动计划任务服务
       /// </summary>
       public void Start()
       {
           if (!mainThread.IsAlive)
           {
               mainThread.Start();
           }
       }
       /// <summary>
       /// 暂停计划任务服务
       /// </summary>
       public void Stop()
       {

           if (mainThread.IsAlive)
           {
               mainThread.Join();
               mainThread.Abort();
           }
       }
       /// <summary>
       /// 计划任务数量
       /// </summary>
       public int Count
       {
           get { return events.Count; }
       }
       /// <summary>
       /// 计时线程的状态
       /// </summary>
       public bool IsAlive
       {
           get { return mainThread.IsAlive; }
       }

       private void ScheduledRun(object call)
       {
           TaskEvent te = events[call.ToString()].Clone() as TaskEvent;
           KeyValuePair<bool, TaskEvent> result = new KeyValuePair<bool, TaskEvent>(false, te);
           try
           {
               result = Actuators[te.TaskActuator](te);
           }
           catch
           {

           }
           finally
           {

               events.TryRemove(call.ToString(), out te);
               events.TryAdd(call.ToString(), result.Value);
           }
       }

       /// <summary>
       /// 执行的异常
       /// </summary>
       /// <returns></returns>
       public IList<string> getErrors()
       {
           return null;
       }

       private void Run()
       {
           Stopwatch sw = new Stopwatch();
           while (true)
           {
               sw.Restart();
               foreach (var i in events)
               {

               }
               sw.Stop();
               if (sw.ElapsedMilliseconds < 1000)
               {
                   Thread.Sleep(1000 - (int)sw.ElapsedMilliseconds);
               }
           }
       }
    }
}

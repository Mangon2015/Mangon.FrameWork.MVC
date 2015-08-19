using Mangon.FrameWork.Package.Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.ScheduledTasks
{
    /// <summary>
    /// 一个计划任务的配置
    /// </summary>
    [Serializable]
   public class TaskEvent:Config.Config,ITaskElement,ICloneable
    {
        public DateTime NextTime { get; set; }
        public bool Waiting { get; set; }
        /// <summary>
        /// 执行器的名称
        /// </summary>
        public string TaskActuator { get; set; }
        /// <summary>
        /// 执行类型
        /// </summary>
        public TaskType type { get; set; }
        /// <summary>
        /// 间隔
        /// </summary>
        public TimeSpan time { get; set; }
        /// <summary>
        /// 如果是以一次性任务的话 年需要设置,默认当年
        /// </summary>
        public int SingleYear { get; set; }

        public int TryCount { get { return 1; } }


        public int TrySleep
        {
            get { return 0; }
        }

        public bool Try()
        {
            switch (type)
            {
                case TaskType.EveryDay:
                    return Hour();
                case TaskType.EveryWeek:
                    return Week();
                case TaskType.EveryHour:
                    return Minute();
                case TaskType.Single:
                    return Day();
                case TaskType.EveryMonth:
                    return Month();
                default:
                    return false;
            }
        }
        /// <summary>
        /// 秒的测试 误差为2秒
        /// </summary>
        /// <returns></returns>
        private bool Second()
        {
            if ((DateTime.Now.Second - time.Seconds) > 0 && (DateTime.Now.Second - time.Seconds) < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 分钟测试
        /// </summary>
        /// <returns></returns>
        private bool Minute()
        {
            if (DateTime.Now.Minute != time.Minutes)
            {
                return false;
            }
            return Second();
        }

        /// <summary>
        /// 小时测试
        /// </summary>
        /// <returns></returns>
        private bool Hour()
        {
            if (DateTime.Now.Hour != time.Hours)
            {
                return false;
            }
            return Minute();
        }

        /// <summary>
        /// 日测试
        /// </summary>
        /// <returns></returns>
        private bool Day()
        {

            if (DateTime.Now.Year == SingleYear && DateTime.Now.DayOfYear != time.Days)
            {
                return false;
            }
            return Hour();
        }
        /// <summary>
        /// 周测试
        /// </summary>
        /// <returns></returns>
        private bool Week()
        {
            if ((int)DateTime.Now.DayOfWeek != time.Days)
            {
                return false;
            }
            return Hour();

        }

        /// <summary>
        /// 月测试
        /// </summary>
        /// <returns></returns>
        private bool Month()
        {
            if ((int)DateTime.Now.Day != time.Days)
            {
                return false;
            }
            return Hour();
        }

        public object Clone()
        {
            var xml = WriteToXml();
            return ReadFromString<TaskEvent>(xml);
        }
    }


    /// <summary>
    /// 调度计划执行器
    /// </summary>
    public abstract class TaskActuator : ITaskActuator
    {
        public abstract string Name { get; }
        public abstract KeyValuePair<bool, TaskEvent> Run(TaskEvent TaksEvent);
    }
    /// <summary>
    /// 调度计划执行器接口
    /// </summary>
    public interface ITaskActuator
    {
        string Name { get; }
        KeyValuePair<bool, TaskEvent> Run(TaskEvent TaksEvent);
    }
}

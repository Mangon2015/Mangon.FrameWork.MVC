using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.ScheduledTasks
{
    [Serializable]
    public enum TaskType
    {
        /// <summary>
        /// 每天
        /// </summary>
        EveryDay,
        /// <summary>
        /// 每周,当类型为此条时,time的日 定义为 每周的第几天
        /// </summary>
        EveryWeek,
        /// <summary>
        /// 每小时
        /// </summary>
        EveryHour,
        /// <summary>
        /// 单次
        /// </summary>
        Single,
        /// <summary>
        /// 每个月,当类型为此条时,time的日 定义为 每月的第几天
        /// </summary>
        EveryMonth,
    }
}

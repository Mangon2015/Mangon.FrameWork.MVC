using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 时间扩展
    /// </summary>
   public static class Mangon_FrameWorkSystemExtension_DateTime
    {

        /// <summary>
        /// 转换到UNIX时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static String ToUnixTimeStamp(this System.DateTime dt)
        {
            System.DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            System.DateTime dtNow = System.DateTime.Parse(dt.ToString());
            System.TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        /// <summary>
        /// 由UNIX时间戳生成
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime FromTimestamp(this DateTime dt, string s)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(s + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            dt = dtStart.Add(toNow);
            return dt;
        }

        /// <summary>
        /// 计算过去时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ComputingPastTime(this DateTime dt)
        {
            TimeSpan ts = new TimeSpan(dt.Ticks);
            TimeSpan now = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan sp = now - ts;
            long minutes = 600000000;
            long hour = 36000000000;
            long day = 864000000000;
            long week = 6048000000000;
            if (sp.Ticks < minutes)//1分钟钟内
            {
                if (sp.Seconds < 0)
                {
                    return "0秒钟前";
                }
                else
                {
                    return sp.Seconds.ToString() + "秒钟前";
                }

            }
            else if (sp.Ticks < hour)//60分钟内
            {
                return sp.Minutes.ToString() + "分钟前";
            }
            else if (sp.Ticks < day)//24小时内
            {
                return sp.Hours.ToString() + "小时前";
            }
            else if (sp.Ticks < week)//七天以内
            {
                return sp.Days.ToString() + "天前";
            }
            else
            {
                return dt.ToString("yyyy-MM-dd");
            }
        }

        public static DateTime MinDataBaseValue(this DateTime dt)
        {
            return new DateTime(1900, 1, 1);
        }
    }
}

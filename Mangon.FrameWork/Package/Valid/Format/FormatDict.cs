using Mangon.FrameWork.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid.Format
{
    /// <summary>
    /// 储存各种格式化
    /// </summary>
    public class FormatDict
    {
        /// <summary>
        /// 格式化的委托字典
        /// </summary>
        private static Dictionary<string, Func<object, string[], Result<object>>> formats = null;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Func<object, string[], Result<object>>> Formats
        {
            get
            {
                if (formats == null)
                    formats = new Dictionary<string, Func<object, string[], Result<object>>>(20);
                return formats;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="DelegateFormat"></param>
        /// <returns></returns>
        protected bool RegFormat(string Name, Func<object, string[], Result<object>> DelegateFormat)
        {
            if (!HasFormat(Name))
            {
                Formats.Add(Name, DelegateFormat);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool HasFormat(string Name)
        {
            return Formats.ContainsKey(Name);
        }

    }
}

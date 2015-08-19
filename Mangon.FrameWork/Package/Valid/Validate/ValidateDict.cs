using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid.Validate
{
    /// <summary>
    /// 储存各种格式化
    /// </summary>
    public class ValidateDict
    {
        /// <summary>
        /// 格式化的委托字典
        /// </summary>
        private static Dictionary<string, Func<object, string[], bool>> validates = null;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Func<object, string[], bool>> Validates
        {
            get
            {
                if (validates == null)
                    validates = new Dictionary<string, Func<object, string[], bool>>(20);
                return validates;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="DelegateValidate"></param>
        /// <returns></returns>
        protected bool RegValidate(string Name, Func<object, string[], bool> DelegateValidate)
        {
            if (!HasValidate(Name))
            {
                Validates.Add(Name, DelegateValidate);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool HasValidate(string Name)
        {
            return Validates.ContainsKey(Name);
        }

    }
}

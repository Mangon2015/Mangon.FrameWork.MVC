using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid
{
    /// <summary>
    /// 元素
    /// </summary>
    public abstract class ValidateElement : IValidElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public abstract bool doValidate(object Value, string[] Params);
        /// <summary>
        /// 
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Type ResultType
        {
            get { return typeof(string); }
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual bool HasEmpty
        {
            get { return true; }
        }
    }
}

using Mangon.FrameWork.Result;
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
    public abstract class FormatElement : IValidElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public abstract Result<object> doFormat(object Value, string[] Params);
        /// <summary>
        /// 
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        public abstract Type ResultType
        {
            get;
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual bool HasEmpty
        {
            get { return false; }
        }
    }
}

using Mangon.FrameWork.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Repository
{
    /// <summary>
    /// 对象仓库
    /// </summary>
    public class GlobalObject:IPackage
    {
        private static Dictionary<string, IElement> repository = new Dictionary<string, IElement>();
        /// <summary>
        /// 注册一个全局对象
        /// </summary>
        /// <param name="key">名称</param>
        /// <param name="Object">对象</param>
        public static void RegObject(string key, IElement Object)
        {
            if (repository.ContainsKey(key))
            {
                repository[key] = Object;
            }
            else
            {
                repository.Add(key, Object);
            }
        }
        /// <summary>
        /// 获取一个全局对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IElement GetObject(string key)
        {
            if (repository.ContainsKey(key))
            {
                return repository[key];
            }
            return null;
        }
    }
}

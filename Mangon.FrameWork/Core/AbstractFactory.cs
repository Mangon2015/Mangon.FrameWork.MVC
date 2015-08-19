using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Core
{
    /// <summary>
    /// 静态泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public abstract  class AbstractFactory<T> where T:AbstractFactory<T>,new()
    {
       /// <summary>
       /// 获取自己
       /// </summary>
       /// <returns></returns>
       public static T GetInstance()
       {
           return new T();
       }
    }
}

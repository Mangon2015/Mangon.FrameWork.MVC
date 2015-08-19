using Mangon.FrameWork.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Storage
{
    /// <summary>
    /// 存储接口
    /// </summary>
   public  interface IStorage
    {
       /// <summary>
       /// 读取数据
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
       Result<object> Get(string name);
       /// <summary>
       /// 保存数据
       /// </summary>
       /// <param name="name"></param>
       /// <param name="data"></param>
       /// <returns></returns>
       bool Set(string name, object data);
       /// <summary>
       /// 杀掉当前的命名控件
       /// </summary>
       /// <returns></returns>
       bool Kill();
    }
}

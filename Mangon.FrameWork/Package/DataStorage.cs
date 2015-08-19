using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package
{
   public  class DataStorage:Mangon.FrameWork.InterFace.IPackage
    {
       /// <summary>
       /// 支持的数据类型
       /// </summary>
       public enum Storeage
       { 
           /// <summary>
           /// 文件
           /// </summary>
        File,
           /// <summary>
           /// web适用的Session
           /// </summary>
           Session,
           /// <summary>
           /// web适用的cache
           /// </summary>
           Cache,
           /// <summary>
           /// web 适用的cookie
           /// </summary>
           Cookie
       }

       
    }
}

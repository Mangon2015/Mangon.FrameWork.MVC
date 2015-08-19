using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    /// <summary>
    /// 数据库基本类
    /// </summary>
   public interface IDataBase:IDisposable,IObjectContextAdapter
    {
       /// <summary>
       /// 数据库连接
       /// </summary>
       System.Data.Common.DbConnection Connection { get; }
       /// <summary>
       /// 打开事物
       /// </summary>
       /// <param name="solationLevel">隔离级别</param>
       /// <returns>一个事物</returns>
       System.Data.Common.DbTransaction BeginTransaction(IsolationLevel solationLevel);
       System.Data.Common.DbTransaction BeginTransaction();
    }
}

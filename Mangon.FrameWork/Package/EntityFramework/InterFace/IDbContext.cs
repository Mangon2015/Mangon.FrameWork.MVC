using Mangon.FrameWork.Package.EntityFramework.Element;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    /// <summary>
    /// 数据库扩展对象
    /// </summary>
   public interface IDbContext
    {
       EntitySetBase GetSet(string entityName);
       EntityProperties[] GetProperties(string entityName);
       EntityProperties[] GetKeyProperties(string entityName);
       EntityKey[] GetKeys(string entityName);
       IEnumerable FetchSQL(string sql, params object[] paramenters);
       IEnumerable<ResultType> FetchSQL<ResultType>(string sql, params object[] paramenters);
       int ExecutionSQL(string sql, params object[] parameters);
       IEnumerable<dynamic> GetValidationErrors();
    }
}

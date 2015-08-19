using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    public enum ListType
    { 
        BlockList,WhiteList
    }
   public interface IManage<TDataModel>
    {
       bool IsAutoSave { get; }
       int SaveChange();
       bool Updata(object key, IDictionary<string, object> values);
       bool Updata(TDataModel entity);
       bool Updata(TDataModel[] entytis);
       bool Updata(TDataModel entity, ListType listType, IEnumerable<string> selectList);
       bool Updata(TDataModel[] entitys, ListType listType, IEnumerable<string> selectList);
       bool Updata(Expression<Func<TDataModel, bool>> where, IDictionary<string, object> values, out int count);
       bool Updata(Expression<Func<TDataModel, bool>> where, IDictionary<string, object> values);

    }
}

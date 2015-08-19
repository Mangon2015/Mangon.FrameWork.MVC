using Mangon.FrameWork.Package.EntityFramework.InterFace;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework
{
   public abstract class SearchModelServiceBase<TDataModel,TDb>:DataBaseContext<TDb>,ISearchInModelOutCount<TDataModel>,IDisposable where TDb:DataBaseBase,IDataBase,new ()  where TDataModel:class,new()
    {

       public SearchModelServiceBase() { }
       public SearchModelServiceBase(DbContext dbContext) : base(dbContext) { }

       protected List<dynamic> QtoList(IQueryable q)
       {
           List<dynamic> l = new List<dynamic>();
           foreach (var item in q)
           {
               l.Add(item);
           }
           return l;
       }



       public List<dynamic> dynamicSearch<OrderType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, System.Linq.Expressions.Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<dynamic> dynamicSearch<OrderType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, Dictionary<System.Linq.Expressions.Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<dynamic> dynamicSearch(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<dynamic> dynamicSearch(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<TDataModel> Search(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<TDataModel> Search(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<TDataModel> Search<OrderType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, Dictionary<System.Linq.Expressions.Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<TDataModel> Search<OrderType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, System.Linq.Expressions.Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<TDataModel> Search<OrderType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, System.Linq.Expressions.Expression<Func<TDataModel, OrderType>> order, int Take, int Skip, out int Count)
       {
           throw new NotImplementedException();
       }

       public List<OutType> Search<OutType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip, out int Count) where OutType : class, new()
       {
           throw new NotImplementedException();
       }

       public List<OutType> Search<OutType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, int Take, int Skip, out int Count) where OutType : class, new()
       {
           throw new NotImplementedException();
       }

       public List<OutType> Search<OrderType, OutType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, Dictionary<System.Linq.Expressions.Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip, out int Count) where OutType : class, new()
       {
           throw new NotImplementedException();
       }

       public List<OutType> Search<OrderType, OutType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, System.Linq.Expressions.Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip, out int Count) where OutType : class, new()
       {
           throw new NotImplementedException();
       }

       public List<OutType> Search<OrderType, OutType>(System.Linq.Expressions.Expression<Func<TDataModel, bool>> where, System.Linq.Expressions.Expression<Func<TDataModel, OrderType>> order, int Take, int Skip, out int Count) where OutType : class, new()
       {
           throw new NotImplementedException();
       }
    }
}

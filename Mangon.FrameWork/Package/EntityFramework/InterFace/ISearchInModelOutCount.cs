using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    public interface ISearchInModelOutCount<TDataModel>
    {


        List<dynamic> dynamicSearch<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip, out int Count);
        List<dynamic> dynamicSearch<OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip, out int Count);
        List<dynamic> dynamicSearch(Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip, out int Count);
        List<dynamic> dynamicSearch(Expression<Func<TDataModel, bool>> where, int Take, int Skip, out int Count);
        List<TDataModel> Search(Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip, out int Count);
        List<TDataModel> Search(Expression<Func<TDataModel, bool>> where, int Take, int Skip, out int Count);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip, out int Count);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip, out int Count);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, int Take, int Skip, out int Count);
        List<OutType> Search<OutType>(Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip, out int Count) where OutType : class ,new();
        List<OutType> Search<OutType>(Expression<Func<TDataModel, bool>> where, int Take, int Skip, out int Count) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip, out int Count) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip, out int Count) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, int Take, int Skip, out int Count) where OutType : class ,new();

    }
}

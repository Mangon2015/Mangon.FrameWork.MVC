using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    public interface ISearchInModel<TDataModel>
    {
        TDataModel[] FindByKeys(object[] keys);
        TDataModel FindByKey(object key);
        int Count(Expression<Func<TDataModel, bool>> where);
        List<dynamic> DynamicSearch<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int take, int skip);
        List<dynamic> DynamicSearch<OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> order, int take, int skip);
        List<dynamic> DynamicSearch(Expression<Func<TDataModel, bool>> where, string order, int take, int skip);
        List<dynamic> DynamicSearch(Expression<Func<TDataModel, bool>> where, int take, int skip);
        List<TDataModel> Search(Expression<Func<TDataModel, bool>> where, string order, int take, int skip);
        List<TDataModel> Search(Expression<Func<TDataModel, bool>> where, int take, int skip);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, int Take, int Skip);
        List<OutType> Search<OutType>(Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip) where OutType : class ,new();
        List<OutType> Search<OutType>(Expression<Func<TDataModel, bool>> where, int Take, int Skip) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, int Take, int Skip) where OutType : class ,new();

    }
}

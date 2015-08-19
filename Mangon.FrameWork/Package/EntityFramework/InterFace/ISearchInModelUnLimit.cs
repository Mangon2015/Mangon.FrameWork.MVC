using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    public interface ISearchInModelUnLimit<TDataModel>
    {
        List<dynamic> dynamicSearch<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort);
        List<dynamic> dynamicSearch<OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders);
        List<dynamic> dynamicSearch(Expression<Func<TDataModel, bool>> where, string order);
        List<dynamic> dynamicSearch(Expression<Func<TDataModel, bool>> where);
        List<TDataModel> Search(Expression<Func<TDataModel, bool>> where, string order);
        List<TDataModel> Search(Expression<Func<TDataModel, bool>> where);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort);
        List<TDataModel> Search<OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order);
        List<OutType> Search<OutType>(Expression<Func<TDataModel, bool>> where, string order) where OutType : class ,new();
        List<OutType> Search<OutType>(Expression<Func<TDataModel, bool>> where) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort) where OutType : class ,new();
        List<OutType> Search<OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order) where OutType : class ,new();

    }
}

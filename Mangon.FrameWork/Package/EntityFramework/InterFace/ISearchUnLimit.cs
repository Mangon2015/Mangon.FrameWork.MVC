using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
    /// <summary>
    /// 不定对象查询
    /// </summary>
    public interface ISearchUnLimit
    {
        List<dynamic> Search(string where, string order, string select);
        List<dynamic> Search(string where, string order);
        List<dynamic> Search(string where);
        List<dynamic> dynamicSearch<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders) where TDataModel : class;
        List<dynamic> dynamicSearch<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort) where TDataModel : class;
        List<dynamic> dynamicSearch<TDataModel>(Expression<Func<TDataModel, bool>> where, string order) where TDataModel : class;
        List<dynamic> dynamicSearch<TDataModel>(Expression<Func<TDataModel, bool>> where) where TDataModel : class;

        List<OutType> Search<OutType>(string where, string order) where OutType : class,new();
        List<OutType> Search<OutType>(string where) where OutType : class ,new();

        List<OutType> Search<TDataModel, OutType>(Expression<Func<TDataModel, bool>> where, string order)
            where OutType : class ,new()
            where TDataModel : class;
        List<OutType> Search<TDataModel, OutType>(Expression<Func<TDataModel, bool>> where)
            where TDataModel : class
            where OutType : class ,new();
        List<OutType> Search<TDataModel, OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order)
            where OutType : class ,new()
            where TDataModel : class;
        List<OutType> Search<TDataModel, OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders)
            where OutType : class ,new()
            where TDataModel : class;
        List<TDataModel> Search<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort) where TDataModel : class;
        List<TDataModel> Search<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders) where TDataModel : class;
        List<TDataModel> Search<TDataModel>(Expression<Func<TDataModel, bool>> where) where TDataModel : class;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework.InterFace
{
   public   interface ISearch
    {
       IQueryable SearchQueryTable { get; }
       int Count(string where);
       int Count<TDataModel>(Expression<Func<TDataModel, bool>> where) where TDataModel : class;
       List<dynamic> Search(string where, string order, string select, int take, int skip);
       List<dynamic> Search(string where, string order, int take, int skip);
       List<dynamic> Search(string where, int take, int skip);
       List<dynamic> DynamicSearch<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> order, int take, int skip) where TDataModel : class;
       List<dynamic> DynamisSearch<TDataModel>(Expression<Func<TDataModel, bool>> where,string order, int take, int skip) where TDataModel : class;
       List<dynamic> DynamisSearch<TDataModel>(Expression<Func<TDataModel, bool>> where, int take, int skip) where TDataModel : class;
       List<OutType> Search<OutType>(string where, string order, int take, int skip) where OutType : class,new();
       List<OutType> Search<OutType>(string where, int take, int skip) where OutType : class,new();
       List<OutType> Search<TDataModel,OutType>(Expression<Func<TDataModel,bool>> where,string order,int take,int skip) where OutType:class,new()
                                                                                                                        where TDataModel:class;
       List<OutType> Search<TDataModel, OutType>(Expression<Func<TDataModel, bool>> where, int take, int skip)
           where OutType : class,new()
           where TDataModel : class;
       List<OutType> Search<TDataModel, OrderType, OutType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> order, int take, int skip)
           where OutType : class ,new()
           where TDataModel : class;
       List<TDataModel> Search<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Expression<Func<TDataModel, OrderType>> order, Sort sort, int Take, int Skip) where TDataModel : class;
       List<TDataModel> Search<TDataModel, OrderType>(Expression<Func<TDataModel, bool>> where, Dictionary<Expression<Func<TDataModel, OrderType>>, Sort> orders, int Take, int Skip) where TDataModel : class;

       List<TDataModel> Search<TDataModel>(Expression<Func<TDataModel, bool>> where, int Take, int Skip) where TDataModel : class;
       List<TDataModel> Search<TDataModel>(Expression<Func<TDataModel, bool>> where, string order, int Take, int Skip) where TDataModel : class;


    }
}

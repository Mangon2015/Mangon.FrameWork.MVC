using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mangon.FrameWork.Package.LinqExtend;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace Mangon.FrameWork.Package.LinqExtend
{
     
    public class PredicateSet<T> where T:class
    {
        private Expression<Func<T, bool>> _where;
        /// <summary>
        /// 查询条件
        /// </summary>
        public Expression<Func<T, bool>> Where
        {
            set { _where = value; }
            get
            {
                if (_where==null)
                {
                    _where = p => 1 == 1;
                }
                return _where;
            }
        }

        private List<PredicateOrder<T>> _orderDict;

        private int _take = 20;
        public int Take { get { return _take; } set { _take = value; } }
        public int Skip { get; set; }

        public PredicateSet() { }
        public PredicateSet(Expression<Func<T, bool>> where, int skip, int take)
        {
            this.Where = where;
            this.Take = take;
            this.Skip = skip;
        }
        public static PredicateSet<T> Instance<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> orderField, string sort, int skip, int take)
        {
            var pSet = new PredicateSet<T>();
            pSet.Where = where;
            pSet.SetOrder<TType>(orderField, sort);
            pSet.Skip = skip;
            pSet.Take = take;
            return pSet;
        }

        public static PredicateSet<T> Instance<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> orderField, OrderType sortMode, int skip, int take)
        {
            var pSet = new PredicateSet<T>();
            pSet.Where = where;
            pSet.SetOrder<TType>(orderField, sortMode);
            pSet.Skip = skip;
            pSet.Take = take;
            return pSet;
        }


        public static PredicateSet<T> Instance(Expression<Func<T, bool>> where)
        {
            var pSet = new PredicateSet<T>();
            pSet.Where = where;
            return pSet;
        }

        public static PredicateSet<T> Instance()
        {
            return new PredicateSet<T>();
        }
        public static PredicateSet<T> Instance(Expression<Func<T, bool>> where, string orderField, string sort, int skip, int take)
        {
            var pSet = new PredicateSet<T>();
            pSet.Where = where;
            pSet.SetOrder(orderField, sort);
            pSet.Skip = skip;
            pSet.Take = take;
            return pSet;
        }

        public static PredicateSet<T> Instance(Expression<Func<T, bool>> where, string orderField, OrderType Sortmode, int skip, int take)
        {
            var pSet = new PredicateSet<T>();
            pSet.Where = where;
            pSet.SetOrder(orderField, Sortmode.ToString());
            pSet.Skip = skip;
            pSet.Take = take;
            return pSet;
        }
        /// <summary>
        /// 添加排序
        /// </summary>
        /// <param name="p"></param>
        public void SetOrders(PredicateOrder<T> p)
        {
            if (_orderDict==null)
            {
                _orderDict = new List<PredicateOrder<T>>();
            }
            _orderDict.Add(p);
        }

        public void SetOrder<TType>(Expression<Func<T, TType>> orderField, OrderType sortMode)
        {
            this.SetOrders(new PredicateOrder<T, TType>(orderField, sortMode));
        }

        public void SetOrder<TType>(Expression<Func<T, TType>> orderField, string sort)
        {
            OrderType sortmode;
            if (sort.ToLower().Trim() == "desc")
            {
                sortmode = OrderType.DESC;
            }
            else
            {
                sortmode = OrderType.ASC;
            }
            this.SetOrders(new PredicateOrder<T, TType>(orderField, sortmode));
        }
        public void SetOrder(string Field, string Sort)
        {
            PredicateOrder<T> po;
            if (Sort.ToLower().Trim() == "desc")
            {
                po = new PredicateOrder<T>(Field, OrderType.DESC);
            }
            else
            {
                po = new PredicateOrder<T>(Field, OrderType.ASC);
            }
            this.SetOrders(po);
        }
        /// <summary>
        /// 附加表达式
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable(IQueryable<T> q)
        {
            string order = "";
            if (_orderDict!=null)
            {
                foreach (var item in _orderDict)
                {
                    order += string.Format(" {0} {1}", item.Field, item.panModel.ToString());
                }
            }
            if (order.Length>3)
            {
                order = order.Substring(0, order.Length - 1);
                q.OrderBy(order);
            }
            if (Skip>0)
            {
                q.Skip(Skip);
            }
            if (Take>0)
            {
                q.Take(Take);
            }
            return q;
        }
        /// <summary>
        /// 建立表达式
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable(DbSet<T> set)
        {
            IQueryable<T> q;
            if (_where!=null)
            {
                q = set.Where(_where);
            }
            else
            {
                q = set;
            }
            return GetQueryable(q);
        }
    }
}

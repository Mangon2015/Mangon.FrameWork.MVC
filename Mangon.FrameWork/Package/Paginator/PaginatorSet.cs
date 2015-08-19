using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Paginator
{
    public class PaginatorSet : Paginator
    {
        /// <summary>
        /// 
        /// </summary>
        public PaginatorSet() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Total"></param>
        /// <param name="Current"></param>
        /// <param name="PageSize"></param>
        public PaginatorSet(string Url, int Total, int Current, int PageSize = 20)
        {
            Set(Url, Total, Current, PageSize);
        }
        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="Url">地址</param>
        /// <param name="ps">样式表</param>
        /// <param name="Total">总数</param>
        /// <param name="Current">当前页</param>
        /// <param name="PageSize">分页大小</param>
        /// <returns>结果的分页实体</returns>
        public Paginator Set(string Url, int Total, int Current, int PageSize = 20)
        {
            if (Url.IndexOf("{0}") < 0) throw new ArgumentNullException("Url Need has a \"{0}\" to format!");
            if (Total <= 0) Total = 0;
            if (PageSize <= 0) PageSize = 20;

            if (Current <= 0) Current = 1;//以1为开始计数   

            this.BaseUrl = Url;
            this.Total = Total;
            this.CurrentIndex = Current;
            this.PageSize = PageSize;
            this.FirstIndex = 0;
            MathPages();
            SetPages();
            return this.Paginator();
        }

        public Paginator Set(string Url, int Total, int Current, int PageSize = 20, int beforeNum = 4, int AfterNum = 5)
        {
            if (Url.IndexOf("{0}") < 0) throw new ArgumentNullException("Url Need has a \"{0}\" to format!");
            if (Total <= 0) Total = 0;
            if (PageSize <= 0) PageSize = 20;

            if (Current <= 0) Current = 1;//以1为开始计数   

            this.BeforeNum = beforeNum;
            this.AfterNum = AfterNum;
            this.BaseUrl = Url;
            this.Total = Total;
            this.CurrentIndex = Current;
            this.PageSize = PageSize;
            this.FirstIndex = 0;
            MathPages();
            SetPages();
            return this.Paginator();
        }

        /// <summary>
        /// 重算
        /// </summary>
        /// <param name="pss"></param>
        /// <returns></returns>
        public Paginator Reset()
        {
            if (BaseUrl.IndexOf("{0}") < 0) throw new ArgumentNullException("Url Need has a \"{0}\" to format!");
            if (Total < 0) throw new ArgumentNullException("Total<0 ");
            MathPages();
            SetPages();
            return (Paginator)this;
        }

        /// <summary>
        /// 运算
        /// </summary>
        protected void MathPages()
        {
            //**
            // *原则 由1开始计数 
            // * 包头不包尾
            // *  如果100 页
            // *  每页10个 则其实计算是  起始位 +9 得到结束位
            // *  则
            // *  p1:1~10  p2:11~20 p3 :21~30 ~~~  p100:91~100
            // */



            //计算总分页
            this.Count = (int)Math.Ceiling((float)Total / (float)PageSize);
            //最后停止位
            this.LastIndex = Count;

            #region 校验当前页以及上下页位移
            //只有一页面
            if (this.Count == 1)
            {
                this.CurrentIndex = 1;//赋予最后一页
                this.NextIndex = CurrentIndex;
                this.PrvIndex = CurrentIndex;
            }
            else//多余1页
            {
                if (this.CurrentIndex >= this.Count)//超限
                {
                    this.CurrentIndex = Count;//赋予最后一页
                    this.NextIndex = CurrentIndex;
                    this.PrvIndex = Count - 1;
                }
                else if (CurrentIndex <= 1)//以1为起始
                {
                    this.CurrentIndex = 1;
                    this.NextIndex = CurrentIndex + 1;
                    this.PrvIndex = CurrentIndex;
                }
                else//不会大于计数 不会小于起始
                {
                    this.NextIndex = CurrentIndex + 1;
                    this.PrvIndex = CurrentIndex - 1;
                }
            }
            #endregion

            #region 显示区域

            if (Count > ViewPanel)//不能全部显示
            {
                //如果当前页在第一版
                if (CurrentIndex <= BeforeNum)//最少
                {
                    ViewStartIndex = 1;
                    ViewEndIndex = ViewPanel;
                }
                else if (CurrentIndex >= Count - AfterNum)//最大
                {
                    ViewStartIndex = Count - ViewPanel + 1;
                    ViewEndIndex = Count;
                }
                else
                {
                    ViewStartIndex = CurrentIndex - BeforeNum;
                    ViewEndIndex = CurrentIndex + AfterNum;
                }
            }
            else if (ViewPanel >= Count)//能全部显示
            {
                ViewStartIndex = 1;
                ViewEndIndex = Count;

            }

            #endregion


        }

        /// <summary>
        /// 生成分页列表
        /// </summary>
        protected void SetPages()
        {
            this.List = new List<Page>();
            for (int i = ViewStartIndex; i <= ViewEndIndex; i++)
            {

                //   var start = i * PageSize - PageSize;
                //  var end = (PageSize * i) > Total ? Total : (PageSize * i);
                //   var pageset = PageSet.getPage(start, end, i, BaseUrl);
                var pageset = PageSet.getPage(i, PageSize, Total, BaseUrl);
                List.Add(pageset);
                if (i == CurrentIndex)
                {
                    this.Current = pageset;
                }

            }
            this.First = PageSet.getPage(FirstIndex, PageSize, Total, BaseUrl);// String.Format(Style.First, First);
            this.Last = PageSet.getPage(LastIndex, PageSize, Total, BaseUrl);// String.Format(Style.Last, Last);
            this.Next = PageSet.getPage(NextIndex, PageSize, Total, BaseUrl);// String.Format(Style.Next, Next);
            this.Prv = PageSet.getPage(PrvIndex, PageSize, Total, BaseUrl); //String.Format(Style.Prv, Prv);


        }

        /// <summary>
        /// 取得分页实体
        /// </summary>
        /// <returns></returns>
        public Paginator Paginator()
        {
            return this;
        }
    }
}

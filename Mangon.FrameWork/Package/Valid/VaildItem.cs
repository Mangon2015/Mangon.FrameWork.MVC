using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid
{
    /// <summary>
    /// 校验元件
    /// </summary>
    public class VaildItem
    {
        /// <summary>
        /// 名
        /// </summary>
        public string Name;
        /// <summary>
        /// 参数
        /// </summary>
        public string[] Param;
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        public VaildItem(string Name, string[] Param)
        {
            this.Name = Name;
            this.Param = Param;
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        public VaildItem() { }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        public VaildItem(string Name)
        {
            this.Name = Name;
            this.Param = new string[0];
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        public VaildItem(string Name, string Param)
        {
            this.Name = Name;
            this.Param = new string[1] { Param };
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        public VaildItem(Enum Name, string[] Param)
        {
            this.Name = Name.ToString();
            this.Param = Param;
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        public VaildItem(Enum Name)
        {
            this.Name = Name.ToString();
            this.Param = new string[0];
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        public VaildItem(Enum Name, string Param)
        {
            this.Name = Name.ToString();
            this.Param = new string[1] { Param };
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static VaildItem Get(string Name, string[] Param)
        {
            return new VaildItem(Name, Param);
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static VaildItem Get(string Name)
        {
            return new VaildItem(Name);
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static VaildItem Get(Enum Name, string[] Param)
        {
            return new VaildItem(Name, Param);
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static VaildItem Get(Enum Name)
        {
            return new VaildItem(Name);
        }
        /// <summary>
        /// 一个校验元素
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static VaildItem Get(Enum Name, string Param)
        {
            return new VaildItem(Name, Param);
        }
    }
}

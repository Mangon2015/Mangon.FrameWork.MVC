using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Result
{
    [DataContract]
    [Serializable]
    public class AJaxResult : Result
    {
        private string returnUrl = "";
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string ReturnUrl
        {
            get
            {
                return returnUrl;
            }
            set
            {
                returnUrl = value;
            }
        }
    }
}

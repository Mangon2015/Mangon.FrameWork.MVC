using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Result
{
    /// <summary>
    /// 非泛型返回对象
    /// </summary>
    [DataContract]
    [Serializable]
    public class Result : IResult, IResultError
    {
        /// <summary>
        /// 值
        /// </summary>
        protected object _data;
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public virtual object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        protected Boolean _bool;
        /// <summary>
        /// 标志
        /// </summary>

        [DataMember]
        public bool Bool
        {
            get
            {
                return _bool;
            }
            set
            {
                _bool = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected Exception _error;
        /// <summary>
        /// 异常
        /// </summary>
        [IgnoreDataMember]

        public Exception Error
        {
            get
            {
                if (_error == null && _ErrorMessage != null)
                {
                    _error = new Exception(_ErrorMessage);
                }
                return _error;
            }
            set
            {
                _error = value;
            }
        }
        private string _ErrorMessage = null;
        /// <summary>
        /// 异常文字
        /// </summary>
        [DataMember]
        public string ErrorMessage
        {
            get
            {
                if (_ErrorMessage != null)
                {
                    return _ErrorMessage;
                }

                if (_error == null) return string.Empty;
                return _error.ToString();
            }
            set
            {
                _ErrorMessage = value;

            }
        }



        /// <summary>
        /// 简化异常
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result getResult(bool Bool, String errorMessage)
        {
            Result result = new Result();
            result.Bool = Bool;
            result.Error = new Exception(errorMessage);
            return result;
        }
        /// <summary>
        /// 只有是否成功
        /// </summary>
        /// <param name="Bool"></param>
        /// <returns></returns>
        public static Result GetResult(bool success)
        {
            return GetResult(success, "");
        }
        /// <summary>
        /// 简化异常
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Exception"></param>
        /// <returns></returns>
        public static Result GetResult(bool success, Exception error)
        {
            return GetResult(success, null, error);
        }

        public static Result GetResult(bool success, string errorMessage)
        {
            return GetResult(success, null, errorMessage);
        }

        /// <summary>
        /// 无异常
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static Result GetResult(bool success, Object data)
        {
           return GetResult(success, data, null,null);
        }
        /// <summary>
        /// 全状态
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Data"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static Result GetResult(bool success, Object data, Exception error)
        {
            return GetResult(success, data, null, error);
        }
        /// <summary>
        /// 全状态
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Data"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public static Result GetResult(bool success, Object data, string errorMessage)
        {
            return GetResult(success, data, errorMessage, null);
        }

        public static Result GetResult(bool success, Object data, string errorMessage, Exception error)
        {
            Result result = new Result()
            {
                Bool = success,
                Data = data,
                Error = error,
                ErrorMessage = errorMessage
            };
            return result;
        }

        public static Result True()
        {
          return  GetResult(true);
        }
        public static Result True(Object data)
        {
            return GetResult(true,data);
        }
        public static Result False(Exception e)
        {
            return GetResult(false, e);
        }
        public static Result False(string errorMessage)
        {
            return GetResult(false, errorMessage);
        }
        public static Result False(object data, string errorMessage)
        {
            return GetResult(false, data, errorMessage);
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadyOnly
        {
            get { return false; }
        }
    }
}

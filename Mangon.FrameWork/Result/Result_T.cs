using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Result
{
    /// <summary>
    /// 标准泛型返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    [Serializable]
    public class Result<T> : IResult<T>
    {
        public static Result<T> True(T data, string errorMessage)
        {
            return GetInstance(true, data, errorMessage);
        }
        public static Result<T> True(T data)
        {
            return GetInstance(true, data, null, null);
        }
        public static Result<T> False(Exception e)
        {
            return False(default(T), e);
        }
        public static Result<T> False(string error)
        {
            return False(default(T), error);
        }
        public static Result<T> False(T data, string error)
        {
            return GetInstance(false, data, error, null);
        }
        public static Result<T> False(T data, Exception error)
        {
            return GetInstance(false, data, null, error);
        }
        public static Result<T> False(T data, string error, Exception e)
        {
            return GetInstance(false, data, error, e);
        }
        /// <summary>
        /// 重写获取对象 
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Data"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static Result<T> GetInstance(Boolean success, T data, Exception error)
        {
            return GetInstance(success, data, null, error);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Data"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public static Result<T> GetInstance(Boolean success, T data, string errorMessage)
        {
            return GetInstance(success, data, errorMessage, null);
        }

        public static Result<T> GetInstance(Boolean success, T data, string errorMessage,Exception error)
        {
            var rs = new Result<T>();
            rs.Bool = success;
            rs.Data = data;
            rs.ErrorMessage = errorMessage;
            rs.Error = error;
            return rs;
        }

        /// <summary>
        /// 静态返回
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Datas"></param>
        /// <param name="Errors"></param>
        /// <returns></returns>
        public static Result<T> GetResult(bool Bool, T Datas, string Errors)
        {
            return Result<T>.GetInstance(Bool, Datas, Errors);
        }
        /// <summary>
        /// 简写只成功与失败
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        public static Result<T> GetResult(bool Bool, T Datas)
        {
            return Result<T>.GetInstance(Bool, Datas, string.Empty);
        }

        /// <summary>
        /// 简写异常
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result<T> GetResult(bool Bool, String errorMessage)
        {
            return Result<T>.GetInstance(Bool, default(T), errorMessage);
        }
        /// <summary>
        /// 缺写数据
        /// </summary>
        /// <param name="Bool"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Result<T> GetResult(bool Bool, Exception e)
        {

            return Result<T>.GetInstance(Bool, default(T), e);
        }


        /// <summary>
        /// 简写是否
        /// </summary>
        /// <param name="Bool"></param>
        /// <returns></returns>
        public static Result<T> GetResult(bool Bool)
        {

            return Result<T>.GetInstance(Bool, default(T), string.Empty);
        }
        /// <summary>
        /// 简写失败
        /// </summary>
        /// <param name="Errors"></param>
        /// <returns></returns>
        public static Result<T> GetResult(string Errors)
        {
            return Result<T>.GetInstance(false, default(T), Errors);
        }
        /// <summary>
        /// 简写异常
        /// </summary>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static Result<T> GetResult(Exception Error)
        {
            return Result<T>.GetInstance(false, default(T), Error);
        }
        /// <summary>
        /// 
        /// </summary>

        protected Boolean _bool;
        /// <summary>
        /// 
        /// </summary>
        protected T _data;
        /// <summary>
        /// 
        /// </summary>
        protected Exception _error;
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
        /// 数据
        /// </summary>
        [DataMember]
        public T Data
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
        /// 异常
        /// </summary>\
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
        /// 异常信息
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
        /// 
        /// </summary>
        [IgnoreDataMember]
        public bool IsReadyOnly
        {
            get { return false; }
        }
    }
}

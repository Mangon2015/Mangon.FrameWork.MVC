using Mangon.FrameWork.Result;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid
{
    /// <summary>
    /// 规则校验器
    /// </summary>
    public class Valider : InterFace.IPackage
    {
        /// <summary>
        ///  校验规则字典
        /// </summary>
        public Dictionary<string, List<VaildItem>> Validate { get; private set; }
        /// <summary>
        /// 过滤规则字典
        /// </summary>
        public Dictionary<string, List<VaildItem>> Formater { get; private set; }
        /// <summary>
        /// 输入
        /// </summary>
        public Dictionary<string, object> Input = new Dictionary<string, object>();
        /// <summary>
        /// 输出
        /// </summary>
        public Dictionary<string, Result<object>> Output { get; private set; }
        /// <summary>
        /// 校验规则器
        /// </summary>
        private Validate.Validate v;
        /// <summary>
        /// 格式规则器
        /// </summary>
        private Format.Format f;
        /// <summary>
        /// 初始化
        /// </summary>
        public Valider()
        {

            v = new Validate.Validate();
            f = new Format.Format();
            Validate = new Dictionary<string, List<VaildItem>>();
            Formater = new Dictionary<string, List<VaildItem>>();
            Output = new Dictionary<string, Result<object>>();
        }
        /// <summary>
        /// 设置一个校验规则
        /// </summary>
        /// <param name="Input">需要校验的字段名</param>
        /// <param name="vi">规则</param>
        /// <returns></returns>
        public bool SetVaildate(string Input, VaildItem vi)
        {
            if (vi == null)
            {
                return true;
            }

            if (string.IsNullOrEmpty(vi.Name) || !v.HasValidate(vi.Name)) return false;
            if (Validate.ContainsKey(Input))
                Validate[Input].Add(vi);
            else
                Validate.Add(Input, new List<VaildItem>() { vi });
            return true;
        }
        /// <summary>
        /// 设置一个规则
        /// </summary>
        /// <param name="Input">输入表中的key</param>
        /// <param name="vaildate">校验规则</param>
        /// <param name="filter">过滤规则</param>
        /// <returns></returns>
        public bool Set(string Input, VaildItem vaildate, VaildItem filter)
        {
            if (SetFilter(Input, filter) && SetVaildate(Input, vaildate))
                return true;
            return false;
        }
        /// <summary>
        /// 设置一个过滤规则
        /// </summary>
        /// <param name="Input">需要校验的字段名</param>
        /// <param name="vi">规则</param>
        /// <returns></returns>
        public bool SetFilter(string Input, VaildItem vi)
        {
            if (vi == null)
            {
                return true;
            }

            if (string.IsNullOrEmpty(vi.Name) || !f.HasFormat(vi.Name)) return false;
            if (Formater.ContainsKey(Input))
                Formater[Input].Add(vi);
            else
                Formater.Add(Input, new List<VaildItem>() { vi });
            return true;
        }
        /// <summary>
        /// 添加输入
        /// </summary>
        /// <param name="Input"></param>
        public void AddInput(NameValueCollection Input)
        {
            if (this.Input == null) this.Input = new Dictionary<string, object>();
            if (Input != null)
            {
                foreach (var i in Input.AllKeys)
                {
                    if (this.Input.ContainsKey(i))
                    {
                        this.Input[i] = Input[i];
                    }
                    else
                    {
                        this.Input.Add(i, Input[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 添加输入
        /// </summary>
        /// <param name="Input"></param>
        public void AddInput(IDictionary<string, object> Input)
        {
            if (this.Input == null) this.Input = new Dictionary<string, object>();
            if (Input != null)
            {
                foreach (var i in Input)
                {
                    if (this.Input.ContainsKey(i.Key))
                    {
                        this.Input[i.Key] = i.Value;
                    }
                    else
                    {
                        this.Input.Add(i.Key, i.Value);
                    }
                }
            }
        }


        /// <summary>
        /// 设置数据字典
        /// </summary>
        /// <param name="Input">输入指点</param>
        public void SetInput(Dictionary<string, object> Input)
        {
            if (Input != null)
                this.Input = Input;
            else
                this.Input = new Dictionary<string, object>();
        }
        /// <summary>
        /// 设置输入并验证
        /// </summary>
        /// <param name="Input"></param>
        public void DataValid(Dictionary<string, object> Input)
        {
            SetInput(Input);
            DataValid();
        }
        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="Input"></param>
        public void DataValid(NameValueCollection Input)
        {
            SetInput(Input);
            DataValid();
        }
        /// <summary>
        /// 设置数据字典
        /// </summary>
        /// <param name="Input">NameValueCollection</param>
        public void SetInput(NameValueCollection Input)
        {
            this.Input = new Dictionary<string, object>();
            if (Input != null)
            {
                foreach (var k in Input.AllKeys)
                {
                    if (!string.IsNullOrEmpty(k))
                        this.Input.Add(k, Input[k]);
                }
            }

        }
        /// <summary>
        /// 验证
        /// </summary>
        public void DataValid()
        {
            //设置默认校验器
            List<VaildItem> BaseVailate = null;
            if (Validate.ContainsKey("*"))
            {
                BaseVailate = Validate["*"];
            }
            //设置默认过滤器
            List<VaildItem> BaseFilter = null;
            if (Formater.ContainsKey("*"))
            {
                BaseFilter = Formater["*"];
            }


            Output = new Dictionary<string, Result<object>>();
            foreach (var i in Input)//循环输入表
            {
                #region base
                if (BaseFilter == null
                    && BaseVailate == null
                    && !Validate.ContainsKey(i.Key)
                    && !Formater.ContainsKey(i.Key)
                    )//如果未设置默认过滤器和验证器 且不再过滤器与验证器名单上的输入将会被抛弃
                    continue;
                #endregion

                Result<object> res = Result<object>.GetResult(true, i.Value, string.Empty);//预设为正常
                #region vailate
                //先处理验证,后处理过滤
                if (Validate.ContainsKey(i.Key))//检查是否有验证器设置
                {
                    foreach (VaildItem vi in Validate[i.Key])
                    {
                        if (!v.Validates[vi.Name](i.Value, vi.Param))//如果验证失败直接返回
                        {
                            res.Bool = false;
                            res.Data = i.Value;
                            res.ErrorMessage = "Validate:" + vi.Name;

                            break;
                        }
                    }
                }
                else if (BaseVailate != null)//如果有默认校验器的
                {
                    foreach (VaildItem vi in BaseVailate)
                    {
                        if (!v.Validates[vi.Name](i.Value, vi.Param))
                        {
                            res.Bool = false;
                            res.Data = i.Value;
                            res.ErrorMessage = "Validate:" + vi.Name;
                            break;
                        }
                    }
                }
                //校验完毕
                if (!res.Bool)//如果验证失败 不过滤直接推出
                {
                    Output.Add(i.Key, res);
                    continue;
                }
                #endregion

                #region format
                //开始转换
                if (Formater.ContainsKey(i.Key))//检查是否有过滤器设置
                {
                    object tempdate = res.Data;
                    foreach (VaildItem fi in Formater[i.Key])
                    {
                        Result<object> ir = f.Formats[fi.Name](tempdate, fi.Param);
                        if (!ir.Bool)//如果失败中断并返回
                        {
                            res.Bool = false;
                            res.ErrorMessage = "Formats:" + fi.Name;
                            res.Data = ir.Data;
                            break;
                        }

                        tempdate = ir.Data;//如果成功,则赋值到寄存器,继续过滤
                    }
                    res.Data = tempdate;//完成之后赋值到返回上
                }
                else if (BaseFilter != null)
                {
                    foreach (VaildItem vi in BaseVailate)
                    {
                        object tempdate = i.Value;
                        foreach (VaildItem fi in BaseFilter)
                        {
                            Result<object> ir = f.Formats[fi.Name](tempdate, fi.Param);
                            if (!ir.Bool)//如果失败中断并返回
                            {
                                res.Bool = false;
                                res.ErrorMessage = "Formats:" + fi.Name;
                                res.Data = ir.Data;
                                break;
                            }

                            tempdate = ir.Data;//如果成功,则赋值到寄存器,继续过滤
                        }
                        res.Data = tempdate;//完成之后赋值到返回上

                    }

                }
                #endregion
                //过滤完毕

                Output.Add(i.Key, res);//赋值到结果字典
            }//end foreach



        }//end func

        /// <summary>
        /// 提取校验结果
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Boolean IsVaild(string Name)
        {
            if (Output.ContainsKey(Name) && Output[Name].Bool)
                return true;
            return false;
        }
        /// <summary>
        /// 提取校验文字
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string IsVaildMessage(string Name)
        {
            if (Output.ContainsKey(Name))
                return Output[Name].ErrorMessage;
            return "Not Found this name";
        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public object GetOutPut(string Name)
        {
            if (Output.ContainsKey(Name) && Output[Name].Bool)
                return Output[Name].Data;
            return null;
        }
        /// <summary>
        /// 获取一个结果
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Result<object> Item(string Name)
        {
            if (Output.ContainsKey(Name))
                return Output[Name];
            return Result<object>.GetResult(false, null, null);
        }
        /// <summary>
        /// 转换为int的结果
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int GetInt(string Name)
        {
            return int.Parse(GetOutPut(Name).ToString());
        }
        /// <summary>
        /// 转换为string
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string GetString(string Name)
        {
            return GetOutPut(Name).ToString();
        }
        /// <summary>
        /// 转换为float
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public float GetFloat(string Name)
        {

            return float.Parse(GetOutPut(Name).ToString());
        }
        /// <summary>
        /// 转换为Bool
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool GetBool(string Name)
        {
            return Boolean.Parse(GetOutPut(Name).ToString());

        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public double GetDouble(string Name)
        {
            return Convert.ToDouble(GetOutPut(Name));
        }
    }
}

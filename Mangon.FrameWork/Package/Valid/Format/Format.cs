using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid.Format
{
    /// <summary>
    /// 
    /// </summary>
    public enum FormatList
    {
        /// <summary>
        /// 布尔值
        /// </summary>
        Bool,
        /// <summary>
        /// 普通字符串,参数:,1 本值,2过滤名单,3,白名单,4,保留中文
        /// </summary>
        String,
        /// <summary>
        /// 浮点数
        /// </summary>
        Float,
        /// <summary>
        ///         /// 把字符串格式化成合法的整数
        /// 过滤全部非数字字符
        /// 如果输入无效者返回0
        /// </summary>
        Int,
        /// <summary>
        ///         /// 把字符串格式化成合法的整数字符串
        /// 过滤全部非数字字符
        /// 如果输入无效者返回0
        /// </summary>
        IntStr,
        /// <summary>
        /// 浮点数字符串
        /// </summary>
        FloatStr,
        /// <summary>
        /// 格式化成时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 返回转换成它们对应 HTML 实体的字符串HtmlEncode
        /// </summary>
        HtmlEncode,
        /// <summary>
        /// 返回转换成它们对应 HTML 实体的字符串HtmlDecode
        /// </summary>
        HtmlDecode,
        /// <summary>
        /// 返回只保留字母和数字的字符串
        /// </summary>
        Alnum
            ,
        /// <summary>
        /// 返回只保留字母的字符串
        /// </summary>
        Alpha,
        /// <summary>
        /// 返回只保留数字的字符串Digits
        /// </summary>
        Digits,
        /// <summary>
        /// 直接用MD5编码,参赛1为返回字符位数 有3个 32,16,8
        /// </summary>
        MD5
    }

    /// <summary>
    /// 
    /// </summary>
    public class Format : FormatDict
    {
        /// <summary>
        /// 
        /// </summary>
        public Format()
        {
            RegFormat(FormatList.Bool.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.Bool().doFormat(Value, strArg); });
            RegFormat(FormatList.MD5.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.MD5().doFormat(Value, strArg); });
            RegFormat(FormatList.String.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.String().doFormat(Value, strArg); });
            RegFormat(FormatList.Float.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.FLoat().doFormat(Value, strArg); });
            RegFormat(FormatList.FloatStr.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.FLoatstr().doFormat(Value, strArg); });
            RegFormat(FormatList.Int.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.Int().doFormat(Value, strArg); });
            RegFormat(FormatList.IntStr.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.Intstr().doFormat(Value, strArg); });
            RegFormat(FormatList.DateTime.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.DateTime().doFormat(Value, strArg); });
            RegFormat(FormatList.HtmlEncode.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.HtmlEncode().doFormat(Value, strArg); });
            RegFormat(FormatList.HtmlDecode.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.HtmlDecode().doFormat(Value, strArg); });
            RegFormat(FormatList.Alnum.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.Alnum().doFormat(Value, strArg); });
            RegFormat(FormatList.Alpha.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.Alpha().doFormat(Value, strArg); });
            RegFormat(FormatList.Digits.ToString(), delegate(object Value, string[] strArg) { return new FormatElements.Digits().doFormat(Value, strArg); });

        }
        /// <summary>
        /// 检查过滤器是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public new bool HasFormat(string Name)
        {
            return base.HasFormat(Name);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Valid.Validate
{
    /// <summary>
    /// 
    /// </summary>
    public enum ValidateList
    {

        /// <summary>
        /// 当且仅当$value只包含字母和数字字符，返回 true
        /// </summary>
        Alnum,
        /// <summary>
        /// 当且仅当$value只包含字母字符，返回 true。这个校验器包括一个考虑空白字符是否有效的选项。 
        /// </summary>
        Alpha,

        /// <summary>
        ///  当且仅当$value在最小值和最大值之间，返回true。缺省地，比较包含边界值（$value可以等于边界值）
        ///  参数1 为最大值
        ///  参数2为最小值
        /// </summary>
        Between,
        /// <summary>
        /// 当$value是一个有效日期，返回true 。
        /// </summary>
        Date,
        /// <summary>
        /// 当且仅当$value只包含数字字符，返回 true。 
        /// </summary>
        IsNumber,
        /// <summary>
        /// 允许你校验一个Float
        /// </summary>
        Float,
        /// <summary>
        /// 允许你校验一个email地址。
        /// </summary>
        IsEmail,
        /// <summary>
        ///  当且仅当$value大于最小值，返回 true。 
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 当且仅当$value是一个有效的整数，返回 true。 
        /// </summary>
        IsInt,
        /// <summary>
        /// Ip
        /// </summary>
        IsIp,
        /// <summary>
        /// 当且仅当$value小于最大值，返回 true。  
        /// 
        /// </summary>
        LessThan,
        /// <summary>
        /// 当且仅当$value非空，返回 true。 
        /// </summary>
        NotEmpty,
        /// <summary>
        /// 当且仅当$value匹配一个正则表达式，返回 true。 
        /// </summary>
        IsRegex,
        /// <summary>
        /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /// 参数0为最短
        /// 参数1为最长
        /// </summary>
        StringLength,
        /// <summary>
        /// 验证时间
        /// </summary>
        IsTime,

        /// <summary>
        ///  /// 验证手机号
        /// </summary>
        IsMobile,
        /// <summary>
        ///  验证身份证是否有效,只支持中国
        /// </summary>
        IsIDCard,
        /// <summary>
        /// 是不是中国电话，格式010-85849685
        /// </summary>
        IsTel,
        /// <summary>
        ///  邮政编码 6个数字
        /// </summary>
        IsPostCode,
        /// <summary>
        /// 中文
        /// </summary>
        IsChinese,
        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        IsSafeChar,
        /// <summary>
        /// 验证网址
        /// </summary>
        IsUrl,
        /// <summary>
        /// 有中文
        /// </summary>
        HasChinese,
        /// <summary>
        /// 有IP
        /// </summary>
        HasIp,
        /// <summary>
        /// 有手机D:\Server\doctor\FamilyDoctor.FrameWork\Package\Repository\
        /// </summary>
        HasMobile,
        /// <summary>
        /// 15为身份证号
        /// </summary>
        IsIDCard15,
        /// <summary>
        /// 18为身份证号
        /// </summary>
        IsIDCard18
    }

    /// <summary>
    /// 
    /// </summary>
    public class Validate : ValidateDict
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetType(Type interfaceType)
        {
            Assembly assembly = Assembly.LoadFrom("FDFW.Package.Valid.Validate.ValidateElements");
            foreach (var type in assembly.GetTypes())
            {
                foreach (var t in type.GetInterfaces())
                {
                    if (t == interfaceType)
                    {
                        yield return type;
                        break;
                    }
                }
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public Validate()
        {

            RegValidate(ValidateList.Alnum.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.Alnum().doValidate(Value, strArg); });
            RegValidate(ValidateList.Alpha.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.Alpha().doValidate(Value, strArg); });
            //  RegValidate("Array", delegate(object Value, string[] strArg){ return new Valid.Validate.ValidateElements.Array().doValidate(Value, strArg); });
            RegValidate(ValidateList.Between.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.Between().doValidate(Value, strArg); });
            RegValidate(ValidateList.GreaterThan.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.GreaterThan().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsChinese.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsChinese().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsTel.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsChineseTel().doValidate(Value, strArg); });
            RegValidate(ValidateList.Date.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsDateTime().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsEmail.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsEmail().doValidate(Value, strArg); });
            RegValidate(ValidateList.Float.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsFloat().doValidate(Value, strArg); });
            //  RegValidate("Hex", delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsHex().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsUrl.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsHttpUrl().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsIDCard.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsIDCard().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsIDCard15.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsIDCard15().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsIDCard18.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsIDCard18().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsInt.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsInt().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsIp.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsIp().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsMobile.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsMobile().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsNumber.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsNumber().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsPostCode.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsPostCode().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsRegex.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsRegex().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsSafeChar.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsSafeChar().doValidate(Value, strArg); });
            RegValidate(ValidateList.IsTime.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.IsTime().doValidate(Value, strArg); });
            RegValidate(ValidateList.LessThan.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.LessThan().doValidate(Value, strArg); });
            RegValidate(ValidateList.NotEmpty.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.NotEmpty().doValidate(Value, strArg); });
            RegValidate(ValidateList.StringLength.ToString(), delegate(object Value, string[] strArg) { return new Valid.Validate.ValidateElements.StringLength().doValidate(Value, strArg); });


            // a.g
            /* 
              RegFormat("Alnum", delegate(object Value, string[] strArg) {        return new Valid. fs.Alnum(Value, strArg); });
              RegFormat("Alpha", delegate(object Value, string[] strArg) {        return fs.Alpha(Value, strArg); });
              RegFormat("Between", delegate(object Value, string[] strArg) {      return fs.Between(Value, strArg); });
              RegFormat("Date", delegate(object Value, string[] strArg) {         return fs.Date(Value, strArg); });
              RegFormat("Float", delegate(object Value, string[] strArg) {        return fs.Float(Value, strArg); });
              RegFormat("GreaterThan", delegate(object Value, string[] strArg) {  return fs.GreaterThan(Value, strArg); });
              RegFormat("HasChinese", delegate(object Value, string[] strArg) {   return fs.HasChinese(Value, strArg); });
              RegFormat("HasIp", delegate(object Value, string[] strArg) {        return fs.HasIp(Value, strArg); });
              RegFormat("HasMobile", delegate(object Value, string[] strArg) {    return fs.HasMobile(Value, strArg); });
              RegFormat("IsChinese", delegate(object Value, string[] strArg) {    return fs.IsChinese(Value, strArg); });
              RegFormat("IsEmail", delegate(object Value, string[] strArg) {      return fs.IsEmail(Value, strArg); });
              RegFormat("IsIDCard", delegate(object Value, string[] strArg) {     return fs.IsIDCard(Value, strArg); });
              RegFormat("IsIDCard15", delegate(object Value, string[] strArg) {   return fs.IsIDCard15(Value, strArg); });
              RegFormat("IsIDCard18", delegate(object Value, string[] strArg) {   return fs.IsIDCard18(Value, strArg); });
              RegFormat("IsInt", delegate(object Value, string[] strArg) {        return fs.IsInt(Value, strArg); });
              RegFormat("IsIp", delegate(object Value, string[] strArg) {         return fs.IsIp(Value, strArg); });
              RegFormat("IsMobile", delegate(object Value, string[] strArg) {     return fs.IsMobile(Value, strArg); });
              RegFormat("IsNumber", delegate(object Value, string[] strArg) {     return fs.IsNumber(Value, strArg); });
              RegFormat("IsPostCode", delegate(object Value, string[] strArg) {   return fs.IsPostCode(Value, strArg); });
              RegFormat("IsRegex", delegate(object Value, string[] strArg) {      return fs.IsRegex(Value, strArg); });
              RegFormat("IsSafeChar", delegate(object Value, string[] strArg) {   return fs.IsSafeChar(Value, strArg); });
              RegFormat("IsTel", delegate(object Value, string[] strArg) {        return fs.IsTel(Value, strArg); });
              RegFormat("IsTime", delegate(object Value, string[] strArg) {       return fs.IsTime(Value, strArg); });
              RegFormat("LessThan", delegate(object Value, string[] strArg) {     return fs.LessThan(Value, strArg); });
              RegFormat("NotEmpty", delegate(object Value, string[] strArg) {     return fs.NotEmpty(Value, strArg); });
              RegFormat("StringLength", delegate(object Value, string[] strArg) { return fs.StringLength(Value, strArg); });
              //RegFormat("StringLength", delegate(object Value, string[] strArg) { return fs.IsRegex(Value, strArg); });
             */

        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.SystemExtension
{
    public static class SystemExtension_DataTable
    {
        /// <summary>
        /// DataRow扩展方法：将DataRow类型转化为指定类型的实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dr) where T : class, new()
        {
            return ToModel<T>(dr, true);
        }
        /// <summary>
        /// DataRow扩展方法：将DataRow类型转化为指定类型的实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dateTimeToString">是否需要将日期转换为字符串，默认为转换,值为true</param>
        /// <returns></returns>
        /// <summary>
        public static T ToModel<T>(this DataRow dr, bool dateTimeToString) where T : class, new()
        {
            if (dr != null)
                return ToList<T>(dr.Table, dateTimeToString).First();

            return null;
        }

        /// <summary>
        /// DataTable扩展方法：将DataTable类型转化为指定类型的实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            return ToList<T>(dt, true);
        }

        /// <summary>
        /// DataTable扩展方法：将DataTable类型转化为指定类型的实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dateTimeToString">是否需要将日期转换为字符串，默认为转换,值为true</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt, bool dateTimeToString) where T : class, new()
        {
            List<T> list = new List<T>();

            if (dt != null)
            {
                List<PropertyInfo> infos = new List<PropertyInfo>();

                Array.ForEach<PropertyInfo>(typeof(T).GetProperties(), p =>
                {
                    if (dt.Columns.Contains(p.Name) == true)
                    {
                        infos.Add(p);
                    }
                });

                SetList<T>(list, infos, dt, dateTimeToString);
            }

            return list;
        }

        #region 私有方法

        private static void SetList<T>(List<T> list, List<PropertyInfo> infos, DataTable dt, bool dateTimeToString) where T : class, new()
        {
            foreach (DataRow dr in dt.Rows)
            {
                T model = new T();

                infos.ForEach(p =>
                {
                    if (dr[p.Name] != DBNull.Value)
                    {
                        object tempValue = dr[p.Name];
                        if (dr[p.Name].GetType() == typeof(DateTime) && dateTimeToString == true)
                        {
                            tempValue = dr[p.Name].ToString();
                        }
                        try
                        {

                            p.SetValue(model, Convert.ChangeType(tempValue, p.PropertyType), null);
                        }
                        catch
                        {
                        }
                    }
                });
                list.Add(model);
            }
        }

        #endregion
        public static Dictionary<int, Dictionary<string, string>> ToStrDictionary(this DataTable dt)
        {
            Dictionary<int, Dictionary<String, string>> MDict = new Dictionary<int, Dictionary<String, string>>(dt.Rows.Count);

            String[] NameAry = new string[dt.Columns.Count];
            for (int i = 0; i < NameAry.Length; i++)
            {
                NameAry[i] = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MDict.Add(i, DrToDictStr(dt.Rows[i], NameAry));
            }
            return MDict;
        }
        /// <summary>
        /// dt转换成2层的字典
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<String, Object>> ToDictionary(this DataTable dt)
        {
            Dictionary<int, Dictionary<String, Object>> MDict = new Dictionary<int, Dictionary<String, Object>>(dt.Rows.Count);

            String[] NameAry = new string[dt.Columns.Count];
            for (int i = 0; i < NameAry.Length; i++)
            {
                NameAry[i] = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MDict.Add(i, DrToDict(dt.Rows[i], NameAry));
            }
            return MDict;
        }
        /// <summary>
        /// dr转换成字典
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> ToDictionary(this DataRow dr, String[] column)
        {
            //String[] nary=dr.Table.Columns.co
            String[] nary = new String[column.Length];
            for (int i = 0; i < column.Length; i++)
            {
                if (dr.Table.Columns.Contains(column[i]))
                {
                    nary[i] = column[i];
                }
            }
            return DrToDict(dr, nary);
        }
        /// <summary>
        /// dr转船成字典
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> ToDictionary(this DataRow dr, DataColumn[] column)
        {
            //String[] nary=dr.Table.Columns.co
            String[] nary = new String[column.Length];
            for (int i = 0; i < column.Length; i++)
            {
                if (dr.Table.Columns.Contains(column[i].ColumnName))
                {
                    nary[i] = column[i].ColumnName;
                }
            }
            return DrToDict(dr, nary);

        }
        /// <summary>
        ///  dr转船成字典
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> ToDictionary(this DataRow dr)
        {
            //String[] nary=dr.Table.Columns.co
            String[] nary = new String[dr.Table.Columns.Count];
            for (int i = 0; i < nary.Length; i++)
            {
                nary[i] = dr.Table.Columns[i].ColumnName;
            }
            return DrToDict(dr, nary);

        }


        private static Dictionary<String, String> DrToDictStr(DataRow dr, String[] NameAry)
        {
            Dictionary<String, String> Dict = new Dictionary<string, String>(NameAry.Length);
            for (int i = 0; i < NameAry.Length; i++)
            {
                Dict.Add(NameAry[i], dr[NameAry[i]].ToString());
            }
            return Dict;
        }
        private static Dictionary<String, Object> DrToDict(DataRow dr, String[] NameAry)
        {
            Dictionary<String, Object> Dict = new Dictionary<string, object>(NameAry.Length);
            for (int i = 0; i < NameAry.Length; i++)
            {
                Dict.Add(NameAry[i], dr[NameAry[i]]);
            }
            return Dict;
        }
    }
}

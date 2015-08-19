using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 转化成Datatable
    /// </summary>
    public static  class Mangon_FrameWorkSystemExtension_IList
    {
        /// <summary>
        /// 转化成DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IList list)
        {
            DataTable result = new DataTable();
            if (list.Count>0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo item in propertys)
                {
                    result.Columns.Add(item.Name, item.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList templist = new ArrayList();
                    foreach (PropertyInfo item in propertys)
                    {
                        object obj = item.GetValue(list[i]);
                        templist.Add(obj);
                    }
                    object[] array = templist.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}

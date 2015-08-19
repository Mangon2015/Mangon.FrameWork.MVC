using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Mangon.FrameWork.Package.Encode
{
    public class Xml
    {
        /// <summary>
        /// 借用 soap解码
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">xml</param>
        /// <returns>对象</returns>
        public static T DataDecode<T>(string str)
        { 
            
            DataContractSerializer serializer=new DataContractSerializer(typeof(T));
            using (MemoryStream ms=new MemoryStream())
            {
                byte[] b = Encoding.UTF8.GetBytes(str);
                ms.Write(b, 0, b.Length);
                ms.Position = 0;
                return (T)serializer.ReadObject(ms);
            }
        }
        /// <summary>
        /// 借用soap编码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DataEncode(object obj)
        {
            string str = null;
            DataContractSerializer dcs = new DataContractSerializer(obj.GetType());
            using (MemoryStream ms=new MemoryStream())
            {
                try
                {
                                    dcs.WriteObject(ms, obj);
                ms.Position = 0;
                using (StreamReader sr=new StreamReader(ms))
                {
                    str = sr.ReadToEnd();
                }
                return str;
                }
                catch (Exception)
                {

                    obj.ToString();
                }
                return str;
            }
        }

        public static XmlDocument EncodeToSimplyXmlDoc(object obj)
        {
            var xml = EncodeToXmlDoc(obj);
            xml.RemoveChild(xml.ChildNodes.Item(0));
            xml.FirstChild.Attributes.RemoveAll();
            xml.InsertBefore(xml.CreateXmlDeclaration("1.0", "utf-8", ""), xml.FirstChild);
            return xml;
        }
        /// <summary>
        /// 序列化成xmldocument 并按照标准化处理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XmlDocument EncodeToXmlDoc(object obj)
        {
            XmlDocument xml = new XmlDocument();
            using (MemoryStream ms=new MemoryStream())
            {
                XmlSerializer ser = new XmlSerializer(obj.GetType());
                ser.Serialize(ms, obj);
                ms.Position = 0;
                xml.Load(ms);
                
            }
            return xml;
        }
        /// <summary>
        /// 用XmlSerializer 编码,对象需要支持xml序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>xml</returns>
        public static string Encode(object obj)
        {
            return EncodeToXmlDoc(obj).OuterXml;
        }
        /// <summary>
        /// 用XmlSerializer 解码
        /// </summary>
        /// <param name="str">xml</param>
        /// <param name="type">需要获得的类型</param>
        /// <returns>对象</returns>
        public static object Decode(string str, Type type)
        {
            XmlSerializer ms = new XmlSerializer(type);
            using (StringReader sr=new StringReader(str))
            {
                return ms.Deserialize(sr);
            }
        }

        /// <summary>
        /// 未完成 只支持 int 和 string格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlstr"></param>
        /// <param name="Root"></param>
        /// <returns></returns>
        public static T ParseTo<T>(string xmlstr, string Root) where T : class,new()
        {
            T retobject = new T();
            XElement objXMl = XElement.Parse(xmlstr);
            Type type = typeof(T);

            Dictionary<string, PropertyInfo> names = type.GetProperties().ToDictionary(p => p.Name, p => p);
            foreach (var dev in objXMl.Elements())
            {
                if (string.IsNullOrWhiteSpace(dev.Value)) continue;
                if (names.ContainsKey(dev.Name.LocalName))
                {
                    if (names[dev.Name.LocalName].PropertyType.Name == "Int")
                    {
                        names[dev.Name.LocalName].SetValue(retobject, Convert.ToInt32(dev.Value), null);
                    }
                    else if (names[dev.Name.LocalName].PropertyType.Name == "String")
                    {
                        names[dev.Name.LocalName].SetValue(retobject, dev.Value, null);
                    }
                }//end if

            }//end  foreach
            return retobject;
        }//end func
    }
}

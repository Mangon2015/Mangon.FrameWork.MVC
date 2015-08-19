using Mangon.FrameWork.Package.Encode.JsonParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Mangon.FrameWork.Package.Encode
{
    /// <summary>
    /// json编码
    /// </summary>
   public  class Json
    {
       public static T JavaScriptSerializer<T>(string str)
       {
           JavaScriptSerializer js = new JavaScriptSerializer();
          return  js.Deserialize<T>(str);
       }
       /// <summary>
       /// Json序列化,用于发送到客户端,调用Soap对象
       /// </summary>
       public static string Encoder(object item)
       {
           
           DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
           using (MemoryStream ms=new MemoryStream())
           {
               serializer.WriteObject(ms, item);
               StringBuilder sb = new StringBuilder();
               sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
               return sb.ToString();
           }
       }
       /// <summary>
       /// Json 序列化，用户发送到客户端，调用Soap对象
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public static byte[] Encoder2Byte(object item)
       {
           DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
           using (MemoryStream ms=new MemoryStream())
           {
               serializer.WriteObject(ms, item);
               return ms.ToArray();
           }
       }
       /// <summary>
       /// Json 序列化，用户发送到客户端，调用Soap对象
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public static Stream Encoder2Stream(object item)
       {
           DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
           using (MemoryStream ms=new MemoryStream())
           {
               serializer.WriteObject(ms, item);
               return ms;
           }
       }
       /// <summary>
       /// Json反序列化，用于接收客户端json后生成的对象
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="jsonString"></param>
       /// <returns></returns>
       public static T Decoder<T>(string jsonString)
       {
           DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
           using (MemoryStream ms=new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
           {
               T jsonObject = (T)serializer.ReadObject(ms);
               return jsonObject;
           }
       }
       /// <summary>
       /// Json反序列化,用于接收客户端Json后生成对应的对象
       /// </summary>
       public static T Decoder<T>(byte[] jsonArray)
       {
           DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
           using (MemoryStream ms=new MemoryStream())
           {
               T jsonObject = (T)serializer.ReadObject(ms);
               return jsonObject;
           }
       }

       public static dynamic Decode(string jsonString)
       {
           var serializer = new JavaScriptSerializer();
           serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
           return serializer.Deserialize<object>(jsonString);
       }

       public static dynamic CreateDynamicJsonObject()
       {
           return new DynamicJsonObject();
       }
       public static string Encode(DynamicJsonObject jobject)
       {
           return jobject.ToString();
       }
    }
}

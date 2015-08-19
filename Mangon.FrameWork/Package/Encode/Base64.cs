using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Encode
{
    /// <summary>
    /// base64编码
    /// </summary>
   public  class Base64
    {
       /// <summary>
       /// base64编码 加密
       /// </summary>
       /// <param name="sourceString">需要加密字符串</param>
       /// <param name="encoding">编码格式</param>
       /// <returns></returns>
       public static string Encode(string sourceString, System.Text.Encoding encoding)
       {
           return Convert.ToBase64String(encoding.GetBytes(sourceString));
       }

       public static string Encode(string sourceString)
       {
          return Encode(sourceString, System.Text.Encoding.GetEncoding(54936));
       }

       /// <summary>
       /// 从base64编码的字符串中还原字符串，支持中文
       /// </summary>
       /// <param name="base64String">base64加密后的字符串</param>
       /// <param name="ens">System.Text.Encoding 对象，如创建中文编码集对象：System.Text.Encoding.GetEncoding(54936)</param>
       /// <returns>还原后的文本字符串</returns>
       public static string Decode(string base64String, System.Text.Encoding encoding)
       {
           return encoding.GetString(Convert.FromBase64String(base64String));
       }
       /// <summary>
       /// 解码
       /// </summary>
       /// <param name="base64String"></param>
       /// <returns></returns>
       public static string Decode(string base64String)
       {
           return Decode(base64String, System.Text.Encoding.GetEncoding(54936));
       }

       /// <summary>
       /// 对任意类型的文件进行base64加码
       /// </summary>
       /// <param name="fileName">文件的路径和文件名</param>
       /// <returns>对文件进行base64编码后的字符串</returns>
       public static string EncodingForFile(string fileName)
       {
           string str;
           using (System.IO.FileStream fs=System.IO.File.OpenRead(fileName))
           {
               using (System.IO.BinaryReader br=new System.IO.BinaryReader(fs))
               {
                   str = Convert.ToBase64String(br.ReadBytes((int)fs.Length));
               }
           }
           return str;
       }
       /// <summary>
       /// 把经过base64编码的字符串保存为文件
       /// </summary>
       /// <param name="base64String">经base64加码后的字符串</param>
       /// <param name="fileName">保存文件的路径和文件名</param>
       /// <returns>保存文件是否成功</returns>
       public static bool SaveDecodingToFile(string base64String, string fileName)
       {
           using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
           {
               byte[] b = Convert.FromBase64String(base64String);
               fs.Write(b, 0, b.Length);
               // using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
               //{
               //     bw.Write();
               // }
           }
           return true;
       }

       /// <summary>
       /// 从网络地址一取得文件并转化为base64编码
       /// </summary>
       /// <param name="url">文件的url地址,一个绝对的url地址</param>
       /// <param name="objWebClient">System.Net.WebClient 对象</param>
       /// <returns></returns>
       public static string EncodingFileFromUrl(string url, System.Net.WebClient webClient)
       {
           return Convert.ToBase64String(webClient.DownloadData(url));
       }
       /// <summary>
       /// 从网络地址一取得文件并转化为base64编码
       /// </summary>
       /// <param name="url">文件的url地址,一个绝对的url地址</param>
       /// <returns>将文件转化后的base64字符串</returns>
       public static string EncodingFileFromUrl(string url)
       {
           using (System.Net.WebClient myWebClient = new System.Net.WebClient())
           {
               return EncodingFileFromUrl(url, myWebClient);
           }
       }
    }
}

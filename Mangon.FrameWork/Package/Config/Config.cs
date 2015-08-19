using Mangon.FrameWork.Package.Encode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Config
{
    /// <summary>
    /// 配置文件
    /// </summary>
    [Serializable]
    public class Config : IConfig
    {
        public virtual string WriteToXml()
        {
            try
            {
                return Xml.Encode(this);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public virtual void WriteToFile(string filePath)
        {
            System.IO.File.WriteAllText(filePath, this.WriteToXml());
        }

        public static T ReadFromFile<T>(string path) where T : class,IConfig
        {
            return ReadFromFile<T>(path, Encoding.UTF8);
        }
        public static T ReadFromFile<T>(string path, Encoding encoding) where T : class,IConfig
        {
            try
            {
                return Config.ReadFromString<T>(System.IO.File.ReadAllText(path, encoding));
            }
            catch 
            {

                return null;
            }
        }

        public static T ReadFromString<T>(string str) where T : class,IConfig
        {
            try
            {
                return Xml.Decode(str, typeof(T)) as T;
            }
            catch 
            {
                
                return null;
            }
        }

        public static string WriteFormConfig<T>(T self) where T : IConfig
        {
            try
            {
                return Xml.Encode(self);
            }
            catch
            {

                return null;
            }
        }
    }
}

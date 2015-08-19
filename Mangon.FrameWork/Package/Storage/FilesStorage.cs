using Mangon.FrameWork.Elements;
using Mangon.FrameWork.Package.Encode;
using Mangon.FrameWork.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Storage
{
    /// <summary>
    /// 文件封装
    /// </summary>
   public  class FilesStorage:IStorage
    {
       private FileStore store = new FileStore();
       public bool AutoSave = false;
       private string fileName;


       public FilesStorage(string name, string path, string ext, double iExpires)
       {
           if (path[path.Length-1]=='/')
           {
               path.Substring(0, path.Length - 1);//去除最后的/
           }
           path = System.Web.HttpContext.Current.Server.MapPath(path);
           fileName = path = "/" + name + ext;
           FileInfo f;
           bool exists = false;
           bool timeout = true;
           if (File.Exists(fileName))
           {
               exists = true;
               f = new FileInfo(fileName);
               if (f.LastWriteTime>DateTime.Now)
               {
                   timeout = false;
               }
           }
           else
           {
               f = null;
           }
           if (exists && timeout) //如果过期并存在
           {
               try
               {
                   f.Delete();
               }
               catch 
               {
               }
           }

           if (File.Exists(fileName)) 
           {
               store.Timeout = DateTime.Now.AddSeconds(iExpires);//更新时间
               String Data = Xml.Encode(store);
               using (FileStream fs = new FileStream(fileName, FileMode.Create))
               {
                   using (StreamWriter sw = new StreamWriter(fs))
                   {
                       sw.WriteLine(Data);
                   }
               }
               f = new FileInfo(fileName);
           }

           f.LastWriteTime = DateTime.Now.AddSeconds(iExpires);//设置新的过期时间
           String AllData = "";
           using (StreamReader sr = new StreamReader(fileName))
           {
               AllData = sr.ReadToEnd();
           }
           try
           {
               store = Xml.Decode(AllData, store.GetType()) as FileStore;
           }
           catch
           {
               store = new FileStore();
           }
       }

        public Result.Result<object> Get(string name)
        {
            if (store.Store.ContainsKey(name))
                return Result<object>.GetResult(true, store.Store[name], null);
            return Result<object>.GetResult(false, null, null);
        }

        public bool Set(string name, object data)
        {
            if (store.Store.ContainsKey(name))
                store.Store[name] = data;
            else
                store.Store.Add(name, data);

            if (AutoSave)
            {
                var res = Save();
                return res.Bool;
            }
            else
                return true;
        }

        public bool Kill()
        {
            System.IO.File.Delete(fileName);
            return true;
        }

        public Result<object> Save()
        {
            try
            {
                String Data = Xml.Encode(store);
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {

                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(Data);
                    }
                }
                return Result<object>.GetResult(true, null, null);
            }
            catch (Exception e)
            {
                return Result<object>.GetResult(false, e, e.Message);
            }

        }
        public FilesStorage(string name, double iExpires)
            : this(name, "~/Cache", ".Storage", iExpires)
        { }
        public FilesStorage(string name) : this(name, 3153600) {
            AutoSave = false;
        }

        public FilesStorage(string name, bool autoSave) : this(name) {
            AutoSave = autoSave;
        }
        public FilesStorage(string name, string path)
            : this(name, path, ".Storage")
        { AutoSave = false; }
        public FilesStorage(string name, string path, string ext)
            : this(name, path, ext, 3153600)
        { }
    }

   /// <summary>
   /// 
   /// </summary>
   public class FileStore
   {
       /// <summary>
       /// 
       /// </summary>
       public SerializableDictionary<string, object> Store = new SerializableDictionary<string, object>();
       /// <summary>
       /// 
       /// </summary>
       public DateTime Timeout;
   }
}

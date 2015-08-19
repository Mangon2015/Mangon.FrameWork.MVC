using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mangon.FrameWork.Package.Storage;

namespace Mangon.FrameWork.Package.Storage
{
    /// <summary>
    /// 存储工厂
    /// </summary>
   public class StorageFactory:InterFace.IFactory<IStorage>
    {
       protected IDictionary<string, IStorage> StorDice = new Dictionary<string, IStorage>();

       public override Type baseType
       {
           get { return typeof(IStorage); }
       }

       protected StorageFactory() { }

       public static IStorage GetInstance(string stroageName, string namePlace, double exp = 60*60)
       {
           switch (stroageName.ToLowerInvariant())
           {
               case "session":
                   return new StorageFactory().Session(namePlace, exp);
               case "cache":
                   return new StorageFactory().Cache(namePlace, exp);
               case "cookie":
                   return new StorageFactory().Cookies(namePlace, exp);
               case "file":
                   return new StorageFactory().Files(namePlace, exp);
               case "simplycache":
                   return new StorageFactory().SimplyCache(namePlace, exp);
           }
           return null;
       }
       /// <summary>
       /// 优先由本进程读取
       /// </summary>
       /// <param name="name"></param>
       /// <param name="exp"></param>
       /// <returns></returns>
       protected IStorage Session(string name, double exp)
       {
           name = "Session_" + name;
           if (!StorDice.ContainsKey(name))
           {
               StorDice.Add(name, new SessionStorage(name, exp));
           }
           return StorDice[name];
       }

       protected IStorage Cache(string name, double exp)
       {
           name = "Cache_" + name;
           if (!StorDice.ContainsKey(name))
           {
               StorDice.Add(name, new CacheStorage(name, exp));
           }
           return StorDice[name];
       }

       protected IStorage SimplyCache(string name, double exp)
       {
           name = "SimplyCache_" + name;
           if (!StorDice.ContainsKey(name))
           {
               StorDice.Add(name, new SimplyCacheStorage(name, exp));
           }
           return StorDice[name];
       }

       protected IStorage Cookies(string name, double exp)
       {
           name = "Cookies_" + name;
           if (!StorDice.ContainsKey(name))
           {
               StorDice.Add(name, new CookiesStorage(name, exp));
           }
           return StorDice[name];
       }

       protected IStorage Files(string name, double exp)
       {
           name = "File_" + name;
           if (!StorDice.ContainsKey(name))
           {
               StorDice.Add(name, new FilesStorage(name, exp));
           }
           return StorDice[name];
       }

    }   
}

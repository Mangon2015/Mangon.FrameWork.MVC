using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Mangon.FrameWork.Result;

namespace Mangon.FrameWork.Package.Storage
{
   public class SessionStorage:IStorage
    {
       private string place;

       private HttpSessionState curSession
       {
           get { return HttpContext.Current.Session; }
           set { }
       }
       public Result.Result<object> Get(string name)
       {
           try
           {
               Dictionary<string, object> Data = curSession[place] as Dictionary<string, object>;
               if (!Data.ContainsKey(name))
                   return Result<object>.GetResult(false, null, null);
               else
               {
                   return Result<object>.GetResult(true, Data[name], null);
               }
           }
           catch (Exception e)
           {
               return Result<object>.GetResult(false, null, e.Message);
           }
       }

        public bool Set(string name, object data)
        {
            Dictionary<string, object> Data = curSession[place] as Dictionary<string, object>;

            if (Data == null) return false;

            if (!Data.ContainsKey(name))
            {
                Data.Add(name, data);
            }
            else
            {
                if (data == null)
                    Data.Remove(name);
                else
                    Data[name] = data;
            }

            curSession[place] = Data;
            return true;
        }

        public bool Kill()
        {
            try
            {
                curSession[place] = null;
                curSession.Remove(place);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public SessionStorage(string name)
            : this(name, 3600)
        { }
        public SessionStorage(string name, double iExpires)
        {
            this.place = name;
            try//如果无法读取就忽视
            {
                this.curSession = HttpContext.Current.Session;
            }
            catch { return; }
            Dictionary<string, object> Data = curSession[name] as Dictionary<string, object>;
            if (Data == null)
            {
                Dictionary<string, object> Stores = new Dictionary<string, object>();
                curSession[name] = Stores;
            }

            curSession.Timeout = int.Parse(iExpires.ToString());
        }
    }
}

using Mangon.FrameWork.MVC.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.MVC.MVC.ACL
{
    public static class ACLInit
    {
        public static void Init(string Key, int Index)
        {
          //  new Mangon.FrameWork.Package.ACL.Client(Key, Index);
            _AuthorizationFilter.Init(Key, "login/long", "error/error", new List<string>() { "home" }, new List<string>());


        }
        public static void Init(string Key, int Index, string LogUrl, string ErrorUrl, List<string> CtrlList, List<string> IpList)
        {
          //  new Mangon.FrameWork.Package.ACL.Client(Key, Index);
            _AuthorizationFilter.Init(Key, LogUrl, ErrorUrl, CtrlList, IpList);


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Excption
{
   public class ACLLoginExcption:Exception
    {
       public override string Message
       {
           get
           {
               return "ACL Login Error";
           }
       }
    }
}

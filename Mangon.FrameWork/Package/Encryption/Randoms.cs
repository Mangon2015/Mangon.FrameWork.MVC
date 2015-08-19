using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Encryption
{
  public  class Randoms
    {
      public static string GetRandom(int length)
      {
          Random ra = new Random(Guid.NewGuid().GetHashCode());
          if (length!=0)
          {
              return ra.Next().ToString().Substring(0, length);
          }
          return ra.Next().ToString();
      }
    }
}

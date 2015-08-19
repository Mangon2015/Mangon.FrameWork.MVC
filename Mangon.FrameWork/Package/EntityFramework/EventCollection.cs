using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework
{

    public enum EFAction
    { 
        Insert=1,
        Updata=2,
        Delete=3,
        SaveChange=4,
        Search=5,
        Other=9
    }
    public class MangonEntityFrameworkException : Exception
    {
        public MangonEntityFrameworkException(Exception e)
            : base("MangonEntityFrameworkException", e)
        { }
        public IEnumerable<DbEntityValidationResult> DbEntityValidationResult { get; set; }
    }

    public delegate void EFEventHandler(object sender, EFEventArgs e);

    public class EFEventArgs : EventArgs
    {
        public object Data;
        public EFAction Action;
        public EFEventArgs(ref object data, EFAction action)
        {
            Data = data;
            Action = action;
        }
    }
}

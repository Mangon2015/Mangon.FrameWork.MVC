using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.MVC
{
    [DataContract]
    public enum AjaxResultInlineMode
    {
        [EnumMember(Value = "replace")]
        Replace,
        [EnumMember(Value = "replaceWith")]
        ReplaceWith,
        [EnumMember(Value = "append")]
        Append,
        [EnumMember(Value = "before")]
        InsertBefore,
        [EnumMember(Value = "after")]
        InsertAfter,


    }
}

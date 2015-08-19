using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.MVC
{
    [DataContract]
    public enum AjaxRequestDataType
    {
        [EnumMember(Value = "html")]
        Html,
        [EnumMember(Value = "json")]
        Json,
        Jsonp,
        [EnumMember(Value = "script")]
        Script,
        [EnumMember(Value = "xml")]
        Xml,
        Text
    }
}

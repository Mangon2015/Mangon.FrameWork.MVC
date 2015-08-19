using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Mangon.FrameWork.Package.EntityFramework.Element
{
    /// <summary>
    /// 表的键
    /// </summary>
    public class EntityKey
    {
        public Type EntityType { get; set; }
        public Type EntityKeyType { get; set; }
        public string EntityKeyName { get; set; }
        public string EntityKeyTypeStr { get; set; }
        public PropertyInfo Key {
            get {
                if (EntityKeyName!=null)
                {
                    return EntityType.GetProperty(EntityKeyName);
                }
                return null;
            }
        }
    }
}

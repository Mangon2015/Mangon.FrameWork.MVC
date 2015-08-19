using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Metadata.Edm;

namespace Mangon.FrameWork.Package.EntityFramework.Element
{
   public class EntityProperties
    {
        public EdmType   Entity { get; set; }
        public bool IsKey { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public Type Type { get; set; }
        public string TypeInBb { get; set; }
        public bool Nullable { get; set; }
        public object DefaultValue { get; set; }
        public Dictionary<string, Facet> Facts = new Dictionary<string, Facet>();
    }
}

using Mangon.FrameWork.Core;
using Mangon.FrameWork.Package.EntityFramework.Element;
using Mangon.FrameWork.Package.EntityFramework.InterFace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.EntityFramework
{
    public abstract class DataBaseContext<TDb> : DataBaseBase, IDbContext, IDataBase where TDb : DataBaseBase, new()
    {
        private TDb _db = null;
        protected TDb Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new TDb();
                }
                return _db;
            }
        }
        public DataBaseContext() { }
        public DataBaseContext(DbContext dbContext) : base(dbContext) { }
        /// <summary>
        /// 命名空间
        /// </summary>
        protected MetadataWorkspace MetaData
        {
            get { return this.ObjectContext.MetadataWorkspace; }
        }

        protected EntityContainer Container
        {
            get
            {
                return MetaData.GetItemCollection(DataSpace.SSpace).GetItems<EntityContainer>().Single();
            }
        }

        private object obj = new object();
        internal static KeyValueCollection<string, EntitySetBase> _sets = null;
        protected KeyValueCollection<string, EntitySetBase> Sets
        {
            get
            {
                if (_sets == null || _sets.Count == 0)
                {
                    lock (obj)
                    {
                        _sets = new KeyValueCollection<string, EntitySetBase>();
                        foreach (var item in Container.BaseEntitySets)
                        {
                            _sets.Add(new KeyValuePair<string, EntitySetBase>(item.Name, item));
                        }
                    }
                }
                return _sets;
            }
        }

        EntityKey[] IDbContext.GetKeys(string entityName)
        {
            List<EntityKey> ek = new List<EntityKey>();
            var set = GetSet(entityName).ElementType;
            var keys = set.KeyMembers;
            foreach (var item in keys)
            {
                var key = new EntityKey();
                key.EntityKeyName = item.Name;
                key.EntityKeyTypeStr = item.TypeUsage.EdmType.Name;
                key.EntityKeyType = ((PrimitiveType)item.TypeUsage.EdmType).ClrEquivalentType;
                ek.Add(key);
            }
            return ek.ToArray();
        }

        private static object _keyStoreLock = new object();
        internal static Dictionary<EntitySetBase, EntityKey[]> _keyStore = null;
        public Dictionary<EntitySetBase, EntityKey[]> KeyStore
        {
            get
            {
                if (_keyStore == null)
                {
                    lock (_keyStoreLock)
                    {
                        _keyStore = new Dictionary<EntitySetBase, EntityKey[]>();
                        foreach (var item in Sets)
                        {
                            _keyStore.Add(item.Value, GetKeys(item.Key));
                        }
                    }
                }
                return _keyStore;
            }
        }

        protected EntityKey[] GetKeys(string entityName)
        {
            return ((IDbContext)this).GetKeys(entityName);
        }
        EntitySetBase IDbContext.GetSet(string entityName)
        {
            return Sets.FirstOrDefault(p => p.Key.ToLower() == entityName.ToLower()).Value;
        }
        protected EntitySetBase GetSet(string entityName)
        {
            return ((IDbContext)this).GetSet(entityName);
        }

        private static Hashtable keyProperties = new Hashtable();
        private static Hashtable Properties = new Hashtable();
        EntityProperties[] IDbContext.GetKeyProperties(string entityName)
        {
            int hcode = this.DbContext.Database.Connection.ConnectionString.GetHashCode();
            if (!keyProperties.ContainsKey(hcode + entityName))
            {
                var obj = GetSet(hcode + entityName);
                List<EntityProperties> eps = new List<EntityProperties>();
                var ps = obj.ElementType.KeyMembers;
                foreach (var item in ps)
                {
                    EntityProperties ep = new EntityProperties();
                    ep.Name = item.Name;
                    ep.Type = ((PrimitiveType)item.TypeUsage.EdmType).ClrEquivalentType;
                    ep.TypeInBb = item.TypeUsage.EdmType.FullName;
                    ep.TypeName = item.TypeUsage.EdmType.Name;
                    ep.Nullable = (bool)item.TypeUsage.Facets.FirstOrDefault(p => p.Name == "Nullable").Value;
                    ep.DefaultValue = item.TypeUsage.Facets.FirstOrDefault(p => p.Name == "DefaultValue").Value;
                    ep.Facts = item.TypeUsage.Facets.ToDictionary(p => p.Name, p => p);
                    eps.Add(ep);
                }
                keyProperties.Add(hcode + entityName, eps.ToArray());
            }
            return keyProperties[hcode + entityName] as EntityProperties[];
        }

        /// <summary>
        /// 元素列表
        /// </summary>
        /// <param name="EntityName"></param>
        /// <returns></returns>
        EntityProperties[] IDbContext.GetProperties(string EntityName)
        {

            int Hcode = this.DbContext.Database.Connection.ConnectionString.GetHashCode();
            if (!Properties.ContainsKey(Hcode + EntityName))
            {

                var obj = GetSet(EntityName);
                List<EntityProperties> eps = new List<EntityProperties>();
                var ps = obj.ElementType.Members;
                foreach (var p in ps)
                {
                    EntityProperties ep = new EntityProperties();
                    ep.Name = p.Name;

                    ep.Type = ((PrimitiveType)p.TypeUsage.EdmType).ClrEquivalentType;//.BaseType.MetadataProperties;
                    ep.TypeInBb = p.TypeUsage.EdmType.FullName;
                    ep.TypeName = p.TypeUsage.EdmType.Name;
                    ep.Nullable = (bool)p.TypeUsage.Facets.FirstOrDefault(c => c.Name == "Nullable").Value;
                    ep.DefaultValue = p.TypeUsage.Facets.FirstOrDefault(c => c.Name == "DefaultValue").Value;
                    ep.Facts = p.TypeUsage.Facets.ToDictionary(f => f.Name, f => f);
                    eps.Add(ep);

                }


            }
            return Properties[Hcode + EntityName] as EntityProperties[];
        }
        protected EntityProperties[] GetProperties(string EntityName)
        {
            return ((IDbContext)this).GetProperties(EntityName);
        }

        public override void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        public override DbContext DbContext
        {
            get { return this.Db.DbContext; }
        }




        public IEnumerable FetchSQL(string sql, params object[] paramenters)
        {
            return Db.DbContext.Database.SqlQuery(typeof(object), sql, paramenters);
        }

        public IEnumerable<ResultType> FetchSQL<ResultType>(string sql, params object[] paramenters)
        {
            return Db.DbContext.Database.SqlQuery<ResultType>(sql, paramenters);
        }

        public int ExecutionSQL(string sql, params object[] parameters)
        {
            return Db.DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<dynamic> GetValidationErrors()
        {
            List<dynamic> d = new List<dynamic>();
            foreach (var item in Db.DbContext.GetValidationErrors())
            {
                d.Add(new { item.Entry.Entity, item.IsValid, Error = item.ValidationErrors.ToString() });
            }
            return d;
        }
        public override System.Data.Entity.Core.Objects.ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)Db).ObjectContext;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mangon.FrameWork.Package.EntityFramework.InterFace;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;


namespace Mangon.FrameWork.Package.EntityFramework
{
    public abstract class DataBaseBase : IDataBase
    {
        #region 事件
        protected event EFEventHandler ExceptionEvent = null;
        protected event EFEventHandler BeforeSearch = null;
        protected event EFEventHandler AfterSearch = null;
        protected static event EFEventHandler BeforeEvent = null;
        protected static event EFEventHandler AfterEvent = null;
        #endregion
        #region  globalevents for event
        public void onBeforeEvent(ref object data, EFAction action)
        {
            if (BeforeEvent != null)
                BeforeEvent.Invoke(this, new EFEventArgs(ref data, action));

        }

        public void onAfterEvent(ref object data, EFAction action)
        {
            if (AfterEvent != null)
                AfterEvent.Invoke(this, new EFEventArgs(ref data, action));

        }
        public static void AddBeforeEvent(EFEventHandler Event)
        {
            BeforeEvent = (EFEventHandler)Delegate.Combine(BeforeEvent, Event);
        }
        public static void AddAfterEvent(EFEventHandler Event)
        {
            AfterEvent = (EFEventHandler)Delegate.Combine(AfterEvent, Event);
        }
        public static void RemoveBeforeEvent(EFEventHandler Event)
        {
            BeforeEvent = (EFEventHandler)Delegate.Remove(BeforeEvent, Event);
        }
        public static void RemoveAfterEvent(EFEventHandler Event)
        {
            AfterEvent = (EFEventHandler)Delegate.Remove(AfterEvent, Event);
        }
        public static void CleanBeforeEvent()
        {
            BeforeEvent = null;
        }
        public static void CleanAfterEvent()
        {
            AfterEvent = null;
        }
        public void onExceptionEvent(Exception e, EFAction action)
        {
            //if (ExceptionEvent != null)
            //    ExceptionEvent.Invoke(this, new EFEventArgs(ref e, action));

        }
        #endregion


        #region Search
        public void onBeforeSearch()
        {
            object o = new object();
             
            if (BeforeSearch != null)
                BeforeSearch.Invoke(this, new EFEventArgs(ref o, EFAction.Search));
        }

        public void onAfterSearch()
        {
            // if (AfterSearch != null)
            //   AfterSearch.Invoke(this, new EFEventArgs(null, EFAction.Search));

        }
        #endregion


        public DataBaseBase() { }
        public DataBaseBase(DbContext DbContext)
        {
            // this.DbContext = DbContext;
        }



        /// <summary>
        /// test only,never useit
        /// </summary>
        /// <param name="PathAndFilename"></param>
        protected void BackupDataBase(string PathAndFilename)
        {
            SqlConnection conn = new SqlConnection(this.Connection.ConnectionString);
            SqlCommand cmdBK = new SqlCommand();
            cmdBK.CommandType = CommandType.Text;
            cmdBK.Connection = conn;
            cmdBK.CommandText = @"backup database test to disk='" + PathAndFilename + "' with init";
            try
            {
                conn.Open();
                cmdBK.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }


        public virtual System.Data.Common.DbConnection Connection
        {
            get { return ((IObjectContextAdapter)this).ObjectContext.Connection; }
        }
        public virtual System.Data.Common.DbTransaction BeginTransaction()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            return this.Connection.BeginTransaction();
        }

        public virtual System.Data.Common.DbTransaction BeginTransaction(System.Data.IsolationLevel IsolationLevel)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            return this.Connection.BeginTransaction(IsolationLevel);
        }

        public abstract System.Data.Entity.DbContext DbContext
        {
            get;

        }

        public abstract void Dispose();

        public virtual ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)DbContext).ObjectContext; }
        }
    }
}

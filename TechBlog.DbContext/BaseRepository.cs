using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.DbContext
{
    public class BaseRepository
    {
        private bool isDisposed;
        private string transactionName;

        public Context Context { get; set; }

        public IDbConnection DbConnection { get; set; }

        public BaseRepository()
        {
            Context = new Context(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            DbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        #region Transaction
        public DbTransaction BeginTransaction([CallerMemberName] string transactionName = null)
        {
            if (String.IsNullOrEmpty(transactionName))
                throw new ArgumentNullException(transactionName);

            return this.BeginTransaction(IsolationLevel.Serializable, transactionName);
        }

        public DbTransaction BeginTransaction(IsolationLevel level, [CallerMemberName] string transactionName = null)
        {
            if (String.IsNullOrEmpty(transactionName))
                throw new ArgumentNullException(transactionName);

            if (this.Context.Connection.State != ConnectionState.Open)
                this.Context.Connection.Open();
            if (this.Context.Transaction == null)
            {
                this.Context.Transaction = this.Context.Connection.BeginTransaction(level);
                this.transactionName = transactionName;
            }

            return this.Context.Transaction;
        }

        public void RollbackTransaction([CallerMemberName] string transactionName = null)
        {
            this.RollbackTransaction(this.Context.Transaction, transactionName);
        }

        public void RollbackTransaction(DbTransaction transaction, [CallerMemberName] string transactionName = null)
        {
            if (transaction == null)
                return;

            if (this.transactionName == transactionName)
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        public void CommitTransaction([CallerMemberName] string transactionName = null)
        {
            this.CommitTransaction(this.Context.Transaction, transactionName);
        }

        public void CommitTransaction(DbTransaction transaction, [CallerMemberName] string transactionName = null)
        {
            if (transaction == null)
                return;

            if (this.transactionName == transactionName)
            {
                transaction.Commit();
                transaction.Dispose();
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                    DbConnection.Dispose();
                }
            }

            isDisposed = true;
        }

        ~BaseRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}

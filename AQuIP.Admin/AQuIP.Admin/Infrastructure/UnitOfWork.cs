using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AQuIP.Admin.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
    public sealed class UnitOfWork : IUnitOfWork
    {
        IDbConnection _connection = null;
        IDbTransaction _transaction = null;

        internal UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }
        IDbConnection IUnitOfWork.Connection
        {
            get
            {
                return _connection;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }      

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
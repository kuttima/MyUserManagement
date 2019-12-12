using System;
using System.Data;
using System.Data.SqlClient;
using AQuIP.Admin.Data.Repositories;

namespace AQuIP.Admin.Data
{
    public class DapperUnitOfWork : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private UserRepository _userRepository;
        private AccountRepository _accountRepository;

        public DapperUnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        //public UserRepository UserRepository
        //{
        //    get
        //    {
        //        return _userRepository ?? (_userRepository = new UserRepository(_transaction));
        //    }
        //}

        //public AccountRepository AccountRepository
        //{
        //    get
        //    {
        //        return _accountRepository ?? (_accountRepository = new AccountRepository(_transaction));
        //    }
        //}

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _userRepository = null;
            _accountRepository = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}

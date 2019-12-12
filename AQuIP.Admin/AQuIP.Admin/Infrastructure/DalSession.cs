using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace AQuIP.Admin.Infrastructure
{
    public sealed class DalSession : IDisposable
    {
        IDbConnection _connection = null;
        UnitOfWork _unitOfwork = null;

        public DalSession()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _connection.Open();
            _unitOfwork = new Infrastructure.UnitOfWork(_connection);
        }

        public UnitOfWork UnitOfWork
        {
            get { return _unitOfwork; }
        }
        public void Dispose()
        {
            _unitOfwork.Dispose();
            _connection.Dispose();
        }
    }
}
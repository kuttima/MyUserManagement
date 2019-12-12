using System.Linq;
using System.Data;
using AQuIP.Admin.Models;
using AQuIP.Admin.Helpers;
using Dapper;
using AQuIP.Admin.Infrastructure;

namespace AQuIP.Admin.Data.Repositories
{
    public class AccountRepository
    {
        //private IDbTransaction _transaction;
        //private IDbConnection _connection { get { return _transaction.Connection; } }

        //public AccountRepository(IDbTransaction transaction)
        //{
        //    _transaction = transaction;
        //}
        IUnitOfWork unitOfwork = null;

        public AccountRepository(IUnitOfWork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public UserAccount GetUserForLogin(string userName, string password)
        {
           
            var parameters = new { Username = userName, Password = password };

            return unitOfwork.Connection.Query<UserAccount>
                    (Constant.GetUserForLoginQuery, parameters, unitOfwork.Transaction).SingleOrDefault();   
                     
        }


    }
}

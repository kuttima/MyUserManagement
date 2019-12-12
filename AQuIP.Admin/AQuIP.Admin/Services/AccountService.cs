using System.Configuration;
using AQuIP.Admin.Models;
using AQuIP.Admin.Data.Repositories;
using AQuIP.Admin.Infrastructure;

namespace AQuIP.Admin.Services
{
    public class AccountService
    {    

        public UserAccount GetUserForLogin(LoginViewModel user)
        {

            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var accountRepository = new AccountRepository(_uow);

                    var existingUser = accountRepository.GetUserForLogin(user.LoginName, user.Pwd);
                    _uow.Commit();

                    return existingUser;
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }
            }        
        }       
    }
}
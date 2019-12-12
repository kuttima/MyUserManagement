using System.Collections.Generic;
using System.Linq;
using AQuIP.Admin.Models;
using AQuIP.Admin.Helpers;
using AQuIP.Admin.Infrastructure;
using AQuIP.Admin.Data.Repositories;

namespace AQuIP.Admin.Services
{
    public class UserService
    { 
             
        public List<UserAccount> FindAll()
        {
            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    var userList = userRepository.FindAll();
                    _uow.Commit();
                    return userList.ToList();
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                } 
            }
        }

        public UserAccount FindByID(int id)
        {

            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    var user = userRepository.FindByID(id);
                    _uow.Commit();

                    return user;
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }
            }
        }

        public void Add(CreateUserDTO model)
        {
            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    if (model.Role == Constant.RoleAssetViewer)
                    {
                        CreateAssetViewerDTO dto = new CreateAssetViewerDTO()
                        {
                            UserLogin = model.UserLogin,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Password = model.Password,
                            Phone = model.Phone
                        };
                        userRepository.AddAssetViewer(dto);
                    }
                    else
                        userRepository.Add(model);

                    _uow.Commit();
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }
            }
        }


        public void UpdateUser(int id, UserAccount model)
        {
            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);

                    userRepository.UpdateUser(id, model);
                    userRepository.UpdateOrganization(model.UserName, model.Name);
                    _uow.Commit();
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }                              
            }
        }

        public int ResetPassword(ResetPasswordViewModel model)
        {
            int rowsAffected = 0;

            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    rowsAffected = userRepository.ResetPassword(model);
                    _uow.Commit();
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                } 
            }

            return rowsAffected;
        }

        public int ActivateUser(string userName)
        {
            int rowsAffected = 0;

            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    rowsAffected = userRepository.ActivateUser(userName);
                    _uow.Commit();
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }
            }

            return rowsAffected;
        }

        public int DeactivateUser(string userName)
        {
            int rowsAffected = 0;

            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    rowsAffected = userRepository.DeactivateUser(userName);
                    _uow.Commit();
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }
            }

            return rowsAffected;
        }

        public void DeleteUser(string userName)
        {
            
            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);

                    userRepository.DeleteUser(userName);
                    _uow.Commit();                                     
                }
                catch
                {
                    _uow.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<string> GetRoles()
        {
            using (var _dalSession = new DalSession())
            {
                UnitOfWork _uow = _dalSession.UnitOfWork;
                _uow.Begin();
                try
                {
                    var userRepository = new UserRepository(_uow);
                    var roleList = userRepository.GetRoles();
                    _uow.Commit();

                    return roleList;
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
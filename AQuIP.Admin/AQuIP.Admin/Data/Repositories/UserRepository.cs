using System.Collections.Generic;
using System.Linq;
using System.Data;
using AQuIP.Admin.Models;
using AQuIP.Admin.Helpers;
using Dapper;
using AQuIP.Admin.Infrastructure;

namespace AQuIP.Admin.Data.Repositories
{
    public sealed class UserRepository
    {
        
        IUnitOfWork unitOfwork = null;

        public UserRepository(IUnitOfWork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }



        public List<UserAccount> FindAll()
        {

            return unitOfwork.Connection.Query<UserAccount>
                    (Constant.UserSelectListSql, transaction: unitOfwork.Transaction).ToList();           
        }       
               

        public UserAccount FindByID(int id)
        {
            
            return (UserAccount)unitOfwork.Connection.Query<UserAccount>
                (Constant.UserDetailsSql + id, new { id }, transaction: unitOfwork.Transaction).SingleOrDefault();            
        }        

        public void Add(CreateUserDTO model)
        {
            unitOfwork.Connection.Execute("DCP.SP_Add_User", model, transaction: unitOfwork.Transaction, commandType: CommandType.StoredProcedure);         
        }

        public void AddAssetViewer(CreateAssetViewerDTO model)
        {
            unitOfwork.Connection.Execute("DCP.SP_Add_AssetViewerUser", model, transaction: unitOfwork.Transaction, commandType: CommandType.StoredProcedure);
        }
       

        public void UpdateUser(int id, UserAccount model)
        {
            
            var parameters = new { Firstname = model.FirstName, Lastname = model.LastName, Username = model.UserName, Phone = model.Phone, LastChangedUser = model.LastChangedUser, Userid = id };
            int rowsAffected = unitOfwork.Connection.Execute(Constant.UpdateUserQuery, parameters, transaction: unitOfwork.Transaction);                
        }

        public void UpdateOrganization(string userName, string organization)
        {
            var parameters = new { Username = userName, Sitename = organization };
            int rowsaffected = unitOfwork.Connection.Execute("dbo.SP_Update_OrganizationKey", param: parameters, transaction: unitOfwork.Transaction, commandType: CommandType.StoredProcedure);
        }

        public int ResetPassword(ResetPasswordViewModel model)
        {
            int rowsAffected = 0;

            var parameters = new { Password = model.Password, username = model.UserName, Currentuser = model.LastChangedUser };
            rowsAffected = unitOfwork.Connection.Execute(Constant.UpdatePassword, parameters, transaction: unitOfwork.Transaction);

            return rowsAffected;
        }

        public int ActivateUser(string userName)
        {
            int rowsAffected = 0;

            var param = new { Username = userName };
            rowsAffected = unitOfwork.Connection.Execute("DCP.Sp_Activate_User", param, transaction: unitOfwork.Transaction, commandType: CommandType.StoredProcedure);

            return rowsAffected;

        }

        public int DeactivateUser(string userName)
        {
            int rowsAffected = 0;

            var param = new { Username = userName };
            rowsAffected = unitOfwork.Connection.Execute("DCP.Sp_Deactivate_user", param, transaction: unitOfwork.Transaction, commandType: CommandType.StoredProcedure);           

            return rowsAffected;
        }

        public void DeleteUser(string userName)
        {
            var param = new { Username = userName };

            unitOfwork.Connection.Execute("DCP.SP_Delete_User", param, transaction: unitOfwork.Transaction, commandType: CommandType.StoredProcedure);

        }

        public IEnumerable<string> GetRoles()
        {
            return unitOfwork.Connection.Query<string>(Constant.GetRoles, transaction: unitOfwork.Transaction);
        }
    }
}

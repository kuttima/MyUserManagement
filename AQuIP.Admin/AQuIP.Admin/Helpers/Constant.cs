using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AQuIP.Admin.Helpers
{
    public static class Constant
    {
        public const string UserSelectListSql = "SELECT UserId, FirstName, LastName, UserName,  o.Name, r.RoleName FROM [DCP].[USER] u JOIN [DCP].[ORGANIZATION] o ON u.OrganizationKey = o.OrganizationId JOIN [DCP].[UserRoles] ur ON ur.UserKey = u.UserId JOIN [DCP].[ROLE] r ON r.RoleId = ur.RoleKey WHERE u.RecordStatusFlag = 'Active'";
        public const string UserDetailsSql = "SELECT UserId, FirstName, LastName, UserName, u.Phone, o.Name, r.RoleName FROM [DCP].[USER] u INNER JOIN [DCP].[ORGANIZATION] o ON u.OrganizationKey = o.OrganizationId INNER JOIN [DCP].[UserRoles] ur ON ur.UserKey = u.UserId INNER JOIN [DCP].[ROLE] r ON r.RoleId = ur.RoleKey WHERE USERID = ";
        public const string UpdateUserQuery = "UPDATE [DCP].[USER] SET FirstName = @Firstname, LastName = @Lastname, Username = @Username, Phone = @Phone, LastChangedDate = GETDATE(), LastChangedUser = @LastChangedUser WHERE UserId = @Userid";
        public const string UpdatePassword = "UPDATE [DCP].[USER] SET Password = @Password, PasswordResetDate = GETDATE(), LastChangedDate = GETDATE(), LastChangedUser = @Currentuser WHERE Username = @Username";
        public const string GetUserForLoginQuery = "Select Username, Password, r.RoleName from [DCP].[User] u INNER JOIN [DCP].[UserRoles] ur ON ur.UserKey = u.UserId INNER JOIN [DCP].[ROLE] r ON r.RoleId = ur.RoleKey WHERE u.UserName = @Username AND u.Password = @Password";

        public const string GetRoles = "SELECT RoleName FROM [DCP].[Role]";

        public const string RoleAdmin = "TRIAdmin";
        public const string RoleAssetViewer = "AssetViewer";
        public const string Active = "Active";

        public const string DeleteUserSuccessMsg = "User successfully deleted!";
        public const string ActivateUserSuccessMsg = "User Activation was successful!";
        public const string DeactivateUserSuccessMsg = "User Deactivation was successful!";
        public const string ResetPasswordSuccessMsg = "Password was successfully reset for user!";
        public const string EditUserSuccessMsg = "User update was successful!";
        public const string CreateUserSuccessMsg = "User added successfully!";

        public const string LoginErrorMsg = "Username and password do not match!";
        public const string ResetPasswordErrorMsg = "Password reset failed, user does not exist!";
        public const string ActivateUserErrorMsg = "Activation failed, user already active or does not exist!";
        public const string DeactivationErrorMsg = "Deactivation failed, user already deactivated or does not exist!";
        public const string DeletionErrorMsg = "Deletion failed, user already deleted!";

        //Error Handling
        public const string ProductName = "AQuIP.Admin";
        public const string LayerName = "MVC";

    }
}
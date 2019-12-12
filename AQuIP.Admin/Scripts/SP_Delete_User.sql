USE [DCP_AQUIP_DEV]
GO

/****** Object:  StoredProcedure [DCP].[SP_Delete_User]    Script Date: 4/22/2019 3:50:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--======================================================  
--V 1.2.1.0  
--Updated Store procedure becouse it's impacted by the addition of  Users Protocols associations  
--Updated by Getnet  
--======================================================  

CREATE PROCEDURE [DCP].[SP_Delete_User] 
	@Username Varchar(50)
AS
BEGIN
	DECLARE @user_ID INT = NULL

	Select @user_ID=[UserId] from [DCP].[User] where [Username]=@Username

	--Checks if User Protocol assocaiotion exists 
    IF NOT EXISTS (SELECT * 
                FROM   [DCP].[userprotocol] up
                WHERE  userkey = @User_ID and up.RecordStatusFlag = 'Active') 
        BEGIN 
                     
			Delete [DCP].[UserRoles] where [UserKey]= @user_ID
			Delete [DCP].[UserProtocol] where [UserKey]= @user_ID
			ALTER TABLE [DCP].[User] DISABLE TRIGGER User_Audit_Trigger
			Delete [DCP].[User] where [UserId]=@user_ID
			ALTER TABLE [DCP].[User] ENABLE TRIGGER User_Audit_Trigger
	
		END 
    ELSE 
        BEGIN 
            RAISERROR ('Unable to delete user because Active UserProtcol associations exists. So, please first delete user protocol assocaitions.' ,16,1); 
		END
END





GO



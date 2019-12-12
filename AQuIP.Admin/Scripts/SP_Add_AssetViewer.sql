USE [DCP_AQUIP_DEV]
GO

/****** Object:  StoredProcedure [DCP].[SP_Add_AssetViewerUser]    Script Date: 4/22/2019 3:50:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Create script for StoredProcedure [DCP].[SP_Add_AssetViewerUser]    ******/

/*
-- Example of a call

EXEC [DCP].[SP_Add_AssetViewerUser] 
	@UserLogin = 'assetviewer@tsfd.com',
	@FirstName = 'PO FNAME2',
	@LastName = 'PO LNAME',
	@Password = 'popassword2',
	@Phone = '123456'

*/

CREATE
 PROCEDURE [DCP].[SP_Add_AssetViewerUser]
	@UserLogin Varchar(50),
	@FirstName Varchar(500),
	@LastName Varchar(500),
	@Password Varchar(200),
	@Phone	Varchar(50)	
AS
BEGIN
	SET NOCOUNT ON;
	
	
	DECLARE @ROLE_ID INT = NULL
	DECLARE @USER_ID INT = NULL
	DECLARE @INPUT_ISVALID int  = 0
	DECLARE @IS_INACTIVE_USER_ALREADY_EXIST int = 0
	DECLARE @ORGANIZATION_ID int
	


	SELECT @ROLE_ID = ROLEID FROM [DCP].[Role] WHERE [RoleName] = 'AssetViewer'
	SELECT @ORGANIZATION_ID = [OrganizationId]  FROM [DCP].[Organization] WHERE [Name] = 'Organization for asset viewers'
	

	--Check if the user already exists
	IF(@ROLE_ID IS NULL)
	BEGIN
		RAISERROR ('Invalid Role.', 16, 1); 
	END
	ELSE IF(EXISTS (SELECT * FROM [DCP].[User] WHERE [Username] = @UserLogin and RecordStatusFlag = 'Active'))
	BEGIN
		RAISERROR ('The user login already exists.', 16, 1);
	END
	ELSE IF(EXISTS (SELECT * FROM [DCP].[User] WHERE [Username] = @UserLogin and RecordStatusFlag <> 'Active'))
	BEGIN 
		SET @IS_INACTIVE_USER_ALREADY_EXIST = 1
	END 
	
	
	IF(@@ERROR > 0) RETURN 1
	
	
	IF(@ROLE_ID IS NOT NULL OR @ORGANIZATION_ID IS  NOT NULL)
	BEGIN
		SET @INPUT_ISVALID = 1		
	END

		
	IF(@INPUT_ISVALID = 1 AND @IS_INACTIVE_USER_ALREADY_EXIST = 0)
	BEGIN
		BEGIN TRAN T	
			INSERT [DCP].[User] ([FirstName], [LastName], [Username], [Password], [LastLoginDate], [PasswordResetDate], [Phone], [OrganizationKey], 
			[TransactionGuid], [RecordStatusFlag], [RecordStatusDate], [QCDoneFlag], [QCDoneDate], [CreatedUser], [CreatedDate], [LastChangedUser], [LastChangedDate]) 
			VALUES (@FirstName, @LastName, @UserLogin, @Password, GETdATE(), GETdATE(), @Phone, @ORGANIZATION_ID, 
			NEWID(), N'Active', GETdATE(), N'No', '1/1/1950', N'TRIAdmin', GETdATE(), N'TRIAdmin', GETdATE())

			SELECT @USER_ID = UserId FROM DCP.[User] WHERE [Username] = @UserLogin AND [RecordStatusFlag] = 'Active'

			INSERT [DCP].[UserRoles] ([UserKey], [RoleKey], 
			[TransactionGuid], [RecordStatusFlag], [RecordStatusDate], [QCDoneFlag], [QCDoneDate], [CreatedUser], [CreatedDate], [LastChangedUser], [LastChangedDate]) 
			VALUES (@USER_ID, @ROLE_ID,
			NEWID(), N'Active', GETdATE(), N'No', '1/1/1950', N'TRIAdmin', GETdATE(), N'TRIAdmin', GETdATE())
		IF(@@ERROR > 0)
		BEGIN
			ROLLBACK TRAN T
			PRINT 'An error has occurred while adding the user and role. Please try again.'
		END
		ELSE 
		BEGIN
			COMMIT TRAN T
			PRINT 'The user was successfully added.'
		END
	END
	ELSE IF(@INPUT_ISVALID = 1 AND @IS_INACTIVE_USER_ALREADY_EXIST = 1)
	BEGIN 
		
			SELECT @USER_ID = UserId FROM DCP.[User] WHERE [Username] = @UserLogin 

			UPDATE [DCP].[User] SET [FirstName] = @FirstName , [LastName] = @LastName, [Username] = @UserLogin, [Password] = @Password, [LastLoginDate] = GETDATE(), [PasswordResetDate] = GETDATE(), [Phone] = @Phone, [OrganizationKey] = @ORGANIZATION_ID
			WHERE UserId = @USER_ID
			
			UPDATE 	[DCP].[UserRoles] SET [RoleKey]= @ROLE_ID , [RecordStatusFlag] = 'Active' WHERE [UserKey] = @USER_ID;

		IF(@@ERROR > 0)
		BEGIN
			ROLLBACK TRAN U
			PRINT 'An error has occurred while adding the user and role. Please try again.'
		END
		ELSE 
		BEGIN
			COMMIT TRAN U
			PRINT 'The user was successfully updated.'
		END
		
	END
	ELSE
	BEGIN
		--PRINT 'Not a valid combination of a requested user, role and organization.'
		RAISERROR ('Could not add the user and the role. 
		Not a valid combination of a requested user, role and organization.
		Please check the organization name, organization type and the appropriate role.', 16, 1);
	END
END


GO



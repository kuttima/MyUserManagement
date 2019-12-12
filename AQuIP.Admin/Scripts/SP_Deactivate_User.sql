USE [DCP_AQUIP_DEV]
GO

/****** Object:  StoredProcedure [DCP].[Sp_Deactivate_user]    Script Date: 4/22/2019 3:51:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Deactivate User and User role    
--======================================================  
--V 1.2.1.0  
--Updated Store procedure becouse it's impacted by the addition of  Users Protocols associations  
--Updated by Getnet  
--======================================================  
CREATE PROCEDURE [DCP].[Sp_Deactivate_user] @Username VARCHAR(50) 
AS 
  BEGIN 
      --Checks if User Protocol assocaiotion exists  
      IF NOT EXISTS (SELECT * 
                     FROM   [DCP].[userprotocol] up 
                            JOIN dcp.[user] us 
                              ON up.userkey = us.userid 
                     WHERE  us.username = @Username 
                            AND up.recordstatusflag = 'Active') 
        BEGIN 
            BEGIN TRAN t 

            DECLARE @USER_ID INT = NULL 

            UPDATE [DCP].[user] 
            SET    [recordstatusflag] = 'Inactive', 
                   [recordstatusdate] = Getdate(), 
                   [lastchangeddate] = Getdate(), 
                   [lastchangeduser] = N'TRIAdmin' 
            WHERE  [username] = @Username 
                   AND [recordstatusflag] = 'Active' 

            SELECT @USER_ID = userid 
            FROM   dcp.[user] 
            WHERE  [username] = @Username 

            UPDATE [DCP].[userroles] 
            SET    [recordstatusflag] = 'Inactive', 
                   [recordstatusdate] = Getdate(), 
                   [lastchangeddate] = Getdate(), 
                   [lastchangeduser] = N'TRIAdmin' 
            WHERE  [userkey] = @USER_ID 
                   AND [recordstatusflag] = 'Active' 

            IF( @@ERROR > 0 ) 
              BEGIN 
                  ROLLBACK TRAN t 

                  PRINT 'An error has occurred while deactivating the user and role. Please try again.' 
				END 
			ELSE 
			  BEGIN 
				  COMMIT TRAN t 

				  PRINT 'The user was successfully deactivated.' 
			  END 
		END 
		ELSE 
              BEGIN 
                  RAISERROR ('Unable to deactivate user because UserProtcol associations exists.So, please first delete user protocol assocaitions.' ,16,1); 
				END 
END 
GO



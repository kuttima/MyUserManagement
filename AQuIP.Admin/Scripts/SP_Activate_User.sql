USE [DCP_AQUIP_DEV]
GO

/****** Object:  StoredProcedure [DCP].[Sp_Activate_User]    Script Date: 4/22/2019 3:52:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Activate User and User role   
CREATE PROCEDURE [DCP].[Sp_Activate_User] @Username VARCHAR(50) 
AS 
  BEGIN 
      BEGIN TRAN t 

      DECLARE @USER_ID INT = NULL 

      UPDATE [DCP].[user] 
      SET    [recordstatusflag] = 'Active', 
             [recordstatusdate] = Getdate(), 
             [lastchangeddate] = Getdate(), 
             [lastchangeduser] = N'TRIAdmin' 
      WHERE  [username] = @Username 
             AND [recordstatusflag] <> 'Active' 

      SELECT @USER_ID = userid 
      FROM   dcp.[user] 
      WHERE  [username] = @Username 

      UPDATE [DCP].[userroles] 
      SET    [recordstatusflag] = 'Active', 
             [recordstatusdate] = Getdate(), 
             [lastchangeddate] = Getdate(), 
             [lastchangeduser] = N'TRIAdmin' 
      WHERE  [userkey] = @USER_ID 
             AND [recordstatusflag] <> 'Active' 

      IF( @@ERROR > 0 ) 
        BEGIN 
            ROLLBACK TRAN t 

            PRINT 
  'An error has occurred while activating the user and role. Please try again.' 
  END 
  ELSE 
  BEGIN 
      COMMIT TRAN t 

      PRINT 'The user successfully activated.' 
  END 
  END 


GO



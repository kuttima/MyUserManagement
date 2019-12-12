USE [DCP_AQUIP_DEV]
GO

/****** Object:  StoredProcedure [dbo].[SP_Update_OrganizationKey]    Script Date: 4/22/2019 3:51:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--====================================================== 
--V 1.2.1.0 
--Updated Store procedure becouse impacted by the addition of  Users Protocols associations 
--Updated by Getnet 
--====================================================== 
CREATE PROCEDURE [dbo].[SP_Update_OrganizationKey] @Username VARCHAR(50), 
                                                  @Sitename VARCHAR(50) 
AS 
  BEGIN 
      DECLARE @ORG_ID INT = NULL 
      DECLARE @ORG_Type_ID INT = NULL 
      DECLARE @Ex_ORG_Type_ID INT = NULL 
      DECLARE @User_ID INT = NULL 

      --New Org 
      SELECT @ORG_ID = organizationid, 
             @ORG_Type_ID = [organizationtypekey] 
      FROM   dcp.organization O 
      WHERE  [name] = @Sitename 
             AND O.recordstatusflag = 'Active' 

      --Existing User Info 
      SELECT @Ex_ORG_Type_ID = [organizationtypekey], 
             @User_ID = us.userid 
      FROM   [DCP].[user] us 
             JOIN [DCP].[organization] org 
               ON us.[organizationkey] = org.[organizationid] 
      WHERE  [username] = @Username 
             AND us.recordstatusflag = 'Active' 
             AND org.recordstatusflag = 'Active' 

      IF ( @ORG_Type_ID = @Ex_ORG_Type_ID ) 
        BEGIN 
            --Checks if User Protocol assocaiotion exists 
            IF NOT EXISTS (SELECT * 
                       FROM   [DCP].[userprotocol] up
                       WHERE  userkey = @User_ID and up.RecordStatusFlag = 'Active') 
              BEGIN 
                  UPDATE [DCP].[user] 
                  SET    [organizationkey] = @ORG_ID 
                  WHERE  [username] = @Username 
              END 
            ELSE 
              BEGIN 
                  RAISERROR ('Unable to update because UserProtcol associations exists.So, please first delete user protocol assocaitions.' ,16,1); 
				END 
END 
ELSE 
  BEGIN 
      RAISERROR ('Unable to update because UserRoles are Different.So Please delete the user and recreate' ,16,1); 
END 
END 


GO



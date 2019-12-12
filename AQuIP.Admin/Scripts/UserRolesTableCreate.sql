USE [DCP_AQUIP_DEV]
GO

/****** Object:  Table [DCP].[UserRoles]    Script Date: 4/22/2019 3:46:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DCP].[UserRoles](
	[UserRolesId] [int] IDENTITY(1,1) NOT NULL,
	[UserKey] [int] NULL,
	[RoleKey] [int] NULL,
	[TransactionGuid] [uniqueidentifier] NOT NULL,
	[RecordStatusFlag] [varchar](10) NOT NULL,
	[RecordStatusDate] [datetime] NOT NULL,
	[QCDoneFlag] [varchar](10) NOT NULL,
	[QCDoneDate] [datetime] NOT NULL,
	[CreatedUser] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastChangedUser] [nvarchar](50) NOT NULL,
	[LastChangedDate] [datetime] NOT NULL,
	[RecordTimestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRolesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__Transac__625A9A57]  DEFAULT (newid()) FOR [TransactionGuid]
GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__RecordS__634EBE90]  DEFAULT ('Active') FOR [RecordStatusFlag]
GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__RecordS__6442E2C9]  DEFAULT (getdate()) FOR [RecordStatusDate]
GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__QCDoneF__65370702]  DEFAULT ('No') FOR [QCDoneFlag]
GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__QCDoneD__662B2B3B]  DEFAULT (getdate()) FOR [QCDoneDate]
GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__Created__671F4F74]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [DCP].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__LastCha__681373AD]  DEFAULT (getdate()) FOR [LastChangedDate]
GO

ALTER TABLE [DCP].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Role] FOREIGN KEY([RoleKey])
REFERENCES [DCP].[Role] ([RoleId])
GO

ALTER TABLE [DCP].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Role]
GO

ALTER TABLE [DCP].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_User] FOREIGN KEY([UserKey])
REFERENCES [DCP].[User] ([UserId])
GO

ALTER TABLE [DCP].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_User]
GO



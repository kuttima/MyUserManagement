USE [DCP_AQUIP_DEV]
GO

/****** Object:  Table [DCP].[User]    Script Date: 4/22/2019 3:40:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DCP].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](500) NOT NULL,
	[LastName] [varchar](500) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[PasswordResetDate] [datetime] NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[OrganizationKey] [int] NOT NULL,
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
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__Transac__625A9A57]  DEFAULT (newid()) FOR [TransactionGuid]
GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__RecordS__634EBE90]  DEFAULT ('Active') FOR [RecordStatusFlag]
GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__RecordS__6442E2C9]  DEFAULT (getdate()) FOR [RecordStatusDate]
GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__QCDoneF__65370702]  DEFAULT ('No') FOR [QCDoneFlag]
GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__QCDoneD__662B2B3B]  DEFAULT (getdate()) FOR [QCDoneDate]
GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__Created__671F4F74]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [DCP].[User] ADD  CONSTRAINT [DF__User__LastCha__681373AD]  DEFAULT (getdate()) FOR [LastChangedDate]
GO

ALTER TABLE [DCP].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Organization] FOREIGN KEY([OrganizationKey])
REFERENCES [DCP].[Organization] ([OrganizationId])
GO

ALTER TABLE [DCP].[User] CHECK CONSTRAINT [FK_User_Organization]
GO



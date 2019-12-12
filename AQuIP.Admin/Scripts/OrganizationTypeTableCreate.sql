USE [DCP_AQUIP_DEV]
GO

/****** Object:  Table [DCP].[OrganizationTypeLookUp]    Script Date: 4/22/2019 3:42:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DCP].[OrganizationTypeLookUp](
	[OrganizationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationType] [varchar](50) NOT NULL,
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
 CONSTRAINT [PK_OrganizationType] PRIMARY KEY CLUSTERED 
(
	[OrganizationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__Transac__625A9A57]  DEFAULT (newid()) FOR [TransactionGuid]
GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__RecordS__634EBE90]  DEFAULT ('Active') FOR [RecordStatusFlag]
GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__RecordS__6442E2C9]  DEFAULT (getdate()) FOR [RecordStatusDate]
GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__QCDoneF__65370702]  DEFAULT ('No') FOR [QCDoneFlag]
GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__QCDoneD__662B2B3B]  DEFAULT (getdate()) FOR [QCDoneDate]
GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__Created__671F4F74]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [DCP].[OrganizationTypeLookUp] ADD  CONSTRAINT [DF__OrganizationTypeLookUp__LastCha__681373AD]  DEFAULT (getdate()) FOR [LastChangedDate]
GO



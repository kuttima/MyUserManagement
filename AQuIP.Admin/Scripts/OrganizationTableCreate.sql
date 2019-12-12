USE [DCP_AQUIP_DEV]
GO

/****** Object:  Table [DCP].[Organization]    Script Date: 4/22/2019 3:41:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DCP].[Organization](
	[OrganizationId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationTypeKey] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[AddressLine1] [varchar](100) NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
	[Phone] [varchar](20) NULL,
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
	[Country] [varchar](200) NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__Transac__625A9A57]  DEFAULT (newid()) FOR [TransactionGuid]
GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__RecordS__634EBE90]  DEFAULT ('Active') FOR [RecordStatusFlag]
GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__RecordS__6442E2C9]  DEFAULT (getdate()) FOR [RecordStatusDate]
GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__QCDoneF__65370702]  DEFAULT ('No') FOR [QCDoneFlag]
GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__QCDoneD__662B2B3B]  DEFAULT (getdate()) FOR [QCDoneDate]
GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__Created__671F4F74]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [DCP].[Organization] ADD  CONSTRAINT [DF__Organization__LastCha__681373AD]  DEFAULT (getdate()) FOR [LastChangedDate]
GO

ALTER TABLE [DCP].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_OrganizationType] FOREIGN KEY([OrganizationTypeKey])
REFERENCES [DCP].[OrganizationTypeLookUp] ([OrganizationTypeId])
GO

ALTER TABLE [DCP].[Organization] CHECK CONSTRAINT [FK_Organization_OrganizationType]
GO



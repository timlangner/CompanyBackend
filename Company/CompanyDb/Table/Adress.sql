CREATE TABLE [dbo].[Adress]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Street] nvarchar(512) NOT NULL,
	[City] nvarchar(256) NOT NULL,
	[ZipCode] varchar(128) NOT NULL,
	[Country] varchar(256) NOT NULL,
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2
)

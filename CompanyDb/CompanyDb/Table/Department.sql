CREATE TABLE [dbo].[Department]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] varchar(256) NOT NULL,
	[Description] nvarchar(max),
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2,
	[CompanyId] INT NOT NULL REFERENCES Company(Id)
)
